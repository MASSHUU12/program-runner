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
}
