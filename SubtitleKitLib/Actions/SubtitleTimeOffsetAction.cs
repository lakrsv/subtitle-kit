namespace SubtitleKitLib.Actions
{
    using System;

    using SubtitleKitLib.Subtitle;

    public class SubtitleTimeOffsetAction : SubtitleAction
    {
        private TimeSpan _offset;

        public SubtitleTimeOffsetAction(TimeSpan offset, ISubtitle subtitle)
            : base(subtitle)
        {
            this._offset = offset;
        }

        public override void PerformAction(Action onCompleted = null)
        {
            foreach (var item in this.Subtitle.Items)
            {
                item.StartTime += (int)this._offset.TotalMilliseconds;
                item.EndTime += (int)this._offset.TotalMilliseconds;
            }

            onCompleted?.Invoke();
        }
    }
}