namespace Ansi
{
  // A collection of ANSI escape codes for text formatting.
  public static class Text
  {
    // Resets all text formatting.
    public const string Reset = "\x1b[0m";
    // Sets text to bold.
    public const string Bold = "\x1b[1m";
    // Sets text to faint.
    public const string Faint = "\x1b[2m";
    // Sets text to italic.
    public const string Italic = "\x1b[3m";
    // Sets text to underlined.
    public const string Underline = "\x1b[4m";
    // Sets text to blink slowly.
    public const string BlinkSlow = "\x1b[5m";
    // Sets text to blink rapidly.
    public const string BlinkRapid = "\x1b[6m";
    // Sets text to strikethrough.
    public const string Strike = "\x1b[9m";
    // Sets text to double underlined.
    public const string UnderlineDouble = "\x1b[21m";
  }

  // A collection of ANSI escape codes for text colors.
  public static class Color
  {
    public const string Reset = "\x1b[0m";

    public static class Foreground
    {
      public const string Black = "\x1b[30m";
      public const string Red = "\x1b[31m";
      public const string Green = "\x1b[32m";
      public const string Yellow = "\x1b[33m";
      public const string Blue = "\x1b[34m";
      public const string Magenta = "\x1b[35m";
      public const string Cyan = "\x1b[36m ";
      public const string White = "\x1b[37m ";
      public const string BrightBlack = "\x1b[90m ";
      public const string BrightRed = "\x1b[91m ";
      public const string BrightGreen = "\x1b[92m ";
      public const string BrightYellow = "\x1b[93m ";
      public const string BrightBlue = "\x1b[94m ";
      public const string BrightMagenta = "\x1b[95m ";
      public const string BrightCyan = "\x1b[96m ";
      public const string BrightWhite = "\x1b[97m ";
    }

    public static class Background
    {
      public const string Black = "\x1b[40m";
      public const string Red = "\x1b[41m";
      public const string Green = "\x1b[42m";
      public const string Yellow = "\x1b[43m";
      public const string Blue = "\x1b[44m";
      public const string Magenta = "\x1b[45m";
      public const string Cyan = "\x1b[46m";
      public const string White = "\x1b[47m";
      public const string BrightBlack = "\x1b[10m";
      public const string BrightRed = "\x1b[101m";
      public const string BrightGreen = "\x1b[102m";
      public const string BrightYellow = "\x1b[103m";
      public const string BrightBlue = "\x1b[104m";
      public const string BrightMagenta = "\x1b[105m";
      public const string BrightCyan = "\x1b[106m";
      public const string BrightWhite = "\x1b[107m";
    }
  }

  // A collection of ANSI escape codes for controlling terminal behavior.
  public static class Misc
  {
    public const string ClearScreen = "\x1b[2J";
  }
}
