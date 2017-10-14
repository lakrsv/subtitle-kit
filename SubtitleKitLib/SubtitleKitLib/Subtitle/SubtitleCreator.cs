using SubtitlesParser.Classes.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Text;

namespace SubtitleKitLib.Subtitle
{
    public class SubtitleCreator : ISubtitleCreator
    {
        public ISubtitle CreateFromFile(FileStream fileStream)
        {
            if(fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            var parser = new SubParser();
            var items = parser.ParseStream(fileStream);

            return (items != null && items.Count != 0) ? new Subtitle(fileStream.Name, items) : null;
        }

        public ISubtitle CreateFromUri(Uri uri)
        {
            if(uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (!uri.IsWellFormedOriginalString())
            {
                throw new FormatException(nameof(uri) + "is not a well formed uri");
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            var parser = new SubParser();
            var items = parser.ParseStream(webResponse.GetResponseStream());

            return (items != null && items.Count != 0) ? new Subtitle(uri.AbsolutePath, items) : null;
        }

        public ISubtitle CreateFromString(string subtitleContent)
        {
            if (string.IsNullOrEmpty(subtitleContent))
            {
                throw new ArgumentNullException(nameof(subtitleContent));
            }

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(subtitleContent)))
            {
                var subParser = new SubParser();
                var subtitle = new Subtitle(null, subParser.ParseStream(stream));

                return subtitle;
            }
        }
    }
}
