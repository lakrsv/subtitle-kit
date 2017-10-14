using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubtitleKitLib.Subtitle
{
    interface ISubtitleCreator
    {
        ISubtitle CreateFromFile(FileStream fileStream);

        ISubtitle CreateFromUri(Uri uri);

        ISubtitle CreateFromString(string subtitleContent);
    }
}
