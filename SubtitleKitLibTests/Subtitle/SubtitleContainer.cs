// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleContainer.cs" author="Lars-Kristian Svenoy">
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

namespace SubtitleKitLibTests.Subtitle
{
    using System.IO;

    using SubtitleKitLib.Subtitle;

    public static class SubtitleContainer
    {
        public static string InvalidSubtitleName = "badsubtitle.srt";

        public static string InvalidSubtitleName2 = "badsubtitle2.srt";

        public static string SubtitleFolderPath = @"Content/SubtitlesFiles/";

        public static string ValidShortSubtitleName = "shortsub.srt";

        public static string ValidSubtitleName = "Annie Hall - Eng 25fps.srt";

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