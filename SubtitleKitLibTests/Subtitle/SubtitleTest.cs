// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleTest.cs" author="Lars-Kristian Svenoy">
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
    using System;
    using System.Linq;

    using SubtitleKitLib.Subtitle;

    using Xunit;

    public class SubtitleTest
    {
        [Fact]
        public void Cloned_Subtitle_IsNotReference()
        {
            var subtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var clone = (ISubtitle)subtitle.Clone();

            Assert.False(subtitle.Items.Equals(clone.Items));
        }

        [Fact]
        public void Cloned_Subtitle_MatchesOriginal()
        {
            var subtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var clone = (ISubtitle)subtitle.Clone();
            var matchesOriginal = !subtitle.Items.Where(
                                      (t, i) => !string.Equals(
                                                    t.ToString(),
                                                    clone.Items[i].ToString(),
                                                    StringComparison.Ordinal)).Any();

            Assert.True(matchesOriginal);
        }

        [Fact]
        public void Subtitle_ToString_MatchesSource()
        {
            var subtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var subtitleCreator = new SubtitleCreator();

            var subtitleString = subtitle.ToString();
            var reconstructedSubtitle = subtitleCreator.CreateFromString(subtitleString);

            for (var i = 0; i < subtitle.Items.Count; i++)
                Assert.Equal(subtitle.Items[i].ToString(), reconstructedSubtitle.Items[i].ToString());
        }
    }
}