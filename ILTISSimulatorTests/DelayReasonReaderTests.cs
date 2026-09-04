using System;
using System.IO;
using System.Linq;
using ILTISSimulator.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTISSimulatorTests;

/// <summary>
///     Testy načítania dôvodov meškania zo zvukovej banky.
/// </summary>
[TestClass]
public class DelayReasonReaderTests
{
    /// <summary>
    ///     Inštalácia, na ktorej bolo číslovanie dôvodov overené naživo proti INISSu.
    /// </summary>
    private const string BardejovGvd = @"D:\INISSroot\INISSes\INISSBj\DATA\Košice.2025";

    [TestMethod]
    public void IsUsable_LastItemOfGroupIsUnreachable()
    {
        // INISS testuje "číslo < počet" a potom siaha na položku "číslo - 1".
        Assert.IsFalse(DelayReasonReader.IsUsable(0, 5));
        Assert.IsTrue(DelayReasonReader.IsUsable(1, 5));
        Assert.IsTrue(DelayReasonReader.IsUsable(4, 5));
        Assert.IsFalse(DelayReasonReader.IsUsable(5, 5));
        Assert.IsFalse(DelayReasonReader.IsUsable(6, 5));
    }

    [TestMethod]
    public void IsUsable_SingleItemGroupHasNothingUsable()
    {
        Assert.IsFalse(DelayReasonReader.IsUsable(1, 1));
        Assert.IsFalse(DelayReasonReader.IsUsable(1, 0));
    }

    [TestMethod]
    public void ResolveRawBankDir_LooksTwoLevelsAboveTheTimetable()
    {
        var root = Path.Combine(Path.GetTempPath(), "iltis-bank-" + Guid.NewGuid().ToString("N"));
        var gvd = Path.Combine(root, "DATA", "Test.2025");
        Directory.CreateDirectory(gvd);

        try
        {
            Assert.IsNull(DelayReasonReader.ResolveRawBankDir(gvd), "banka ešte neexistuje");

            var bank = Path.Combine(root, DelayReasonReader.RawBankDirName);
            Directory.CreateDirectory(bank);

            Assert.AreEqual(bank, DelayReasonReader.ResolveRawBankDir(gvd));
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [TestMethod]
    public void ResolveRawBankDir_HandlesMissingInput()
    {
        Assert.IsNull(DelayReasonReader.ResolveRawBankDir(null));
        Assert.IsNull(DelayReasonReader.ResolveRawBankDir(""));
    }

    [TestMethod]
    public void ReadForGvd_WithoutBank_ReturnsEmpty()
    {
        var root = Path.Combine(Path.GetTempPath(), "iltis-nobank-" + Guid.NewGuid().ToString("N"));
        var gvd = Path.Combine(root, "DATA", "Test.2025");
        Directory.CreateDirectory(gvd);

        try
        {
            Assert.AreEqual(0, DelayReasonReader.ReadForGvd(gvd).Count);
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [TestMethod]
    public void Read_MissingDirectory_Throws()
    {
        Assert.ThrowsException<DirectoryNotFoundException>(
            () => DelayReasonReader.Read(Path.Combine(Path.GetTempPath(), "iltis-ziadna-banka-" + Guid.NewGuid())));
    }

    [TestMethod]
    public void ReadForGvd_BardejovBank_MatchesWhatInissReported()
    {
        if (!Directory.Exists(BardejovGvd))
            Assert.Inconclusive($"Inštalácia {BardejovGvd} nie je k dispozícii.");

        var reasons = DelayReasonReader.ReadForGvd(BardejovGvd);

        Assert.AreEqual(5, reasons.Count);
        CollectionAssert.AreEqual(
            new[] { "Nehoda", "Počasie", "Prevádzkové dôvody", "Technické príčiny", "Výlukové práce" },
            reasons.Select(reason => reason.Key).ToArray());

        // Čísla 01 až 04 vrátil bežiaci INISS s týmito názvami, 05 vrátil ako (unknown).
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, reasons.Select(reason => reason.Number).ToArray());
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 },
            reasons.Where(reason => reason.Usable).Select(reason => reason.Number).ToArray());

        Assert.AreEqual("z dôvodu nehody na trati", reasons[0].Text);
        Assert.AreEqual("01 Nehoda", reasons[0].ToString());
    }
}
