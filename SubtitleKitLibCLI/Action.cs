using System;
using System.Collections.Generic;
using System.Text;
using SubtitleKitLib;
using SubtitleKitLib.Subtitle;
using System.IO;
using SubtitleKitLib.Actions;
using System.Globalization;
using System.Threading;

namespace SubtitleKitLibCLI
{
    public class Action
    {
        private string _sourceFile;

        private string _destination;

        private int _timeOffset;

        private string _language;

        public bool AddParameter(string action, string argument)
        {
            switch (action)
            {
                case "-f":
                    _sourceFile = argument;
                    break;
                case "-t":
                    _timeOffset = int.Parse(argument);
                    break;
                case "-o":
                    _destination = argument;
                    break;
                case "-l":
                    _language = argument;
                    break;
                default:
                    return false;
            }
            return false;
        }

        public void PerformAction()
        {
            var subtitleCreator = new SubtitleCreator();
            var subtitle = subtitleCreator.CreateFromFile(File.OpenRead(_sourceFile));

            if(_timeOffset != 0)
            {
                Console.WriteLine("Offsetting your subtitle by " + _timeOffset + " seconds");

                var timeOffsetAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(_timeOffset), subtitle);
                timeOffsetAction.PerformAction();
            }
            if(!string.IsNullOrEmpty(_language))
            {
                var culture = new CultureInfo(_language);
                var translatorAction = new SubtitleTranslatorAction(subtitle, culture);

                var reset = new ManualResetEvent(false);

                Console.WriteLine("Translating your subtitle to " + culture.EnglishName);
                Console.WriteLine("Please wait...");

                translatorAction.PerformAction(()=> 
                {
                    SaveSubtitleToOutput(subtitle, _destination);
                    reset.Set();
                });

                reset.WaitOne();
            }
            else
            {
                SaveSubtitleToOutput(subtitle, _destination);
            }
        }

        private void SaveSubtitleToOutput(ISubtitle subtitle, string outputDestination)
        {
            string subtitleString = subtitle.ToString();
            
            using(var writer = File.CreateText(_destination))
            {
                writer.Write(subtitleString);
            }
        }
    }
}