using SubtitleKitLibTests.Subtitle;
using System.IO;
using Xunit;

namespace SubtitleKitLibTests.Subtitle
{
    public class SubtitleCreatorTest
    {
        [Fact]
        public void Subtitle_Created_WithValidSub_IsNotNull()
        {
            var subtitle = SubtitleContainer.GetValidSubtitle();

            Assert.NotNull(subtitle);
        }
    }
}
