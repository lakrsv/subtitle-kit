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