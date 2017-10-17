// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Action.cs" author="Lars-Kristian Svenoy">
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software Foundation,
//  Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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

            if (_timeOffset != 0)
            {
                Console.WriteLine("Offsetting your subtitle by " + _timeOffset + " seconds");

                var timeOffsetAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(_timeOffset), subtitle);
                timeOffsetAction.PerformAction();
            }

            if (!string.IsNullOrEmpty(_language))
            {
                var culture = new CultureInfo(_language);
                var translatorAction = new SubtitleTranslatorAction(subtitle, culture);

                var reset = new ManualResetEvent(false);

                Console.WriteLine("Translating your subtitle to " + culture.EnglishName);
                Console.WriteLine("Please wait...");

                translatorAction.PerformAction(
                    () =>
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
            var subtitleString = subtitle.ToString();

            using (var writer = File.CreateText(outputDestination))
            {
                writer.Write(subtitleString);
            }
        }
    }
}