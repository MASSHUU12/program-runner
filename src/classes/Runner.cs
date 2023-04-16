using TextFile;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Constants;

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
    public PrepareProps(string? FilePath, string? ListName, bool? GlobalElevated)
    {
      this.FilePath = FilePath ?? string.Empty;
      this.ListName = ListName ?? string.Empty;
      this.GlobalElevated = GlobalElevated ?? false;
    }
  }


  /// <summary>
  /// Prepares and runs programs from a list.
  /// </summary>
  /// <param name="props">The properties for preparing and running the programs.</param>
  public static void Prepare(PrepareProps props)
  {
    List<ListData>? data = JsonHelper.Load(props.FilePath);

    // If the list is empty or not found, return.
    if (data == null || data.Count() <= 0)
      return;

    Log.Info(Messages.RunningFromList(props.ListName, props.FilePath));

    // Find the list data for the specified list name.
    ListData? listData = FindList(props.ListName, data);

    // If the list data is not found, log an error and return.
    if (listData == null)
    {
      Log.Error(Messages.ListNotFound(props.ListName, props.FilePath));
      return;
    }

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
    Process process = new Process();
    ProcessStartInfo startInfo = new ProcessStartInfo();

    // Set the start info for the command line based on the operating system.
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      // On Windows, use the "cmd" command with the "/c" argument to run the command.
      startInfo.FileName = "cmd";
      startInfo.Arguments = $"/c {command} {arguments}";

      // If elevated privileges are requested, set the "Verb" property of the start info to "runas".
      if (elevated)
        startInfo.Verb = "runas";
    }
    else
    {
      // On macOS and Linux, use "/bin/bash" to run the command,
      // or "sudo" if elevated privileges are requested.
      startInfo.FileName = elevated ? "sudo" : "/bin/bash";
      startInfo.Arguments = $"-c \"{command}\" {arguments}";
    }

    // Configure the start info for the process.
    startInfo.UseShellExecute = elevated ? true : false;
    startInfo.CreateNoWindow = true;
    process.StartInfo = startInfo;

    try
    {
      // Start the process and return true if it is successfully launched.
      process.Start();
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
    string? program = programPath;

    // If file doesn't exists in specified path
    // or the caller does not have sufficient permissions,
    // try to find the program in PATH.
    if (!File.Exists(program))
      program = FindProgramInPATH(program);

    // If program still doesn't exists, return false
    if (program == null)
      return false;

    ProcessStartInfo startInfo = new ProcessStartInfo(program);
    startInfo.Arguments = arguments;
    startInfo.UseShellExecute = elevated ? true : false;

    // If elevated is true, configure the start info to run the program with elevated privileges.
    if (elevated)
      // If running on Windows, use the "runas" verb to elevate the process.
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        startInfo.Verb = "runas";
      // If running on Linux or macOS, use sudo to elevate the process.
      else
        startInfo.FileName = "sudo";

    try
    {
      // Start the process with the specified start info.
      Process.Start(startInfo);
    }
    catch (Exception)
    {
      // Return false if there was an error starting the process.
      return false;
    }

    return true;
  }

  /// <summary>
  /// Finds the full path to a program
  /// by looking for it in the directories listed in the PATH environment variable.
  /// </summary>
  /// <param name="program">The name of the program to search for.</param>
  /// <returns>The full path to the program if found; otherwise, null.</returns>
  private static string? FindProgramInPATH(string program)
  {
    char pathSeparators = Path.PathSeparator;
    string? PATH = Environment.GetEnvironmentVariable("PATH");

    if (PATH == null)
      return null;

    // Split the PATH env variable into separate path
    string[] paths = PATH.Split(pathSeparators);

    // Loop through each path and look if the program exists
    foreach (string path in paths)
    {
      string fullPath = Path.Combine(path, program);

      if (File.Exists(fullPath))
        return fullPath;
    }

    // If the program is not found return null
    return null;
  }

  /// <summary>
  /// Searches for a specific ListData in a List of ListData objects by comparing their titles.
  /// </summary>
  /// <param name="listTitle">The title of the ListData to search for.</param>
  /// <param name="data">The List of ListData objects to search in.</param>
  /// <returns>The first ListData object with a matching title, or null if no match is found.</returns>
  private static ListData? FindList(string listTitle, List<ListData> data)
  {
    foreach (ListData listData in data)
    {
      if (listData.Title == listTitle)
        return listData;
    }

    return null;
  }
}
