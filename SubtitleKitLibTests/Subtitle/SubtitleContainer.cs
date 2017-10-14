using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubtitleKitLibTests.Subtitle
{
    public static class SubtitleContainer
    {
        public static string SubtitleFolderPath = @"Content/SubtitlesFiles/";
        public static string ValidSubtitleName = "Annie Hall - Eng 25fps.srt";

        public static ISubtitle GetValidSubtitle()
        {
            var subtitleCreator = new SubtitleKitLib.Subtitle.SubtitleCreator();

            var subtitlePath = SubtitleFolderPath + ValidSubtitleName;
            var subtitleStream = File.OpenRead(subtitlePath);
            var subtitle = subtitleCreator.CreateFromFile(subtitleStream);

            return subtitle;
        }
    }
}
