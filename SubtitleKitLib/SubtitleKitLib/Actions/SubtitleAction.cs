using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Actions
{
    public abstract class SubtitleAction : IAction
    {
        private ISubtitle _originalSubtitle;

        public ISubtitle Subtitle { get; private set; }

        protected SubtitleAction(ISubtitle subtitle)
        {
            _originalSubtitle = (ISubtitle)subtitle.Clone();
            Subtitle = subtitle;
        }

        public abstract void PerformAction(Action onCompleted = null);

        public virtual void UndoAction(Action onCompleted = null)
        {
            Subtitle = _originalSubtitle;
            onCompleted?.Invoke();
        }
    }
}
