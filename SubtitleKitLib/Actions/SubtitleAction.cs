namespace SubtitleKitLib.Actions
{
    using System;

    using SubtitleKitLib.Subtitle;

    public abstract class SubtitleAction : IAction
    {
        private ISubtitle _originalSubtitle;

        protected SubtitleAction(ISubtitle subtitle)
        {
            if (subtitle == null)
            {
                throw new ArgumentNullException();
            }

            this._originalSubtitle = (ISubtitle)subtitle.Clone();
            this.Subtitle = subtitle;
        }

        public ISubtitle Subtitle { get; private set; }

        public abstract void PerformAction(Action onCompleted = null);

        public virtual void UndoAction(Action onCompleted = null)
        {
            this.Subtitle.Set(this._originalSubtitle);
            onCompleted?.Invoke();
        }
    }
}