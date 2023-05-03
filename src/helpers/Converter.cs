/// <summary>
/// A static class that provides methods for converting values.
/// </summary>
public static class Converter
{
  /// <summary>
  /// Attempts to parse the input string as a boolean value.
  /// </summary>
  /// <param name="str">The input string to parse.</param>
  /// <returns>
  /// If the parse operation is successful, returns the boolean value that corresponds to the input string.
  /// If the parse operation is not successful, returns a null value.
  /// </returns>
  public static bool? ToBool(string str)
  {
    // Attempts to parse the input string as a boolean value.
    if (Boolean.TryParse(str, out bool result))
      return result;
    // If the parse operation is not successful, returns a null value.
    return null;
  }

  /// <summary>
  /// Converts a string representation of yes or no to a nullable boolean value.
  /// </summary>
  /// <param name="str">The string to convert.</param>
  /// <returns>
  /// If the string is "yes" or "y", returns true.
  /// If the string is "no" or "n", returns false.
  /// If the string is any other value, returns null.
  /// </returns>
  public static bool? YesNoToBool(string str)
  {
    string lower = str.ToLower();

    switch (lower)
    {
      case "yes":
      case "y":
        return true;

      case "no":
      case "n":
        return false;
      default:
        return null;
    }
  }
}
