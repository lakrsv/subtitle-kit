using SubtitleKitLib.Actions;
using SubtitleKitLibTests.Subtitle;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SubtitleKitLibTests.Actions
{
    public class SubtitleActionTest
    {
        [Fact]
        public void SubtitleAction_PerformAction_DoesCallback()
        {
            var validSubtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var subtitleAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), validSubtitle);
            bool didCallback = false;

            subtitleAction.PerformAction(() => didCallback = true);

            Assert.True(didCallback);
        }

        [Fact]
        public void SubtitleAction_UndoAction_DoesCallback()
        {
            var validSubtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var subtitleAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), validSubtitle);
            bool didCallback = false;

            subtitleAction.PerformAction();
            subtitleAction.UndoAction(() => didCallback = true);

            Assert.True(didCallback);
        }

        [Fact]
        public void SubtitleAction_WithNullSubtitle_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var subtitleAction = new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), null);
            });
        }
    }
}
