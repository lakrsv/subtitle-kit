using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubtitleKitLib.Subtitle
{
    public interface ISubtitle : ICloneable
    {
        string FilePath { get; }

        List<SubtitlesParser.Classes.SubtitleItem> Items { get; }

        string ToString();
    }
}
