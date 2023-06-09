using TextFile;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Constants;
using Env;

/// <summary>
/// This class provides methods to prepare and run programs from a list.
/// </summary>
public static class Runner
{
  /// <summary>
  /// A class that contains properties for preparing and running programs from a list.
  /// </summary>
  public class PrepareProps
  {
    /// <summary>
    /// The file path of the JSON file that contains the list of programs to run.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// The name of the list to run.
    /// </summary>
    public string ListName { get; }

    /// <summary>
    /// A value indicating whether to run the programs with elevated privileges.
    /// </summary>
    public bool GlobalElevated { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrepareProps"/> class with the specified properties.
    /// </summary>
    /// <param name="FilePath">The file path of the JSON file that contains the list of programs to run.</param>
    /// <param name="ListName">The name of the list to run.</param>
    /// <param name="GlobalElevated">A value indicating whether to run the programs with elevated privileges.</param>
    public PrepareProps(string FilePath, string ListName, bool GlobalElevated)
    {
      this.FilePath = FilePath;
      this.ListName = ListName;
      this.GlobalElevated = GlobalElevated;
    }
  }


  /// <summary>
  /// Prepares and runs programs from a list.
  /// </summary>
  /// <param name="props">The properties for preparing and running the programs.</param>
  public static void Prepare(PrepareProps props)
  {
    List<ListData> data = JsonHelper.Load(props.FilePath);

    // If the list is empty, return.
    if (data.Count() <= 0)
      return;

    Log.Info(Messages.RunningFromList(props.ListName, props.FilePath));

    // Find the list data for the specified list name.
    ListData listData = FindList(props.ListName, data);

    // Run each program in the list data.
    foreach (ListProgram program in listData.Programs)
    {
      bool elevated = false;

      if (props.GlobalElevated || program.Elevated == true)
        elevated = true;

      Log.Info(Messages.TryingToRun(program.Name, program.Args));

      // Try to run the program using the specified path and arguments.
      if (!RunProgram(program.Run, program.Args ?? string.Empty, elevated))
        // If the program cannot be run using the specified path and arguments,
        // try to run it using the command line
        if (!RunCommand(program.Run, program.Args ?? string.Empty, elevated))
        {
          // If the program still cannot be run, log an error and continue
          Log.Error(Messages.RunningFailed(program.Name));
          continue;
        }
      Log.Success(Messages.RunningSucceeded(program.Name));
    };
  }

  /// <summary>
  /// Runs a command using the command line.
  /// </summary>
  /// <param name="command">The command to run.</param>
  /// <param name="arguments">The arguments to pass to the command.</param>
  /// <param name="elevated">Specifies whether to run the command with elevated privileges (e.g., as an administrator on Windows).</param>
  /// <returns>True if the command is successfully launched, otherwise false.</returns>
  public static bool RunCommand(string command, string arguments, bool elevated)
  {
    try
    {
      // Start the process and return true if it is successfully launched.
      using Process? process = Process.Start(CreateStartInfo(arguments, elevated, command: command));
    }
    catch (Exception)
    {
      return false;
    }
    return true;
  }

  /// <summary>
  /// Runs a program with the specified path and arguments.
  /// </summary>
  /// <param name="programPath">The path of the program to run.</param>
  /// <param name="arguments">The arguments to pass to the program.</param>
  /// <param name="elevated">Whether to run the program with elevated privileges.</param>
  /// <returns>True if the program was successfully started, false otherwise.</returns>
  public static bool RunProgram(string programPath, string arguments, bool elevated)
  {
    string program = programPath;

    // If file doesn't exists in specified path
    // or the caller does not have sufficient permissions,
    // try to find the program in PATH.
    if (!File.Exists(program))
      program = PATH.Find(program) ?? string.Empty;

    // If program still doesn't exists, return false
    if (string.IsNullOrEmpty(program))
      return false;

    try
    {
      // Create start info & run the process.
      using Process? process = Process.Start(CreateStartInfo(arguments, elevated, program));
    }
    catch (Exception)
    {
      // Return false if there was an error starting the process.
      return false;
    }

    return true;
  }

  /// <summary>
  /// Creates a <see cref="ProcessStartInfo"/> object with the specified arguments and options.
  /// </summary>
  /// <param name="arguments">The arguments to pass to the process.</param>
  /// <param name="elevated">Whether to run the process with elevated privileges.</param>
  /// <param name="program">The program to run.</param>
  /// <param name="command">The command to run.</param>
  /// <returns>A <see cref="ProcessStartInfo"/> object with the specified options.</returns>
  public static ProcessStartInfo CreateStartInfo(string arguments, bool elevated, string program = "", string command = "")
  {
    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.FileName = string.IsNullOrEmpty(command) ? program : command;
    startInfo.Arguments = arguments;
    startInfo.UseShellExecute = elevated;
    startInfo.CreateNoWindow = true;

    // If elevated is true, configure the start info to run the program with elevated privileges.
    if (elevated)
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        // If running on Windows, use the "runas" verb to elevate the process.
        startInfo.Verb = "runas";
      else
        // If running on Linux or macOS, use sudo to elevate the process.
        startInfo.FileName = "sudo";

    // Set the start info for the command line based on the operating system.
    if (command != string.Empty && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      // On Windows, use the "cmd" command with the "/c" argument to run the command.
      startInfo.FileName = "cmd";
      startInfo.Arguments = $"/c {command} {arguments}";

      // If elevated privileges are requested, set the "Verb" property of the start info to "runas".
      if (elevated)
        startInfo.Verb = "runas";
    }
    else if (command != string.Empty)
    {
      // On macOS and Linux, use "/bin/bash" to run the command,
      // or "sudo" if elevated privileges are requested.
      startInfo.FileName = elevated ? "sudo" : "/bin/bash";
      startInfo.Arguments = $"-c \"{command}\" {arguments}";
    }

    return startInfo;
  }

  /// <summary>
  /// Searches for a specific ListData in a List of ListData objects by comparing their titles.
  /// </summary>
  /// <param name="listTitle">The title of the ListData to search for.</param>
  /// <param name="data">The List of ListData objects to search in.</param>
  /// <returns>The first ListData object with a matching title, or null if no match is found.</returns>
  private static ListData FindList(string listTitle, List<ListData> data)
  {
    foreach (ListData listData in data)
    {
      if (listData.Title == listTitle)
        return listData;
    }

    // If the list data is not found, throw an error.
    throw new KeyNotFoundException(Messages.ListNotFound(listTitle));
  }
}
