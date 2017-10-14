
using System;

namespace SubtitleKitLib.Actions
{
    public interface IAction
    {
        void PerformAction(Action onCompleted);

        void UndoAction(Action onCompleted);
    }
}
