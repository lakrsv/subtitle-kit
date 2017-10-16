namespace SubtitleKitLibCLI
{
    class Program
    {
        // TODO - Clean this up. This is for testing only now.
        static void Main(string[] args)
        {
            // Usage: -f "file" -l "Language" -o "output"
            // Usage: -f "file" -t seconds -o "output"
            args = new[]
                       {
                           "-f",
                           @"D:\Documents\Visual Studio Projects\SubtitleKit\SubtitleKitLibCLI\bin\Debug\netcoreapp2.0\lalaland.srt",
                           "-l", "pl", "-o",
                           @"D:\Documents\Visual Studio Projects\SubtitleKit\SubtitleKitLibCLI\bin\Debug\netcoreapp2.0\lalalando.srt"
                       };

            var action = new Action();
            for (int i = 0; i < args.Length; i += 2)
            {
                string actionParam = args[i];
                string argumentParam = args[i + 1];

                action.AddParameter(actionParam, argumentParam);
            }

            action.PerformAction();
        }
    }
}