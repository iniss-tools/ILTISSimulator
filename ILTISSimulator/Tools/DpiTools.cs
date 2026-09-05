namespace ILTISSimulator.Tools;

/// <summary>
///     Prepočet rozmerov z návrhu na DPI monitora.
/// </summary>
/// <remarks>
///     V režime PerMonitorV2 sa vo Windows Forms zväčší písmo, ale rozmery zadané v návrhu zostanú tak, ako
///     sú - na monitore so 150 % by tak boli všetky polia o tretinu užšie, než ich text potrebuje. Ovládacie
///     prvky, ktoré si veľkosť rátajú samy, sú v poriadku; dopočítať treba tie s pevnou šírkou, preto sa
///     škáluje celé okno naraz. Písmo <see cref="Control.Scale(SizeF)" /> nemení, to je už v správnej
///     veľkosti.
/// </remarks>
internal static class DpiTools
{
    /// <summary>
    ///     DPI, pre ktoré platia rozmery v návrhu okien.
    /// </summary>
    public const float DesignDpi = 96f;

    /// <summary>
    ///     Preškáluje okno z návrhových rozmerov na DPI monitora, na ktorom vzniklo.
    /// </summary>
    /// <param name="form">Okno hneď po <c>InitializeComponent</c>.</param>
    /// <param name="designClientWidth">Šírka vnútra okna podľa návrhu. Slúži ako značka: ak sa už nezhoduje,
    ///     rozmery preškálovali Windows Forms samy a druhýkrát sa škálovať nesmie.</param>
    public static void ScaleToDpi(Form form, int designClientWidth)
    {
        if (form is null)
            throw new ArgumentNullException(nameof(form));

        var factor = form.DeviceDpi / DesignDpi;

        if (factor <= 1.01f || form.ClientSize.Width != designClientWidth)
            return;

        form.Scale(new SizeF(factor, factor));
    }

    /// <summary>
    ///     Prepočíta rozmer z návrhu na DPI okna. Slúži pre položky panelov nástrojov, ktoré nie sú
    ///     ovládacími prvkami, a preto ich <see cref="ScaleToDpi" /> nezachytí.
    /// </summary>
    /// <param name="control">Ovládací prvok, z ktorého sa zoberie DPI.</param>
    /// <param name="designValue">Rozmer podľa návrhu.</param>
    /// <returns>Prepočítaný rozmer.</returns>
    public static int Scaled(Control control, int designValue)
    {
        if (control is null)
            throw new ArgumentNullException(nameof(control));

        return (int)Math.Round(designValue * control.DeviceDpi / DesignDpi);
    }

    /// <summary>
    ///     Zmenší okno tak, aby sa zmestilo na obrazovku.
    /// </summary>
    /// <param name="form">Okno.</param>
    public static void FitToScreen(Form form)
    {
        if (form is null)
            throw new ArgumentNullException(nameof(form));

        // Cez obdĺžnik, nie cez okno - inak by sa okno muselo vytvoriť skôr, než ho zobrazíme.
        var work = Screen.FromRectangle(form.Bounds).WorkingArea;

        if (form.Width > work.Width)
            form.Width = work.Width;
        if (form.Height > work.Height)
            form.Height = work.Height;
    }
}
