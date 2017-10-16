namespace SubtitleKitLib.Actions
{
    using System;
    using System.Collections.Generic;

    public class ActionPerformer : IActionPerformer
    {
        private readonly Stack<IAction> _performedActions = new Stack<IAction>();

        public void PerformAction(IAction action, Action onCompleted = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.PerformAction(onCompleted);
            this._performedActions.Push(action);
        }

        public IAction UndoPreviousAction(Action onCompleted = null)
        {
            if (this._performedActions.Count > 0)
            {
                var action = this._performedActions.Pop();
                action.UndoAction(onCompleted);
                return action;
            }

            return null;
        }
    }
}