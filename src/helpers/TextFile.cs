using System.Text.Json;

namespace TextFile
{
  public class ListData
  {
    public string Title { get; set; }
    public List<ListProgram> Programs { get; set; }

    public ListData()
    {
      Title = "";
      Programs = new List<ListProgram>();
    }
  }

  public class ListProgram
  {
    public string? Name { get; set; }
    public string Path { get; set; }
    public List<string> Args { get; set; }

    public ListProgram()
    {
      Path = "";
      Args = new List<string>();
    }
  }

  public static class JsonHelper
  {
    public static List<ListData>? Load(string filePath)
    {
      if (!File.Exists(filePath))
        return null;

      // Read the entire contents of the file at the specified path and store it in a string variable called json
      string json = File.ReadAllText(filePath);

      try
      {
        // Deserialize the JSON string into a ListData object
        List<ListData>? data = JsonSerializer.Deserialize<List<ListData>>(json);

        // Return the deserialized ListData object
        return data;
      }
      catch (JsonException)
      {
        return null;
      }
    }
  }
}
