using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubtitleKitLib.Subtitle
{
    public class Subtitle : ISubtitle
    {
        public string FilePath { get; }
        public List<SubtitlesParser.Classes.SubtitleItem> Items { get; private set; }

        internal Subtitle(string filePath, List<SubtitlesParser.Classes.SubtitleItem> subtitleItems)
        {
            FilePath = filePath;
            Items = subtitleItems;
        }

        public object Clone()
        {
            return new Subtitle(FilePath, Items.ConvertAll(x => new SubtitlesParser.Classes.SubtitleItem()
            {
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Lines = x.Lines.ToList()
            }));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            int lineCount = 1;
            foreach(var item in Items)
            {
                builder.Append(lineCount.ToString());
                builder.Append(Environment.NewLine);
                builder.Append(item.ToString());
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);

                lineCount++;
            }

            return builder.ToString();
        }
    }
}
