namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.Collections.Generic;

    using SubtitleParsers.Classes;

    public interface ISubtitle : ICloneable
    {
        string FilePath { get; }

        List<SubtitleItem> Items { get; }

        void Set(ISubtitle subtitle);

        string ToString();
    }
}