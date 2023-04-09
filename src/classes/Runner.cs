using TextFile;

public static class Runner
{

  public static void Run(string filePath, string listName)
  {
    List<ListData>? data = JsonHelper.Load(filePath);

    if (data == null || data.Count() <= 0)
      return;

    Log.Info(
      string.Format(
        "Running programs from a list \"{0}\" from a file: {1}",
        listName,
        filePath
      )
    );

    foreach (ListProgram program in data[0].Programs)
    {
      Console.WriteLine(program.Name + " " + program.Path);
    }
  }

  private static void ValidateList()
  {

  }
}
