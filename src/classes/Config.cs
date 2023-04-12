public static class Config
{
  private static Dictionary<string, Dictionary<string, string>> config = new Dictionary<string, Dictionary<string, string>>();

  public static Dictionary<string, Dictionary<string, string>> Data
  {
    get { return config; }
  }

  public static void Init(string path)
  {
    config = Load(path);

    Console.WriteLine(config["general"]["logLevel"]);
  }

  private static void Validate()
  {

  }

  private static Dictionary<string, Dictionary<string, string>> Load(string path)
  {
    // If file does not exists, throw an error.
    if (!File.Exists(path))
      throw new FileNotFoundException($"File {path} does not exists");

    // Dictionary to store the values.
    Dictionary<string, Dictionary<string, string>> sections = new Dictionary<string, Dictionary<string, string>>();

    // Read the lines from the file.
    string[] lines = File.ReadAllLines(path);

    // Loop through each line and parse the values.
    string section = string.Empty;
    foreach (string line in lines)
    {
      // Ignore comments and blank lines.
      if (line.StartsWith(";") || string.IsNullOrWhiteSpace(line))
        continue;

      // Check if line is a section header.
      if (line.StartsWith("[") && line.EndsWith("]"))
      {
        section = line.Substring(1, line.Length - 2);

        // If section already exists, throw an error.
        if (sections.ContainsKey(section))
          throw new InvalidDataException($"Duplicate section: {section}");

        sections.Add(section, new Dictionary<string, string>());
        continue;
      }

      // Split the line into key and value.
      string[] parts = line.Split("=", 2, StringSplitOptions.RemoveEmptyEntries);

      // If the line does not contain a valid key-value pair, throw an error.
      if (parts.Length != 2)
        throw new InvalidDataException($"Invalid line in section {section}: {line}");

      // Add the key-value pair to the dictionary.
      string key = parts[0].Trim();
      string value = parts[1].Trim();

      // If the key is blank, throw an error.
      if (string.IsNullOrWhiteSpace(key))
        throw new InvalidDataException($"Blank key in section: {section}");

      // If the key already exists, throw an error.
      if (sections[section].ContainsKey(key))
        throw new InvalidDataException($"Duplicate key in section {section}: {key}");

      sections[section].Add(key, value);
    }
    return sections;
  }
}
