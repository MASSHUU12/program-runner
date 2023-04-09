using TextFile;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class Runner
{

  public static void Prepare(string filePath, string listName)
  {
    List<ListData>? data = JsonHelper.Load(filePath);

    if (data == null || data.Count() <= 0)
      return;

    Log.Info(
      string.Format(
        "Running programs from a list \"{0}\" from a file \"{1}\".",
        listName,
        filePath
      )
    );

    ListData? listData = FindList(listName, data);

    if (listData == null)
    {
      Log.Error(
        string.Format(
          "The list named \"{0}\" was not found in the file \"{1}\".",
          listName,
          filePath
        )
      );
      return;
    }

    foreach (ListProgram program in listData.Programs)
    {
      Log.Info(
        string.Format(
          "Trying to run a program \"{0}\" with arguments \"{1}\".",
          program.Name ?? "undefined",
          program.Args
        )
      );

      if (!RunProgram(program.Path, program.Args))
        if (!RunCommand(program.Path, program.Args))
        {
          Log.Error("An attempt to run the program failed.\n");
          continue;
        }

      Log.Success("Program successfully launched.\n");
    }
  }

  public static bool RunCommand(string command, string arguments)
  {
    Process process = new Process();
    ProcessStartInfo startInfo = new ProcessStartInfo();

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

    startInfo.UseShellExecute = false;
    startInfo.CreateNoWindow = true;
    process.StartInfo = startInfo;

    try
    {
      process.Start();
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public static bool RunProgram(string programPath, string arguments)
  {
    string? program = programPath;

    // If file doesn't exists in specified path
    // or the caller does not have sufficient permissions,
    // try to find the program in PATH
    if (!File.Exists(program))
      program = FindProgramInPATH(program);

    // If program still doesn't exists, return false
    if (program == null)
      return false;

    ProcessStartInfo startInfo = new ProcessStartInfo(program);
    startInfo.Arguments = arguments;

    try
    {
      Process.Start(startInfo);
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

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
