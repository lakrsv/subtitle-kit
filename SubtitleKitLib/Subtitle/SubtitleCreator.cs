namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    using SubtitlesParser.Classes.Parsers;

    public class SubtitleCreator : ISubtitleCreator
    {
        public ISubtitle CreateFromFile(FileStream fileStream)
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            ISubtitle subtitle = null;

            try
            {
                subtitle = this.GetSubtitleFromStream(fileStream.Name, fileStream);
            }
            catch (FormatException e)
            {
                throw new FormatException(string.Format("Subtitle File {0} is invalid", fileStream.Name), e);
            }

            return subtitle;
        }

        public ISubtitle CreateFromString(string subtitleContent)
        {
            if (string.IsNullOrEmpty(subtitleContent))
            {
                throw new ArgumentNullException(nameof(subtitleContent));
            }

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(subtitleContent)))
            {
                ISubtitle subtitle = null;

                try
                {
                    subtitle = this.GetSubtitleFromStream(null, stream);
                }
                catch (FormatException e)
                {
                    throw new FormatException("Subtitle String is invalid", e);
                }

                return subtitle;
            }
        }

        public ISubtitle CreateFromUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (!uri.IsWellFormedOriginalString())
            {
                throw new FormatException(nameof(uri) + " is not a well formed uri");
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            ISubtitle subtitle = null;

            try
            {
                subtitle = this.GetSubtitleFromStream(uri.AbsolutePath, webResponse.GetResponseStream());
            }
            catch (FormatException e)
            {
                throw new FormatException(string.Format("Subtitle File {0} is invalid", uri.AbsolutePath), e);
            }

            return subtitle;
        }

        private ISubtitle GetSubtitleFromStream(string filePath, Stream stream)
        {
            Subtitle subtitle = null;
            var parser = new SubParser();

            try
            {
                var items = parser.ParseStream(stream);
                subtitle = new Subtitle(filePath, items);
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (ArgumentException e)
            {
                throw new FormatException(e.Message, e);
            }

            return subtitle;
        }
    }
}