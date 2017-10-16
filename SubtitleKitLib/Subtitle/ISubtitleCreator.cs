namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.IO;

    interface ISubtitleCreator
    {
        ISubtitle CreateFromFile(FileStream fileStream);

        ISubtitle CreateFromString(string subtitleContent);

        ISubtitle CreateFromUri(Uri uri);
    }
}