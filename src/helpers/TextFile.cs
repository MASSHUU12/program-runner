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
    public string Args { get; set; }

    public ListProgram()
    {
      Path = "";
      Args = "";
    }
  }

  public static class JsonHelper
  {
    public static List<ListData>? Load(string filePath)
    {
      if (!File.Exists(filePath))
      {
        Log.Error("The specified file doesn't exist.");
        return null;
      }

      // Read the entire contents of the file at the specified path.
      string json = File.ReadAllText(filePath);

      try
      {
        // Parse the JSON string into a JsonDocument object.
        JsonDocument document = JsonDocument.Parse(json);

        // Create a empty list of ListData objects.
        List<ListData>? data = new List<ListData>();

        // Traverse the JsonDocument object and populate the ListData objects.
        foreach (JsonElement element in document.RootElement.EnumerateArray())
        {
          ListData listData = new ListData
          {
            Title = element.GetProperty("Title").GetString() ?? string.Empty
          };

          foreach (JsonElement programElement in element.GetProperty("Programs").EnumerateArray())
          {
            ListProgram program = new ListProgram
            {
              Name = programElement.GetProperty("Name").GetString(),
              Path = programElement.GetProperty("Path").GetString() ?? string.Empty,
              Args = programElement.GetProperty("Args").GetString() ?? string.Empty,
            };
            listData.Programs.Add(program);
          }
          data.Add(listData);
        }

        // Return the list of ListData objects
        return data;
      }
      catch (Exception)
      {
        Log.Error("Something went wrong when reading the contents of the file.");
        return null;
      }
    }
  }
}
