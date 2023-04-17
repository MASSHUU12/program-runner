using System.Text.Json;

namespace TextFile
{
  /// <summary>
  /// Represents a list of programs.
  /// </summary>
  public class ListData
  {
    /// <summary>
    /// The title of the list.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The programs in the list.
    /// </summary>
    public List<ListProgram> Programs { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListData"/> class.
    /// </summary>
    public ListData()
    {
      Title = string.Empty;
      Programs = new List<ListProgram>();
    }
  }

  /// <summary>
  /// Represents a program in a list.
  /// </summary>
  public class ListProgram
  {
    /// <summary>
    /// The name of the program.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The path (or command) to the program.
    /// </summary>
    public string Run { get; set; }

    /// <summary>
    /// The arguments to pass to the program.
    /// </summary>
    public string? Args { get; set; }

    public bool? Elevated { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProgram"/> class.
    /// </summary>
    public ListProgram()
    {
      Name = string.Empty;
      Run = string.Empty;
      Args = string.Empty;
      Elevated = false;
    }
  }

  /// <summary>
  /// Provides helper methods for working with JSON data.
  /// </summary>
  public static class JsonHelper
  {
    /// <summary>
    /// Loads a JSON file into a list of <see cref="ListData"/> objects.
    /// </summary>
    /// <param name="filePath">The path to the JSON file.</param>
    /// <returns>A list of <see cref="ListData"/> objects if the file exists and is valid; otherwise, null.</returns>
    public static List<ListData> Load(string filePath)
    {
      if (!File.Exists(filePath))
        throw new FileNotFoundException(Constants.Messages.FILE_DOESNT_EXISTS);

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
            // Define a function to check if the "Elevated" property exists in the current JSON element.
            Func<bool> IsElevated = () =>
            {
              try
              {
                return programElement.GetProperty("Elevated").GetBoolean();
              }
              catch (KeyNotFoundException)
              {
                return false;
              }
            };

            Func<string, string> TryGetProperty = (string property) =>
            {
              try
              {
                return programElement.GetProperty(property).GetString() ?? string.Empty;
              }
              catch (KeyNotFoundException)
              {
                return "";
              }
            };

            ListProgram program = new ListProgram
            {
              Name = TryGetProperty("Name"),
              Run = TryGetProperty("Run"),
              Args = TryGetProperty("Args"),
              Elevated = IsElevated()
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
        throw new FileLoadException(Constants.Messages.READING_FILE_FAILED);
      }
    }
  }
}
