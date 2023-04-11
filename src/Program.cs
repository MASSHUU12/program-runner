using Spectre.Console.Cli;

namespace Program
{
  class Program
  {
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    static void Main(string[] args)
    {
      var app = new CommandApp();
      app.Configure(config =>
      {
        config.AddCommand<RunCommand>("run")
              .WithAlias("r")
              .WithDescription("Run programs form the list.").
              WithExample(new[] { "run", "./path/to/the/list.json", "--log", "all" });
      });
      app.Run(args);
    }
  }
}
