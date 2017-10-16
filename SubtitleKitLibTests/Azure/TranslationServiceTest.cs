namespace SubtitleKitLibTests.Azure
{
    using System;
    using System.Globalization;

    using SubtitleKitLib.Azure;

    using SubtitleKitLibTests.Subtitle;

    using Xunit;

    public class TranslationServiceTest
    {
        [Fact]
        public async void Translation_WithNullAuthKey_ThrowsArgumentNullExceptionAsync()
        {
            var validSubtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidShortSubtitleName);

            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                    {
                        await TranslationService.TranslateArrayAsync(
                            validSubtitle.Items.ToArray(),
                            CultureInfo.CurrentCulture,
                            null);
                    });
        }
    }
}