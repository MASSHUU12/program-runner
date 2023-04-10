/// <summary>
/// Provides methods for logging messages with different severity levels.
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
  public static string severity = "all";

  /// <summary>
  /// Logs an error message to the console with red foreground color.
  /// </summary>
  /// <param name="message">The error message to log.</param>
  public static void Error(string message)
  {
    if (severity == "off")
      return;

    Console.WriteLine($"{Ansi.Color.Foreground.Red}{message}{Ansi.Color.Reset}");
  }

  /// <summary>
  /// Logs a collection of error messages to the console with red foreground color.
  /// </summary>
  /// <param name="messages">The collection of error messages to log.</param>
  public static void Error(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || severity == "off")
      return;

    Console.Write(Ansi.Color.Foreground.Red);

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }

    Console.Write(Ansi.Color.Reset);
  }

  /// <summary>
  /// Logs a warning message to the console with yellow foreground color.
  /// </summary>
  /// <param name="message">The warning message to log.</param>
  public static void Warning(string message)
  {
    if (severity != "all")
      return;

    Console.WriteLine($"{Ansi.Color.Foreground.Yellow}{message}{Ansi.Color.Reset}");
  }

  /// <summary>
  /// Logs a collection of warning messages to the console with yellow foreground color.
  /// </summary>
  /// <param name="messages">The collection of warning messages to log.</param>
  public static void Warning(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || severity != "all")
      return;

    Console.Write(Ansi.Color.Foreground.Yellow);

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }

    Console.Write(Ansi.Color.Reset);
  }

  /// <summary>
  /// Logs an information message to the console.
  /// </summary>
  /// <param name="message">The information message to log.</param>
  public static void Info(string message)
  {
    if (severity != "all")
      return;

    Console.WriteLine(message);
  }

  /// <summary>
  /// Logs a collection of information messages to the console.
  /// </summary>
  /// <param name="messages">The collection of information messages to log.</param>
  public static void Info(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || severity != "all")
      return;

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
    if (severity != "all")
      return;

    Console.WriteLine($"{Ansi.Color.Foreground.Green}{message}{Ansi.Color.Reset}");
  }

  /// <summary>
  /// Logs a collection of success messages to the console with green foreground color.
  /// </summary>
  /// <param name="messages">The collection of success messages to log.</param>
  public static void Success(IEnumerable<string> messages)
  {
    if (messages.Count() <= 0 || severity != "all")
      return;

    Console.Write(Ansi.Color.Foreground.Green);

    foreach (string message in messages)
    {
      Console.WriteLine(message);
    }

    Console.Write(Ansi.Color.Reset);
  }
}
