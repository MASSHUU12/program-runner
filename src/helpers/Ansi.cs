namespace Ansi
{
  /// <summary>
  /// A collection of ANSI escape codes for text formatting.
  /// </summary>
  public static class Text
  {
    /// <summary>
    /// Resets all text formatting.
    /// </summary>
    public const string Reset = "\x1b[0m";
    /// <summary>
    /// Sets text to bold.
    /// </summary>
    public const string Bold = "\x1b[1m";
    /// <summary>
    /// Sets text to faint.
    /// </summary>
    public const string Faint = "\x1b[2m";
    /// <summary>
    /// Sets text to italic.
    /// </summary>
    public const string Italic = "\x1b[3m";
    /// <summary>
    /// Sets text to underlined.
    /// </summary>
    public const string Underline = "\x1b[4m";
    /// <summary>
    /// Sets text to blink slowly.
    /// </summary>
    public const string BlinkSlow = "\x1b[5m";
    /// <summary>
    /// Sets text to blink rapidly.
    /// </summary>
    public const string BlinkRapid = "\x1b[6m";
    /// <summary>
    /// Sets text to strikethrough.
    /// </summary>
    public const string Strike = "\x1b[9m";
    /// <summary>
    /// Sets text to double underlined.
    /// </summary>
    public const string UnderlineDouble = "\x1b[21m";
  }

  /// <summary>
  /// A collection of ANSI escape codes for text colors.
  /// </summary>
  public static class Color
  {
    /// <summary>
    /// Resets all text formatting.
    /// </summary>
    public const string Reset = "\x1b[0m";

    /// <summary>
    /// A collection of ANSI escape codes for foreground text colors.
    /// </summary>
    public static class Foreground
    {
      /// <summary>
      /// Sets foreground text color to black.
      /// </summary>
      public const string Black = "\x1b[30m";
      /// <summary>
      /// Sets foreground text color to red.
      /// </summary>
      public const string Red = "\x1b[31m";
      /// <summary>
      /// Sets foreground text color to green.
      /// </summary>
      public const string Green = "\x1b[32m";
      /// <summary>
      /// Sets foreground text color to yellow.
      /// </summary>
      public const string Yellow = "\x1b[33m";
      /// <summary>
      /// Sets foreground text color to blue.
      /// </summary>
      public const string Blue = "\x1b[34m";
      /// <summary>
      /// Sets foreground text color to magenta.
      /// </summary>
      public const string Magenta = "\x1b[35m";
      /// <summary>
      /// Sets foreground text color to cyan.
      /// </summary>
      public const string Cyan = "\x1b[36m ";
      /// <summary>
      /// Sets foreground text color to white.
      /// </summary>
      public const string White = "\x1b[37m ";
      /// <summary>
      /// Sets bright foreground text color to black.
      /// </summary>
      public const string BrightBlack = "\x1b[90m ";
      /// <summary>
      /// Sets bright foreground text color to red.
      /// </summary>
      public const string BrightRed = "\x1b[91m ";
      /// <summary>
      /// Sets bright foreground text color to bright green.
      /// </summary>
      public const string BrightGreen = "\x1b[92m ";
      /// <summary>
      /// Sets bright foreground text color to bright yellow.
      /// </summary>
      public const string BrightYellow = "\x1b[93m ";
      /// <summary>
      /// Sets bright foreground text color to bright blue.
      /// </summary>
      public const string BrightBlue = "\x1b[94m ";
      /// <summary>
      /// Sets bright foreground text color to bright magenta.
      /// </summary>
      public const string BrightMagenta = "\x1b[95m ";
      /// <summary>
      /// Sets bright foreground text color to bright cyan.
      /// </summary>
      public const string BrightCyan = "\x1b[96m ";
      /// <summary>
      /// Sets bright foreground text color to bright white.
      /// </summary>
      public const string BrightWhite = "\x1b[97m ";
    }

    /// <summary>
    /// A collection of ANSI escape codes for background text colors.
    /// </summary>
    public static class Background
    {
      /// <summary>
      /// Sets background text color to black.
      /// </summary>
      public const string Black = "\x1b[40m";
      /// <summary>
      /// Sets background text color to red.
      /// </summary>
      public const string Red = "\x1b[41m";
      /// <summary>
      /// Sets background text color to green.
      /// </summary>
      public const string Green = "\x1b[42m";
      /// <summary>
      /// Sets background text color to yellow.
      /// </summary>
      public const string Yellow = "\x1b[43m";
      /// <summary>
      /// Sets background text color to blue.
      /// </summary>
      public const string Blue = "\x1b[44m";
      /// <summary>
      /// Sets background text color to magenta.
      /// </summary>
      public const string Magenta = "\x1b[45m";
      /// <summary>
      /// Sets background text color to cyan.
      /// </summary>
      public const string Cyan = "\x1b[46m";
      /// <summary>
      /// Sets background text color to white.
      /// </summary>
      public const string White = "\x1b[47m";
      /// <summary>
      /// Sets background text color to bright black.
      /// </summary>
      public const string BrightBlack = "\x1b[10m";
      /// <summary>
      /// Sets background text color to bright red.
      /// </summary>
      public const string BrightRed = "\x1b[101m";
      /// <summary>
      /// Sets background text color to bright green.
      /// </summary>
      public const string BrightGreen = "\x1b[102m";
      /// <summary>
      /// Sets background text color to bright yellow.
      /// </summary>
      public const string BrightYellow = "\x1b[103m";
      /// <summary>
      /// Sets background text color to bright blue.
      /// </summary>
      public const string BrightBlue = "\x1b[104m";
      /// <summary>
      /// Sets background text color to bright magenta.
      /// </summary>
      public const string BrightMagenta = "\x1b[105m";
      /// <summary>
      /// Sets background text color to bright cyan.
      /// </summary>
      public const string BrightCyan = "\x1b[106m";
      /// <summary>
      /// Sets background text color to bright white.
      /// </summary>
      public const string BrightWhite = "\x1b[107m";
    }
  }

  /// <summary>
  /// A collection of ANSI escape codes for controlling terminal behavior.
  /// </summary>
  public static class Misc
  {
    /// <summary>
    /// Clears terminal screen.
    /// </summary>
    public const string ClearScreen = "\x1b[2J";
  }
}
