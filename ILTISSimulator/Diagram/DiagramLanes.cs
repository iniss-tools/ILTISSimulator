using GVDEditor.Domain.Entities;
using ILTISSimulator.Simulation;

namespace ILTISSimulator.Diagram;

/// <summary>
///     Pruh vlaku v koľajovom diagrame - jazda a riadok v rámci koľaje.
/// </summary>
/// <param name="Run">Jazda vlaku.</param>
/// <param name="Row">Riadok v koľaji - viac riadkov má koľaj len pri súbehu vlakov.</param>
public sealed record DiagramBar(TrainRun Run, int Row);

/// <summary>
///     Koľaj v koľajovom diagrame s pruhmi vlakov, ktoré ju držia.
/// </summary>
/// <param name="Track">Koľaj.</param>
/// <param name="Excluded">Koľaj je vynechaná z plánovania - simulátor na ňu sám vlaky nestavia.</param>
/// <param name="Rows">Počet riadkov, aspoň jeden.</param>
/// <param name="Bars">Pruhy vlakov zoradené podľa začiatku obsadenia.</param>
public sealed record DiagramLane(Track Track, bool Excluded, int Rows, IReadOnlyList<DiagramBar> Bars);

/// <summary>
///     Rozloženie koľajového diagramu - koľaje v poradí grafikonu a vlaky na nich.
/// </summary>
public static class DiagramLanes
{
    /// <summary>
    ///     Zostaví koľaje diagramu. Sú v ňom koľaje, ktoré sa dajú ohlásiť, v poradí grafikonu, a za nimi koľaje, na
    ///     ktorých stojí vlak, hoci sa ohlásiť nedajú. Vlaky, ktoré koľaj držia naraz (súbeh), dostanú vlastný riadok.
    /// </summary>
    /// <param name="tracks">Koľaje stanice.</param>
    /// <param name="excluded">Kľúče koľají vynechaných z plánovania.</param>
    /// <param name="runs">Jazdy dňa.</param>
    /// <returns>Koľaje diagramu zhora nadol.</returns>
    public static IReadOnlyList<DiagramLane> Build(IEnumerable<Track> tracks, IEnumerable<string> excluded,
        IEnumerable<TrainRun> runs)
    {
        ArgumentNullException.ThrowIfNull(tracks);
        ArgumentNullException.ThrowIfNull(runs);

        var skip = new HashSet<string>(excluded ?? [], StringComparer.OrdinalIgnoreCase);
        var byTrack = runs.Where(run => run.Track is not null)
            .GroupBy(run => run.Track.Key, StringComparer.Ordinal)
            .ToDictionary(group => group.Key, group => group.ToList(), StringComparer.Ordinal);

        var ordered = tracks.Where(TrackAllocator.CanUse).ToList();
        var known = new HashSet<string>(ordered.Select(track => track.Key), StringComparer.Ordinal);

        // vlak na kolaji, ktora sa ohlasit neda (napriklad z grafikonu) - do diagramu patri tiez
        foreach (var runsOnTrack in byTrack.Values)
        {
            var track = runsOnTrack[0].Track;
            if (known.Add(track.Key))
                ordered.Add(track);
        }

        return ordered
            .Select(track => Lane(track, skip.Contains(track.Key), byTrack.GetValueOrDefault(track.Key) ?? []))
            .ToList();
    }

    private static DiagramLane Lane(Track track, bool excluded, List<TrainRun> runs)
    {
        var rowEnds = new List<DateTime>();
        var bars = new List<DiagramBar>();

        foreach (var run in runs.OrderBy(run => run.OccupiedFrom).ThenBy(run => run.OccupiedTo))
        {
            var row = rowEnds.FindIndex(end => end <= run.OccupiedFrom);
            if (row < 0)
            {
                row = rowEnds.Count;
                rowEnds.Add(run.OccupiedTo);
            }
            else
            {
                rowEnds[row] = run.OccupiedTo;
            }

            bars.Add(new DiagramBar(run, row));
        }

        return new DiagramLane(track, excluded, Math.Max(1, rowEnds.Count), bars);
    }
}
