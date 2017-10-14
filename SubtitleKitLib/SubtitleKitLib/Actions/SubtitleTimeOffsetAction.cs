﻿using SubtitleKitLib.Subtitle;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Actions
{
    public class SubtitleTimeOffsetAction : SubtitleAction
    {
        private TimeSpan _offset;

        public SubtitleTimeOffsetAction(TimeSpan offset, ISubtitle subtitle) : base(subtitle)
        {
            _offset = offset;
        }

        public override void PerformAction()
        {
            foreach(var item in originalSubtitle.Items)
            {
                item.StartTime += _offset.Milliseconds;
                item.EndTime += _offset.Milliseconds;
            }
        }
    }
}
