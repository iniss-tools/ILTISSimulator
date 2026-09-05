using System;
using System.Collections.Generic;
using System.Linq;
using ILTISSimulator.Gtn;
using ILTISSimulator.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTISSimulatorTests;

/// <summary>
///     Testy časovej osi a jadra simulácie. Jadro sa krokuje ručne cez <see cref="SimulationEngine.Pump" />,
///     takže testy nepotrebujú vlákna ani čakanie.
/// </summary>
[TestClass]
public class SimulationEngineTests
{
    private static readonly DateTime Day = SimulationFixture.FirstDay;
    private static readonly DateTime EndOfDay = SimulationFixture.FirstDay.AddDays(2);

    private static SimulationOptions Options() => new()
    {
        Seed = 5,
        OnTimeProbability = 1.0,
        DelayGrowthProbability = 0.0,
        TrackDiversionProbability = 0.0,
        CancellationProbability = 0.0
    };

    private static SimulationEngine NewEngine(SimulationOptions options = null) =>
        new(SimulationFixture.Gvd, options ?? Options());

    [TestMethod]
    public void Timeline_IsSortedByTime()
    {
        var runs = new DayPlanner(SimulationFixture.Gvd, Options()).Plan(Day);
        var timeline = new SimulationTimeline(runs);

        var times = timeline.Events.Select(simEvent => simEvent.Time).ToList();
        CollectionAssert.AreEqual(times.OrderBy(time => time).ToArray(), times);
    }

    [TestMethod]
    public void Timeline_SkipsCancelledRuns()
    {
        var options = Options();
        options.CancellationProbability = 1.0;

        var timeline = new SimulationTimeline(new DayPlanner(SimulationFixture.Gvd, options).Plan(Day));

        Assert.AreEqual(0, timeline.Events.Count);
        Assert.IsTrue(timeline.IsFinished);
    }

    [TestMethod]
    public void DueUpTo_ReleasesEventsGraduallyAndOnlyOnce()
    {
        var timeline = new SimulationTimeline(new DayPlanner(SimulationFixture.Gvd, Options()).Plan(Day));
        var total = timeline.Events.Count;

        var firstBatch = timeline.DueUpTo(Day.AddHours(11));
        Assert.IsTrue(firstBatch.Count > 0);
        Assert.IsTrue(firstBatch.Count < total);
        Assert.AreEqual(0, timeline.DueUpTo(Day.AddHours(11)).Count);

        var rest = timeline.DueUpTo(EndOfDay);
        Assert.AreEqual(total, firstBatch.Count + rest.Count);
        Assert.IsTrue(timeline.IsFinished);
    }

    [TestMethod]
    public void SeekTo_SkipsEventsWithoutReleasingThem()
    {
        var timeline = new SimulationTimeline(new DayPlanner(SimulationFixture.Gvd, Options()).Plan(Day));
        var total = timeline.Events.Count;

        var skipped = timeline.SeekTo(Day.AddHours(11));

        Assert.IsTrue(skipped > 0);
        Assert.AreEqual(total - skipped, timeline.DueUpTo(EndOfDay).Count);
    }

    [TestMethod]
    public void PlanDay_ThenPump_EmitsEveryEventInOrder()
    {
        using var engine = NewEngine();
        var seen = new List<SimEventDueEventArgs>();
        engine.EventDue += (_, args) => seen.Add(args);

        engine.PlanDay(Day);
        var count = engine.Pump(EndOfDay);

        Assert.AreEqual(engine.Timeline.Events.Count, count);
        Assert.AreEqual(count, seen.Count);
        CollectionAssert.AreEqual(seen.Select(args => args.SimEvent.Time).OrderBy(time => time).ToArray(),
            seen.Select(args => args.SimEvent.Time).ToArray());
    }

    [TestMethod]
    public void PlanDay_WithStartTime_SkipsEarlierEvents()
    {
        using var engine = NewEngine();
        var seen = new List<SimEventDueEventArgs>();
        engine.EventDue += (_, args) => seen.Add(args);

        engine.PlanDay(Day, Day.AddHours(11));
        engine.Pump(EndOfDay);

        Assert.IsTrue(seen.Count > 0);
        Assert.IsTrue(seen.All(args => args.SimEvent.Time > Day.AddHours(11)));
    }

    [TestMethod]
    public void JumpTo_DropsEventsItPassed()
    {
        using var engine = NewEngine();
        var seen = new List<SimEventDueEventArgs>();
        engine.EventDue += (_, args) => seen.Add(args);

        engine.PlanDay(Day);
        engine.JumpTo(Day.AddHours(23));
        engine.Pump(EndOfDay);

        Assert.IsTrue(seen.All(args => args.SimEvent.Time > Day.AddHours(23)));
    }

    [TestMethod]
    public void Pump_BuildsMessageForEveryEvent()
    {
        using var engine = NewEngine();
        var messages = new List<GtnMessage>();
        engine.EventDue += (_, args) => messages.Add(args.Message);

        engine.PlanDay(Day);
        engine.Pump(EndOfDay);

        Assert.AreEqual(engine.Timeline.Events.Count, messages.Count);
        Assert.IsTrue(messages.All(message => message.StationField == "613600"));
        Assert.IsTrue(messages.All(message => GtnEncoder.Encode(message).Length >= 30));
    }

