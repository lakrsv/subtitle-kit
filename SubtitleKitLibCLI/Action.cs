namespace SubtitleKitLibCLI
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;

    using SubtitleKitLib.Actions;
    using SubtitleKitLib.Subtitle;

    public class Action
    {
        private string _destination;

        private string _language;

        private string _sourceFile;

        private int _timeOffset;

        public bool AddParameter(string action, string argument)
        {
            switch (action)
            {
                case "-f":
                    this._sourceFile = argument;
                    break;
                case "-t":
                    this._timeOffset = int.Parse(argument);
                    break;
                case "-o":
                    this._destination = argument;
                    break;
                case "-l":
                    this._language = argument;
                    break;
                default:
                    return false;
            }
            return false;
        }

        public void PerformAction()
        {
            var subtitleCreator = new SubtitleCreator();
            var subtitle = subtitleCreator.CreateFromFile(File.OpenRead(this._sourceFile));

            if (this._timeOffset != 0)
            {
                Console.WriteLine("Offsetting your subtitle by " + this._timeOffset + " seconds");

                var timeOffsetAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(this._timeOffset), subtitle);
                timeOffsetAction.PerformAction();
            }

            if (!string.IsNullOrEmpty(this._language))
            {
                var culture = new CultureInfo(this._language);
                var translatorAction = new SubtitleTranslatorAction(subtitle, culture);

                var reset = new ManualResetEvent(false);

                Console.WriteLine("Translating your subtitle to " + culture.EnglishName);
                Console.WriteLine("Please wait...");

                translatorAction.PerformAction(
                    () =>
                        {
                            this.SaveSubtitleToOutput(subtitle, this._destination);
                            reset.Set();
                        });

                reset.WaitOne();
            }
            else
            {
                this.SaveSubtitleToOutput(subtitle, this._destination);
            }
        }

        private void SaveSubtitleToOutput(ISubtitle subtitle, string outputDestination)
        {
            string subtitleString = subtitle.ToString();

            using (var writer = File.CreateText(this._destination))
            {
                writer.Write(subtitleString);
            }
        }
    }
}