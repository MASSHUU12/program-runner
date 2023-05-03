using Constants;

/// <summary>
/// Provides methods for logging messages with different Severity levels.
/// </summary>
public static class Log
{
  /// <summary>
  /// Can be:
  /// <br />
  /// - all: Log all messages
  /// <br />
  /// - off: Disable logging
  /// <br />
  /// - error: Log only errors
  /// </summary>
  public static short Severity { get; set; } = Defaults.SEVERITY;

  /// <summary>
  /// Logs an error message to the console with red foreground color.
  /// </summary>
  /// <param name="message">The error message to log.</param>
  public static void Error(string message)
  {
    if (Severity == ((short)Defaults.Severity.Off))
      return;

    Console.WriteLine($"{Ansi.Color.Background.Red}{Ansi.Color.Foreground.BrightWhite}ERR {Ansi.Color.Reset} {message}");
  }

  /// <summary>
  /// Logs a collection of error messages to the console with red foreground color.
  /// </summary>
  /// <param name="messages">The collection of error messages to log.</param>
  public static void Error(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || Severity == ((short)Defaults.Severity.Off))
      return;

    Console.Write($"{Ansi.Color.Background.Red}{Ansi.Color.Foreground.BrightWhite}ERR {Ansi.Color.Reset} ");

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }
  }

  /// <summary>
  /// Logs a warning message to the console with yellow foreground color.
  /// </summary>
  /// <param name="message">The warning message to log.</param>
  public static void Warning(string message)
  {
    if (Severity != ((short)Defaults.Severity.All))
      return;

    Console.WriteLine($"{Ansi.Color.Background.Yellow}{Ansi.Color.Foreground.BrightWhite}WARN {Ansi.Color.Reset} {message}");
  }

  /// <summary>
  /// Logs a collection of warning messages to the console with yellow foreground color.
  /// </summary>
  /// <param name="messages">The collection of warning messages to log.</param>
  public static void Warning(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || Severity != ((short)Defaults.Severity.All))
      return;

    Console.Write($"{Ansi.Color.Background.Yellow}{Ansi.Color.Foreground.BrightWhite}WARN {Ansi.Color.Reset} ");

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }
  }

  /// <summary>
  /// Logs an information message to the console.
  /// </summary>
  /// <param name="message">The information message to log.</param>
  public static void Info(string message)
  {
    if (Severity != ((short)Defaults.Severity.All))
      return;

    Console.WriteLine($"{Ansi.Color.Foreground.Black}{Ansi.Color.Background.BrightCyan} LOG {Ansi.Color.Reset} {message}");
  }

  /// <summary>
  /// Logs a collection of information messages to the console.
  /// </summary>
  /// <param name="messages">The collection of information messages to log.</param>
  public static void Info(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || Severity != ((short)Defaults.Severity.All))
      return;

    Console.Write($"{Ansi.Color.Foreground.Black}{Ansi.Color.Background.BrightCyan} LOG {Ansi.Color.Reset} ");

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }
  }

  /// <summary>
  /// Logs a success message to the console with green foreground color.
  /// </summary>
  /// <param name="message">The success message to log.</param>
  public static void Success(string message)
  {
    if (Severity != ((short)Defaults.Severity.All))
      return;

    Console.WriteLine($"{Ansi.Color.Background.Green}{Ansi.Color.Foreground.BrightWhite}OK {Ansi.Color.Reset} {message}");
  }

  /// <summary>
  /// Logs a collection of success messages to the console with green foreground color.
  /// </summary>
  /// <param name="messages">The collection of success messages to log.</param>
  public static void Success(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || Severity != ((short)Defaults.Severity.All))
      return;

    Console.Write($"{Ansi.Color.Background.Green}{Ansi.Color.Foreground.BrightWhite}OK {Ansi.Color.Reset} ");

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }
  }

  /// <summary>
  /// Ignore severity settings and always log message.
  /// </summary>
  /// <param name="message">The message to log.</param>
  public static void Override(string message)
  {
    Console.WriteLine(message);
  }
}
