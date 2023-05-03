namespace Env
{
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

    public static void Add(string program)
    {
      return;
    }

    private static string[]? GetPaths()
    {
      string? path = Environment.GetEnvironmentVariable("PATH");

      if (string.IsNullOrEmpty(path))
        return null;

      // Split the PATH env variable into separate path and return it.
      return path.Split(Path.PathSeparator);
    }
  }
}
