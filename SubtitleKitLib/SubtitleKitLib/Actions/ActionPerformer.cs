using System;
using System.Collections.Generic;
using System.Text;
using SubtitleKitLib.Actions;
using System.Diagnostics.Contracts;

namespace SubtitleKitLib.Actions
{
    public class ActionPerformer : IActionPerformer
    {
        private readonly Stack<IAction> _performedActions = new Stack<IAction>();

        public void PerformAction(IAction action)
        {
            if(action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.PerformAction();
            _performedActions.Push(action);
        }

        public IAction UndoPreviousAction()
        {
            if (_performedActions.Count > 0)
            {
                var action = _performedActions.Pop();
                action.UndoAction();
                return action;
            }

            return null;
        }
    }
}
