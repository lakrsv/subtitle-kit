using System;
using System.IO;

namespace SubtitleKitLib.Subtitle
{
    interface ISubtitleCreator
    {
        ISubtitle CreateFromFile(FileStream fileStream);

        ISubtitle CreateFromUri(Uri uri);

        ISubtitle CreateFromString(string subtitleContent);
    }
}
