namespace SubtitleKitLib.Actions
{
    using System;

    using SubtitleKitLib.Subtitle;

    public abstract class SubtitleAction : IAction
    {
        private readonly ISubtitle _originalSubtitle;

        protected SubtitleAction(ISubtitle subtitle)
        {
            if (subtitle == null) throw new ArgumentNullException();

            _originalSubtitle = (ISubtitle)subtitle.Clone();
            Subtitle = subtitle;
        }

        public ISubtitle Subtitle { get; }

        public abstract void PerformAction(Action onCompleted);

        public virtual void UndoAction(Action onCompleted = null)
        {
            Subtitle.Set(_originalSubtitle);
            onCompleted?.Invoke();
        }
    }
}