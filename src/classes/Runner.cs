using TextFile;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Spectre.Console;

/// <summary>
/// This class provides methods to prepare and run programs from a list.
/// </summary>
public static class Runner
{

  /// <summary>
  /// Prepares and runs programs from a list.
  /// </summary>
  /// <param name="filePath">The file path of the JSON file that contains the list of programs to run.</param>
  /// <param name="listName">The name of the list to run</param>
  public static void Prepare(string filePath, string listName)
  {
    List<ListData>? data = JsonHelper.Load(filePath);

    // If the list is empty or not found, return.
    if (data == null || data.Count() <= 0)
      return;

    AnsiConsole.Status()
      .Start("Thinking...", ctx =>
      {
        ctx.Spinner(Spinner.Known.Dqpb);
        ctx.SpinnerStyle(Style.Parse("green"));

        ctx.Status($"Running programs from a list \"{listName}\" from a file \"{filePath}\"");

        // Find the list data for the specified list name.
        ListData? listData = FindList(listName, data);

        // If the list data is not found, log an error and return.
        if (listData == null)
        {
          Log.Error($"The list named \"{listName}\" was not found in the file \"{filePath}\"");
          return;
        }

        // Run each program in the list data.
        foreach (ListProgram program in listData.Programs)
        {
          ctx.Status(
            $"Trying to run a program \"{program.Name ?? "undefined"}\" with arguments \"{program.Args}\""
          );

          // Try to run the program using the specified path and arguments.
          if (!RunProgram(program.Path, program.Args))
            // If the program cannot be run using the specified path and arguments,
            // try to run it using the command line
            if (!RunCommand(program.Path, program.Args))
            {
              // If the program still cannot be run, log an error and continue
              Log.Error($"An attempt to run the {program.Name} failed.");
              continue;
            }
          Log.Success($"Program {program.Name} successfully launched.");
        }
      });
  }

  /// <summary>
  /// Runs a command using the command line.
  /// </summary>
  /// <param name="command">The command to run.</param>
  /// <param name="arguments">The arguments to pass to the command.</param>
  /// <returns>True if the command is successfully launched, otherwise false.</returns>
  public static bool RunCommand(string command, string arguments)
  {
    Process process = new Process();
    ProcessStartInfo startInfo = new ProcessStartInfo();

    // Set the start info for the command line based on the operating system.
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      startInfo.FileName = "cmd";
      startInfo.Arguments = $"/c {command} {arguments}";
    }
    else
    {
      startInfo.FileName = "/bin/bash";
      startInfo.Arguments = $"-c \"{command}\" {arguments}";
    }

    // Configure the start info for the process.
    startInfo.UseShellExecute = false;
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
  /// <returns>True if the program was successfully started, false otherwise.</returns>
  public static bool RunProgram(string programPath, string arguments)
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
