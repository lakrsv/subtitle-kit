using System;
using System.Collections.Generic;

namespace SubtitleKitLib.Subtitle
{
    public interface ISubtitle : ICloneable
    {
        string FilePath { get; }

        List<SubtitlesParser.Classes.SubtitleItem> Items { get; }

        string ToString();
    }
}
