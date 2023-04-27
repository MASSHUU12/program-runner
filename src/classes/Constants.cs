/// <summary>
/// Namespace containing classes with constant values.
/// </summary>
namespace Constants
{
  /// <summary>
  /// Class containing default values.
  /// </summary>
  public static class Defaults
  {
    /// <summary>
    /// Possible values for severity level.
    /// </summary>
    public enum Severity
    {
      All,
      Off,
      Error
    }

    /// <summary>
    /// Default severity level for logging.
    /// </summary>
    public const short SEVERITY = (short)Severity.All;

    /// <summary>
    /// Default name for a list.
    /// </summary>
    public const string LIST_NAME = "main";
  }

  /// <summary>
  /// Class containing messages for logging and output.
  /// </summary>
  public static class Messages
  {
    /// <summary>
    /// Message for when a file does not exist.
    /// </summary>
    public const string FILE_DOESNT_EXISTS = "File does not exists";

    /// <summary>
    /// Message for when no list is passed to run.
    /// </summary>
    public const string NO_LIST_RUNNING_DEFAULT =
                $"No list was passed to run, running the default \"{Defaults.LIST_NAME}\".";

    /// <summary>
    /// Message for when reading a file fails.
    /// </summary>
    public const string READING_FILE_FAILED =
                "Something went wrong when reading the contents of the file.";

    /// <summary>
    /// Message for when running programs from a list.
    /// </summary>
    public const string RUN_FROM_LIST = "Run programs from the list.";

    /// <summary>
    /// Message for when no programs were found in the list.
    /// </summary>
    public const string NO_PROGRAMS_FOUND = "No programs found to run in the list";

    /// <summary>
    /// Message for when a log option is not accepted.
    /// </summary>
    /// <param name="option">The log option that is not accepted.</param>
    /// <returns>A string describing that the log option is not accepted and the possible values for it.</returns>
    public static string LogDoesntAccept(string? option)
    {
      return $"Log option does not accept: {option}.\nPossible values are: {string.Join(
        ", ",
        string.Join(", ", Enum.GetValues(typeof(Defaults.Severity)).Cast<Defaults.Severity>().Select(x => x.ToString())))}";
    }

    /// <summary>
    /// Message for when running programs from a list.
    /// </summary>
    /// <param name="list">The name of the list.</param>
    /// <param name="file">The name of the file containing the list.</param>
    /// <returns>A string describing that programs are being run from a list.</returns>
    public static string RunningFromList(string? list, string? file)
    {
      return $"Running programs from a list \"{list}\" from a file \"{file}\".";
    }

    /// <summary>
    /// Message for when a list is not found.
    /// </summary>
    /// <param name="list">The name of the list that was not found.</param>
    /// <returns>A string describing that the list was not found in the file.</returns>
    public static string ListNotFound(string list)
    {
      return $"The list named \"{list}\" was not found.";
    }

    /// <summary>
    /// Message for when trying to run a program.
    /// </summary>
    /// <param name="name">The name of the program being run.</param>
    /// <param name="args">The arguments passed to the program.</param>
    /// <returns>A string describing that a program is being run with its name and arguments.</returns>
    public static string TryingToRun(string? name, string? args)
    {
      if (name == null || name == string.Empty)
        name = "undefined";
      if (args == null || args == string.Empty)
        return $"Trying to run a program \"{name}\" without arguments.";
      return $"Trying to run a program \"{name}\" with arguments \"{args}\".";
    }

    /// <summary>
    /// Message for when running a program fails.
    /// </summary>
    /// <param name="name">The name of the program that failed to run.</param>
    /// <returns>A string describing that running the program failed.</returns>
    public static string RunningFailed(string? name)
    {
      if (name == null || name == string.Empty)
        name = "undefined";
      return $"An attempt to run the {name} failed.";
    }

    /// <summary>
    /// Message for when running a program succeeds.
    /// </summary>
    /// <param name="name">The name of the program that was successfully run.</param>
    /// <returns>A string describing that running the program succeeded.</returns>
    public static string RunningSucceeded(string? name)
    {
      if (name == null || name == string.Empty)
        name = "undefined";
      return $"Program {name} successfully launched.";
    }

    /// <summary>
    /// Returns a string message indicating that a property with the specified name
    /// is not a string or boolean value.
    /// </summary>
    /// <param name="propertyName">The name of the property that is not a string or boolean value.</param>
    /// <returns>A string message indicating that a property with the specified name
    /// is not a string or boolean value.</returns>
    public static string PropertyNotStringOrBool(string propertyName)
    {
      return $"Property \"{propertyName}\" is not a string or boolean value.";
    }

    /// <summary>
    /// Returns a string message indicating that a required property with the specified name is missing.
    /// </summary>
    /// <param name="propertyName">The name of the missing property.</param>
    /// <returns>A string message indicating that a required property with the specified name is missing.</returns>
    public static string PropertyMissing(string propertyName)
    {
      return $"Property \"{propertyName}\" is missing.";
    }

    /// <summary>
    /// Returns a string message indicating that a non-optional property with the specified name is not present.
    /// </summary>
    /// <param name="propertyName">The name of the missing property.</param>
    /// <returns>A string message indicating that a non-optional property with the specified name is not present.</returns>
    public static string PropertyNotOptional(string propertyName)
    {
      return $"Property \"{propertyName}\" is not optional.";
    }
  }
}
