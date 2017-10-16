namespace SubtitleKitLib.Actions
{
    using System;

    public interface IAction
    {
        void PerformAction(Action onCompleted);

        void UndoAction(Action onCompleted);
    }
}