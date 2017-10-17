// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionPerformerTest.cs" author="Lars-Kristian Svenoy">
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

    using NSubstitute;

    using SubtitleKitLib.Actions;

    using Xunit;

    public class ActionPerformerTest
    {
        [Fact]
        public void Performer_PerformNullAction_ThrowsArgumentNullException()
        {
            var actionPerformer = new ActionPerformer();

            Assert.Throws<ArgumentNullException>(() => actionPerformer.PerformAction(null));
        }

        [Fact]
        public void Performer_UndoAction_IsOriginal()
        {
            var action = Substitute.For<IAction>();
            var actionPerformer = new ActionPerformer();

            actionPerformer.PerformAction(action);

            Assert.Equal(action, actionPerformer.UndoPreviousAction());
        }

        [Fact]
        public void Performer_UndoAction_WithNoAction_IsNull()
        {
            var action = Substitute.For<IAction>();
            var actionPerformer = new ActionPerformer();

            actionPerformer.PerformAction(action);
            actionPerformer.UndoPreviousAction();

            Assert.Null(actionPerformer.UndoPreviousAction());
        }
    }
}