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
            _offset = offset;
        }

        // ReSharper disable once OptionalParameterHierarchyMismatch
        public override void PerformAction(Action onCompleted = null)
        {
            foreach (var item in Subtitle.Items)
            {
                item.StartTime += (int)_offset.TotalMilliseconds;
                item.EndTime += (int)_offset.TotalMilliseconds;
            }

            onCompleted?.Invoke();
        }
    }
}