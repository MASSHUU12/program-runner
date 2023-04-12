using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public class RunCommand : Command<RunCommand.Settings>
{
  public class Settings : CommandSettings
  {
    [CommandArgument(0, "<lists>")]
    public string? List { get; set; }

    [CommandArgument(1, "[listName]")]
    public string? ListName { get; set; }

    [CommandOption("-l|--log")]
    [DefaultValue("all")]
    public string? Log { get; set; }
  }

  public override ValidationResult Validate([NotNull] CommandContext context, [NotNull] Settings settings)
  {
    if (!File.Exists(settings.List))
      return ValidationResult.Error($"File does not exists: {settings.List}");

    if (!new[] { "all", "off", "error" }.Contains(settings.Log))
      return ValidationResult.Error(
        $"Log option does not accept: {settings.Log}\nPossible values are: all, off, error."
        );

    return base.Validate(context, settings);
  }

  public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
  {
    // Set log level
    Log.severity = settings.Log ?? "main";

    Func<string> checkListName = () =>
    {
      if (settings.ListName != null)
        return settings.ListName;

      Log.Warning("No list was passed to run, running the default \"main\".");
      return "main";
    };

    Runner.Prepare(settings.List ?? string.Empty, checkListName());

    return 0;
  }
}
