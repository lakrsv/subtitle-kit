// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleTimeOffsetActionTest.cs" author="Lars-Kristian Svenoy">
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

    using SubtitleKitLib.Actions;

    using Xunit;

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

            for (var i = 0; i < originalStartTimes.Length; i++)
            {
                var originalTime = originalStartTimes[i];
                var newTime = newStartTimes[i];

                Assert.True(newTime == originalTime + (int)offset.TotalMilliseconds);
            }

            for (var i = 0; i < originalEndTimes.Length; i++)
            {
                var originalTime = originalEndTimes[i];
                var newTime = newEndTimes[i];

                Assert.True(newTime == originalTime + (int)offset.TotalMilliseconds);
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

            for (var i = 0; i < originalStartTimes.Length; i++)
            {
                var originalTime = originalStartTimes[i];
                var newTime = newStartTimes[i];

                Assert.True(newTime == originalTime + (int)offset.TotalMilliseconds);
            }

            for (var i = 0; i < originalEndTimes.Length; i++)
            {
                var originalTime = originalEndTimes[i];
                var newTime = newEndTimes[i];

                Assert.True(newTime == originalTime + (int)offset.TotalMilliseconds);
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

            for (var i = 0; i < originalStartTimes.Length; i++)
            {
                var originalTime = originalStartTimes[i];
                var newTime = newStartTimes[i];

                Assert.True(newTime == originalTime);
            }

            for (var i = 0; i < originalEndTimes.Length; i++)
            {
                var originalTime = originalEndTimes[i];
                var newTime = newEndTimes[i];

                Assert.True(newTime == originalTime);
            }
        }
    }
}