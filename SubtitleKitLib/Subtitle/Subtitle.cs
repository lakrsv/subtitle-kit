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
            this.FilePath = filePath;
            this.Items = subtitleItems;
        }

        public string FilePath { get; private set; }

        public List<SubtitleItem> Items { get; private set; }

        public object Clone()
        {
            return new Subtitle(
                this.FilePath,
                this.Items.ConvertAll(
                    x => new SubtitlesParser.Classes.SubtitleItem()
                             {
                                 StartTime = x.StartTime,
                                 EndTime = x.EndTime,
                                 Lines = x.Lines.ToList()
                             }));
        }

        public void Set(ISubtitle subtitle)
        {
            this.FilePath = subtitle.FilePath;
            this.Items = subtitle.Items;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            int lineCount = 1;
            foreach (var item in this.Items)
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