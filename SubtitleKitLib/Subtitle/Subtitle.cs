namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SubtitlesParser.Classes;

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