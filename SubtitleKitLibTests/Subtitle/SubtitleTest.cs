using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SubtitleKitLibTests.Subtitle
{
    public class SubtitleTest
    {
        [Fact]
        public void Cloned_Subtitle_MatchesOriginal()
        {
            var subtitle = SubtitleContainer.GetValidSubtitle();
            var clone = (ISubtitle)subtitle.Clone();

            Assert.True(subtitle.Items.SequenceEqual(clone.Items));
        }

        [Fact]
        public void Cloned_Subtitle_IsNotReference()
        {
            var subtitle = SubtitleContainer.GetValidSubtitle();
            var clone = (ISubtitle)subtitle.Clone();

            Assert.False(subtitle.Items.Equals(clone.Items));
        }

        [Fact]
        public void Subtitle_ToString_MatchesSource()
        {
            var subtitle = SubtitleContainer.GetValidSubtitle();
            var subtitleCreator = new SubtitleCreator();

            var subtitleString = subtitle.ToString();
            var reconstructedSubtitle = subtitleCreator.CreateFromString(subtitleString);

            for(int i = 0; i < subtitle.Items.Count; i++)
            {
                Assert.Equal(subtitle.Items[i].ToString(), reconstructedSubtitle.Items[i].ToString());
            }
        }
    }
}
