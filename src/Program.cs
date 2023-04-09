namespace Program
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 1)
      {
        Log.Error("You need to provide path to the file as an argument.");
        return;
      }

      Func<string> checkListName = () =>
      {
        if (args.Length >= 2)
          return args[1];

        Log.Warning("No list was passed to run, running the default \"main\".");
        return "main";
      };

      Runner.Prepare(args[0], checkListName());
    }
  }
}
