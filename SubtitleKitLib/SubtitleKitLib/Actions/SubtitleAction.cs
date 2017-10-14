using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Actions
{
    public abstract class SubtitleAction : IAction
    {
        protected ISubtitle originalSubtitle;

        public ISubtitle Subtitle { get; private set; }

        protected SubtitleAction(ISubtitle subtitle)
        {
            originalSubtitle = (ISubtitle)subtitle.Clone();
            Subtitle = subtitle;
        }

        public abstract void PerformAction();

        public virtual void UndoAction()
        {
            Subtitle = originalSubtitle;
        }
    }
}
