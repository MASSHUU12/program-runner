using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Env
{
  /// <summary>
  /// A static class that provides methods for working with the PATH environment variable.
  /// </summary>
  public static class PATH
  {
    /// <summary>
    /// Finds the full path to a program
    /// by looking for it in the directories listed in the PATH environment variable.
    /// </summary>
    /// <param name="program">The name of the program to search for.</param>
    /// <returns>The full path to the program if found; otherwise, null.</returns>
    public static string? Find(string program)
    {
      string[]? paths = GetPaths();

      if (paths == null)
        return null;

      // Loop through each path and look if the program exists.
      foreach (string path in paths)
      {
        string fullPath = Path.Combine(path, program);

        if (File.Exists(fullPath))
          return fullPath;
      }

      // Program is not found.
      return null;
    }

    /// <summary>
    /// Adds a path to a program to the PATH environment variable.
    /// </summary>
    /// <param name="pathToTheProgram">The full path to the program to add.</param>
    public static void Add(string pathToTheProgram)
    {
      string? path = GetPath();

      // If the paths are not available, log an error and return.
      if (path == null)
      {
        Log.Error("No access to PATH");
        return;
      }

      // Append the new path to the paths array.
      path += Path.PathSeparator + pathToTheProgram;

      // Check the operating system platform to determine how to set the PATH environment variable.
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        // On Windows, use the Environment.SetEnvironmentVariable method.
        Environment.SetEnvironmentVariable(
          "PATH",
          pathToTheProgram,
          EnvironmentVariableTarget.User
        );
      else
      {
        // On Unix-like systems, use a bash command to set the PATH environment variable.
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "/bin/bash";
        startInfo.Arguments = "-c \"export PATH='"
          + path
          + "'; exec \"$SHELL\"\"";
        startInfo.UseShellExecute = false;
        Process.Start(startInfo);
      }

      Log.Info("The program should now be available through PATH, if not, try restarting your terminal.");
    }

    /// <summary>
    /// Retrieves the paths as string from the PATH environment variable.
    /// </summary>
    private static string? GetPath()
    {
      string? path = Environment.GetEnvironmentVariable("PATH");

      // If the PATH environment variable is not set or is empty, return null.
      if (string.IsNullOrEmpty(path))
        return null;

      // Split the PATH env variable into separate path and return it.
      return path;
    }

    /// <summary>
    /// Retrieves the paths as array from the PATH environment variable.
    /// </summary>
    private static string[]? GetPaths()
    {
      string? path = GetPath();

      // If the PATH environment variable is not set or is empty, return null.
      if (string.IsNullOrEmpty(path))
        return null;

      // Split the PATH env variable into separate path and return it.
      return path.Split(Path.PathSeparator);
    }
  }
}
