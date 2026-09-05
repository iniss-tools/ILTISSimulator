using System.Reflection;
using ILTISSimulator.Data;

namespace ILTISSimulatorTests;

/// <summary>
///     Spoločný testovací grafikon z priečinka <c>TestData</c>.
/// </summary>
internal static class SimulationFixture
{
    private static GvdData _gvd;

    /// <summary>
    ///     Prvý deň platnosti testovacieho grafikonu.
    /// </summary>
    public static readonly DateTime FirstDay = new(2024, 1, 1);

    /// <summary>
    ///     Načítaný testovací grafikon.
    /// </summary>
    public static GvdData Gvd => _gvd ??= GvdLightReader.Read(Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".", "TestData"));
}
