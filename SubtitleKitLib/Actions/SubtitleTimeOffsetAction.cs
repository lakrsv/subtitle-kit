// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleTimeOffsetAction.cs" author="Lars-Kristian Svenoy">
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software Foundation,
//  Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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