    [TestMethod]
    public void Pump_MessagesRoundTripThroughTheEncoder()
    {
        using var engine = NewEngine();
        var pairs = new List<SimEventDueEventArgs>();
        engine.EventDue += (_, args) => pairs.Add(args);

        engine.PlanDay(Day);
        engine.Pump(EndOfDay);

        foreach (var pair in pairs)
        {
            var wire = GtnEncoder.Encode(pair.Message);

            Assert.IsTrue(GtnEncoder.TryDecode(wire, pair.SimEvent.Time, out var decoded, out var error), error);
            Assert.AreEqual(pair.SimEvent.Run.Train.Number, decoded.TrainNumber);
            Assert.AreEqual(pair.SimEvent.Time, decoded.Time);
            Assert.IsTrue(StationCodec.Matches(decoded.StationField, SimulationFixture.Gvd.StationId));

            // Správy 080-5 a 080-6 koľaj nenesú.
            if (pair.SimEvent.Kind != SimEventKind.DelayReason)
                Assert.AreEqual(pair.SimEvent.Track.Key, decoded.Track);
        }
    }

    [TestMethod]
    public void Pump_EveryMessageStaysInsideInissMatchingWindow()
    {
        var options = Options();
        options.OnTimeProbability = 0.0;
        options.MeanDelayMinutes = 60;

        using var engine = NewEngine(options);
        var pairs = new List<SimEventDueEventArgs>();
        engine.EventDue += (_, args) => pairs.Add(args);

        engine.PlanDay(Day);
        engine.Pump(EndOfDay);

        foreach (var pair in pairs)
        {
            var offset = pair.SimEvent.Time - pair.SimEvent.PlannedTime;

            Assert.IsTrue(offset > TimeSpan.FromHours(-2), $"{pair.SimEvent} je viac než 2 h pred grafikonom");
            Assert.IsTrue(offset < TimeSpan.FromHours(18), $"{pair.SimEvent} je viac než 18 h po grafikone");
        }
    }

    [TestMethod]
    public void MessageBuilder_MapsDelayReasonEventToItsMessage()
    {
        var options = Options();
        options.OnTimeProbability = 0.0;
        options.MeanDelayMinutes = 30;
        options.DelayReasonProbability = 1.0;
        options.DelayReasons = new List<int> { 2 };

        var builder = new GtnMessageBuilder(SimulationFixture.Gvd.StationId);
        var run = new DayPlanner(SimulationFixture.Gvd, options).Plan(Day)
            .First(candidate => candidate.DelayReason != 0);

        var message = builder.Build(run.Events.First(simEvent => simEvent.Kind == SimEventKind.DelayReason));

        Assert.AreEqual(GtnMessageType.DelayReason, message.Type);
        Assert.AreEqual("02", message.DelayReason);
    }

    [TestMethod]
    public void MessageBuilder_MapsEventKindsToMessageTypes()
    {
        var builder = new GtnMessageBuilder(SimulationFixture.Gvd.StationId);
        var runs = new DayPlanner(SimulationFixture.Gvd, Options()).Plan(Day);
        var run = runs.Single(candidate => candidate.Train.Number == "2005");

        var byKind = run.Events.ToDictionary(simEvent => simEvent.Kind, builder.Build);

        Assert.AreEqual(GtnMessageType.Route, byKind[SimEventKind.EntryRoute].Type);
        Assert.AreEqual(GtnRouteKind.Entry, byKind[SimEventKind.EntryRoute].RouteKind);
        Assert.AreEqual(GtnMessageType.Route, byKind[SimEventKind.ExitRoute].Type);
        Assert.AreEqual(GtnRouteKind.Exit, byKind[SimEventKind.ExitRoute].RouteKind);
        Assert.AreEqual(GtnMessageType.Arrival, byKind[SimEventKind.Arrival].Type);
        Assert.AreEqual(GtnMessageType.Departure, byKind[SimEventKind.Departure].Type);
    }

    [TestMethod]
    public void Start_BeforePlanDay_Throws()
    {
        using var engine = NewEngine();

        Assert.ThrowsExactly<InvalidOperationException>(() => engine.Start());
    }

    [TestMethod]
    public void VirtualTime_AdvancesWithTimeFactor()
    {
        var options = Options();
        options.TimeFactor = 600;

        using var engine = NewEngine(options);
        engine.PlanDay(Day, Day.AddHours(6));

        Assert.AreEqual(Day.AddHours(6), engine.VirtualTime);

        engine.Start();
        System.Threading.Thread.Sleep(300);
        engine.Pause();

        var advanced = engine.VirtualTime - Day.AddHours(6);
        Assert.IsTrue(advanced > TimeSpan.FromSeconds(30), $"virtuálny čas sa posunul len o {advanced}");

        var frozen = engine.VirtualTime;
        System.Threading.Thread.Sleep(150);
        Assert.AreEqual(frozen, engine.VirtualTime);
    }
}
