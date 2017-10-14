using SubtitleKitLib.Actions;
using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;

namespace SubtitleKitLibTests.Actions
{
    public class ActionPerformerTest
    {
        [Fact]
        public void Performer_PerformNullAction_ThrowsArgumentNullException()
        {
            IAction action = null;
            var actionPerformer = new ActionPerformer();

            Assert.Throws<ArgumentNullException>(() => actionPerformer.PerformAction(action));
        }

        [Fact]
        public void Performer_UndoAction_IsOriginal()
        {
            IAction action = Substitute.For<IAction>();
            var actionPerformer = new ActionPerformer();

            actionPerformer.PerformAction(action);

            Assert.Equal(action, actionPerformer.UndoPreviousAction());
        }

        [Fact]
        public void Performer_UndoAction_WithNoAction_IsNull()
        {
            IAction action = Substitute.For<IAction>();
            var actionPerformer = new ActionPerformer();

            actionPerformer.PerformAction(action);
            actionPerformer.UndoPreviousAction();

            Assert.Null(actionPerformer.UndoPreviousAction());
        }
    }
}
