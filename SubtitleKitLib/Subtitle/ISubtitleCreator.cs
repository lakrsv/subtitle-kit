namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.IO;

    internal interface ISubtitleCreator
    {
        ISubtitle CreateFromFile(FileStream fileStream);

        ISubtitle CreateFromString(string subtitleContent);

        ISubtitle CreateFromUri(Uri uri);
    }
}