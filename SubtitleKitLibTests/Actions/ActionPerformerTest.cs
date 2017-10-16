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
            IAction action = null;
            var actionPerformer = new ActionPerformer();

            Assert.Throws<ArgumentNullException>(() => actionPerformer.PerformAction(action));
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