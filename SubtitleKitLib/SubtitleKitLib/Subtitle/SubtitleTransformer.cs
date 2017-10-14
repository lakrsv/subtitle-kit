using System;
using System.Collections.Generic;
using System.Text;
using SubtitleKitLib.Actions;
using System.Diagnostics.Contracts;

namespace SubtitleKitLib.Subtitle
{
    public class SubtitleTransformer : ISubtitleTransformer
    {
        private readonly Stack<IAction> _performedActions = new Stack<IAction>();

        public void PerformAction(IAction action, Subtitle subtitle)
        {
            if(action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if(subtitle == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.PerformAction();
            _performedActions.Push(action);
        }

        public IAction UndoPreviousAction(Subtitle subtitle)
        {
            if(subtitle == null)
            {
                throw new ArgumentNullException(nameof(subtitle));
            }

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
