using System;
using System.Collections.Generic;

namespace SubtitleKitLib.Subtitle
{
    public interface ISubtitle : ICloneable
    {
        string FilePath { get; }

        List<SubtitlesParser.Classes.SubtitleItem> Items { get; }

        void Set(ISubtitle subtitle);

        string ToString();
    }
}
