/// <summary>
/// A static class that provides methods for prompting the user for input.
/// </summary>
public static class Prompt
{
  /// <summary>
  /// Prompts the user for a true or false response to the given message.
  /// </summary>
  /// <param name="message">The message to display to the user.</param>
  /// <returns>
  /// If the user enters 'true', returns true.
  /// If the user enters 'false', returns false.
  /// If the user enters any other input, prompts the user again until valid input is received.
  /// </returns>
  public static bool Bool(string message)
  {
    bool? input;

    while (true)
    {
      Log.Override($"{message} [True/False]");

      // Attempts to convert the user's input to a boolean value.
      input = Converter.ToBool(GetInput() ?? string.Empty);

      // If the input could not be converted to a boolean value,
      // prompts the user again until valid input is received.
      if (input == null)
      {
        Log.Error("Incorrect data was entered. Acceptable options are True/False.");
        continue;
      }
      break;
    }

    return (bool)input;
  }

  /// <summary>
  /// Prompts the user for a yes or no response to the given message.
  /// </summary>
  /// <param name="message">The message to display to the user.</param>
  /// <returns>
  /// If the user enters 'Y' or 'y', returns true.
  /// If the user enters 'N' or 'n', returns false.
  /// If the user enters any other input, prompts the user again until valid input is received.
  /// </returns>
  public static bool YesNo(string message)
  {
    bool? input;

    while (true)
    {
      Log.Override($"{message} [Y/N]");

      // Attempts to convert the user's input to a boolean value.
      input = Converter.YesNoToBool(GetInput() ?? string.Empty);

      // If the input could not be converted to a boolean value,
      // prompts the user again until valid input is received.
      if (input == null)
      {
        Log.Error("Incorrect data was entered. Acceptable options are Y/N.");
        continue;
      }
      break;
    }

    return (bool)input;
  }

  /// <summary>
  /// Reads a line of text entered by the user from the console.
  /// </summary>
  /// <returns>
  /// If the user enters null or an empty string, returns null.
  /// Otherwise, returns the trimmed input string.
  /// </returns>
  private static string? GetInput()
  {
    // Calls the Log class to set the console output to prompt the user for input.
    Log.Override("> ");

    // Reads a line of text entered by the user from the console.
    string? input = Console.In.ReadLine();
    string trimmed;

    // If the input is null or an empty string, return null.
    if (string.IsNullOrEmpty(input))
      return null;

    // Trim any leading or trailing white spaces from the input.
    trimmed = input.Trim();

    // If the trimmed input is null or an empty string, return null.
    if (string.IsNullOrEmpty(trimmed))
      return null;

    return trimmed;
  }
}
