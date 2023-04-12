using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Constants;

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
      return ValidationResult.Error($"{Messages.FILE_DOESNT_EXISTS}: {settings.List}");

    if (!Defaults.SEVERITY_VALUES.Contains(settings.Log))
      return ValidationResult.Error(Messages.LogDoesntAccept(settings.Log));

    return base.Validate(context, settings);
  }

  public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
  {
    // Set log level
    Log.severity = settings.Log ?? Defaults.SEVERITY;

    Func<string> checkListName = () =>
    {
      if (settings.ListName != null)
        return settings.ListName;

      Log.Warning(Messages.NO_LIST_RUNNING_DEFAULT);
      return Defaults.LIST_NAME;
    };

    Runner.Prepare(settings.List ?? string.Empty, checkListName());

    return 0;
  }
}
