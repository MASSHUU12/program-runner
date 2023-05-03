using Spectre.Console.Cli;

public class AddToPathCommand : Command
{
  public override int Execute(CommandContext context)
  {
    bool input = Prompt.YesNo("\nYou are going to add this program to the PATH.\nThis option was built with cross-platform operation in mind, but was only 'tested' on Windows.\n\nAre you sure you want to perform this operation?");

    if (input)
      Env.PATH.Add(Environment.CurrentDirectory);

    return 0;
  }
}
