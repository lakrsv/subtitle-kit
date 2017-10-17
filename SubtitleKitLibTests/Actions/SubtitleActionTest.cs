// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleActionTest.cs" author="Lars-Kristian Svenoy">
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
                // ReSharper disable once ObjectCreationAsStatement
                () => { new SubtitleTimeOffsetAction(TimeSpan.FromSeconds(1), null); });
        }
    }
}