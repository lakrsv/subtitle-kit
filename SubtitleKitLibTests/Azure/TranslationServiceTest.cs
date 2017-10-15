using SubtitleKitLib.Azure;
using SubtitleKitLibTests.Subtitle;
using SubtitlesParser.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;
namespace SubtitleKitLibTests.Azure
{
    public class TranslationServiceTest
    {
        [Fact]
        public async void Translation_WithNullAuthKey_ThrowsArgumentNullExceptionAsync()
        {
            var validSubtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await TranslationService.TranslateArrayAsync(validSubtitle.Items.ToArray(), CultureInfo.CurrentCulture, null);
            });
        }
    }
}
