namespace SubtitleKitLibTests.Actions
{
    using System;

    using SubtitleKitLib.Actions;

    using SubtitleKitLibTests.Subtitle;

    using Xunit;

    public class SubtitleActionTest
    {
        [Fact]
        public void SubtitleAction_PerformAction_DoesCallback()
        {
            var validSubtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var subtitleAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), validSubtitle);
            var didCallback = false;

            subtitleAction.PerformAction(() => didCallback = true);

            Assert.True(didCallback);
        }

        [Fact]
        public void SubtitleAction_UndoAction_DoesCallback()
        {
            var validSubtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var subtitleAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), validSubtitle);
            var didCallback = false;

            subtitleAction.PerformAction();
            subtitleAction.UndoAction(() => didCallback = true);

            Assert.True(didCallback);
        }

        [Fact]
        public void SubtitleAction_WithNullSubtitle_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var subtitleAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), null);
                    });
        }
    }
}