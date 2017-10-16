namespace SubtitleKitLib.Actions
{
    using System;

    public interface IActionPerformer
    {
        void PerformAction(IAction action, Action onCompleted);

        IAction UndoPreviousAction(Action onCompleted);
    }
}