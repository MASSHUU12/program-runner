using Spectre.Console.Cli;

namespace Program
{
  class Program
  {
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

      // Func<string> checkListName = () =>
      // {
      //   if (args.Length >= 2)
      //     return args[1];

      //   Log.Warning("No list was passed to run, running the default \"main\".");
      //   return "main";
      // };

      // Runner.Prepare(args[0], checkListName());
    }
  }
}
