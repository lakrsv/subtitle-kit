using SubtitleKitLib.Actions;
using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SubtitleKitLibTests.Subtitle
{
    public class SubtitleTimeOffsetActionTest
    {
        [Fact]
        public void Subtitle_IsOffset()
        {
            var subtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var offset = TimeSpan.FromSeconds(1);
            var timeOffsetAction = new SubtitleTimeOffsetAction(offset, subtitle);

            var originalStartTimes = subtitle.Items.Select(s => s.StartTime).ToArray();
            var originalEndTimes = subtitle.Items.Select(s => s.EndTime).ToArray();

            timeOffsetAction.PerformAction();

            var newStartTimes = subtitle.Items.Select(s => s.StartTime).ToArray();
            var newEndTimes = subtitle.Items.Select(s => s.EndTime).ToArray();

            for(int i = 0; i < originalStartTimes.Length; i++)
            {
                var originalTime = originalStartTimes[i];
                var newTime = newStartTimes[i];

                Assert.True(newTime == originalTime + offset.TotalMilliseconds);
            }
            for(int i = 0; i < originalEndTimes.Length; i++)
            {
                var originalTime = originalEndTimes[i];
                var newTime = newEndTimes[i];

                Assert.True(newTime == originalTime + offset.TotalMilliseconds);
            }
        }

        [Fact]
        public void Subtitle_IsOffset2()
        {
            var subtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var offset = TimeSpan.FromSeconds(-1);
            var timeOffsetAction = new SubtitleTimeOffsetAction(offset, subtitle);

            var originalStartTimes = subtitle.Items.Select(s => s.StartTime).ToArray();
            var originalEndTimes = subtitle.Items.Select(s => s.EndTime).ToArray();

            timeOffsetAction.PerformAction();

            var newStartTimes = subtitle.Items.Select(s => s.StartTime).ToArray();
            var newEndTimes = subtitle.Items.Select(s => s.EndTime).ToArray();

            for (int i = 0; i < originalStartTimes.Length; i++)
            {
                var originalTime = originalStartTimes[i];
                var newTime = newStartTimes[i];

                Assert.True(newTime == originalTime + offset.TotalMilliseconds);
            }
            for (int i = 0; i < originalEndTimes.Length; i++)
            {
                var originalTime = originalEndTimes[i];
                var newTime = newEndTimes[i];

                Assert.True(newTime == originalTime + offset.TotalMilliseconds);
            }
        }

        [Fact]
        public void Subtitle_IsOriginal_WhenActionUndone()
        {
            var subtitle = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var offset = TimeSpan.FromSeconds(1);
            var timeOffsetAction = new SubtitleTimeOffsetAction(offset, subtitle);

            var originalStartTimes = subtitle.Items.Select(s => s.StartTime).ToArray();
            var originalEndTimes = subtitle.Items.Select(s => s.EndTime).ToArray();

            timeOffsetAction.PerformAction();
            timeOffsetAction.UndoAction();

            var newStartTimes = subtitle.Items.Select(s => s.StartTime).ToArray();
            var newEndTimes = subtitle.Items.Select(s => s.EndTime).ToArray();

            for (int i = 0; i < originalStartTimes.Length; i++)
            {
                var originalTime = originalStartTimes[i];
                var newTime = newStartTimes[i];

                Assert.True(newTime == originalTime);
            }
            for (int i = 0; i < originalEndTimes.Length; i++)
            {
                var originalTime = originalEndTimes[i];
                var newTime = newEndTimes[i];

                Assert.True(newTime == originalTime);
            }
        }
    }
}
