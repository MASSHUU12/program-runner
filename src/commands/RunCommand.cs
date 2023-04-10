using Spectre.Console.Cli;
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
    public string? Log { get; set; }
  }

  public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
  {
    Console.WriteLine($"{settings.List} {settings.ListName ?? "main"} {settings.Log}");

    return 0;
  }
}
