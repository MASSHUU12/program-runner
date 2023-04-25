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
      // Check that the file path is not null or empty.
      if (string.IsNullOrEmpty(filePath))
        throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

      // Check that the file exists.
      if (!File.Exists(filePath))
        throw new FileNotFoundException(Constants.Messages.FILE_DOESNT_EXISTS);

      // Read the entire contents of the file at the specified path.
      string json = File.ReadAllText(filePath);

      // Parse the JSON string into a JsonDocument object.
      using JsonDocument document = JsonDocument.Parse(json);

      // Create a empty list of ListData objects.
      List<ListData>? data = new List<ListData>();

      // Traverse the JsonDocument object and populate the ListData objects.
      foreach (JsonElement element in document.RootElement.EnumerateArray())
      {
        // Create a new ListData object and set its Title property
        // based on the value of the "Title" property in the current JSON element.
        ListData listData = new ListData
        {
          Title = Validate("Title", element)
        };

        // Traverse the "Programs" property of the current JSON element
        // and populate the ListProgram objects in the ListData object.
        foreach (JsonElement programElement in element.TryGetProperty("Programs",
          out JsonElement programsElement)
            ? programsElement.EnumerateArray()
            : throw new JsonException($"No programs found to run in the list \"{listData.Title}\"."))
        {
          // Create a new ListProgram object and set its Name, Run, Args, and Elevated properties
          // based on the values of the corresponding properties in the current JSON element.
          ListProgram program = new ListProgram
          {
            Name = Validate("Name", programElement),
            Run = Validate("Run", programElement, optional: false),
            Args = Validate("Args", programElement),
            Elevated = Boolean.TryParse(
              Validate("Elevated", programElement), out bool elevated
              ) ? elevated : false
          };
          listData.Programs.Add(program);
        }
        // Add the ListData object to the list of data.
        data.Add(listData);
      }

      // Return the list of ListData objects
      return data;
    }

    /// <summary>
    /// Validates a given property within a JSON element.
    /// If the property does not exist, and it is not optional, an exception is thrown.
    /// If the property is null, an exception is thrown.
    /// If the property is optional and does not exist, an empty string is returned.
    /// </summary>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="optional">Whether or not the property is optional.</param>
    /// <returns>The value of the property, or an empty string if the property is optional and does not exist.</returns>
    private static string Validate(string propertyName, JsonElement element, bool optional = true)
    {
      string property = string.Empty;

      // Try to get the JSON property with the specified name from the JSON element.
      if (element.TryGetProperty(propertyName, out JsonElement propertyElement))
        // If the property exists and has a non-null value, try to get its string value.
        if (propertyElement.ValueKind != JsonValueKind.Null)
        {
          try
          {
            // Try to get the property's string value.
            property = propertyElement.GetString()!;
          }
          catch (InvalidOperationException)
          {
            // If getting the property's string value fails, try to get its boolean value.
            try
            {
              // Try to get the property's boolean value and convert it to a string.
              bool boolValue = propertyElement.GetBoolean();
              property = boolValue.ToString();
            }
            catch (InvalidOperationException)
            {
              // If getting the property's boolean value also fails, throw an exception.
              throw new InvalidOperationException($"Property \"{propertyName}\" is not a string or boolean value.");
            }
          }
        }
        // If the property exists but has a null value, check if it's optional or not.
        else if (!optional)
          // If the property is not optional, throw an exception.
          throw new InvalidOperationException($"Property \"{propertyName}\" is missing.");

      // If the property is still empty and is not optional, throw an exception.
      if (property == string.Empty && !optional)
        throw new InvalidOperationException($"Property \"{propertyName}\" is not optional.");

      // Return the property value (which can be either a string or a boolean converted to a string).
      return property;
    }
  }
}
