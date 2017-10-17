// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Subtitle.cs" author="Lars-Kristian Svenoy">
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

namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SubtitleParsers.Classes;

    public class Subtitle : ISubtitle
    {
        internal Subtitle(string filePath, List<SubtitleItem> subtitleItems)
        {
            FilePath = filePath;
            Items = subtitleItems;
        }

        public string FilePath { get; private set; }

        public List<SubtitleItem> Items { get; private set; }

        public object Clone()
        {
            return new Subtitle(
                FilePath,
                Items.ConvertAll(
                    x => new SubtitleItem { StartTime = x.StartTime, EndTime = x.EndTime, Lines = x.Lines.ToList() }));
        }

        public void Set(ISubtitle subtitle)
        {
            FilePath = subtitle.FilePath;
            Items = subtitle.Items;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            var lineCount = 1;
            foreach (var item in Items)
            {
                builder.Append(lineCount.ToString());
                builder.Append(Environment.NewLine);
                builder.Append(item);
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);

                lineCount++;
            }

            return builder.ToString();
        }
    }
}