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
        public static string ValidShortSubtitleName = "shortsub.srt";
        public static string InvalidSubtitleName = "badsubtitle.srt";
        public static string InvalidSubtitleName2 = "badsubtitle2.srt";

        public static ISubtitle GetSubtitleFromFile(string fileName)
        {
            var subtitleCreator = new SubtitleCreator();

            var subtitlePath = SubtitleFolderPath + fileName;
            var subtitleStream = File.OpenRead(subtitlePath);
            var subtitle = subtitleCreator.CreateFromFile(subtitleStream);

            return subtitle;
        }
    }
}
