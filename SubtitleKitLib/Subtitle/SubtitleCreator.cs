// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleCreator.cs" author="Lars-Kristian Svenoy">
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software Foundation,
//  Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SubtitleKitLib.Subtitle
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    using SubtitleParsers.Classes.Parsers;

    public class SubtitleCreator : ISubtitleCreator
    {
        public ISubtitle CreateFromFile(FileStream fileStream)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            ISubtitle subtitle;

            try
            {
                subtitle = GetSubtitleFromStream(fileStream.Name, fileStream);
            }
            catch (FormatException e)
            {
                throw new FormatException(string.Format("Subtitle File {0} is invalid", fileStream.Name), e);
            }

            return subtitle;
        }

        public ISubtitle CreateFromString(string subtitleContent)
        {
            if (string.IsNullOrEmpty(subtitleContent)) throw new ArgumentNullException(nameof(subtitleContent));

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(subtitleContent)))
            {
                ISubtitle subtitle;

                try
                {
                    subtitle = GetSubtitleFromStream(null, stream);
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
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            if (!uri.IsWellFormedOriginalString()) throw new FormatException(nameof(uri) + " is not a well formed uri");

            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            var webResponse = (HttpWebResponse)webRequest.GetResponse();

            ISubtitle subtitle;

            try
            {
                subtitle = GetSubtitleFromStream(uri.AbsolutePath, webResponse.GetResponseStream());
            }
            catch (FormatException e)
            {
                throw new FormatException(string.Format("Subtitle File {0} is invalid", uri.AbsolutePath), e);
            }

            return subtitle;
        }

        private ISubtitle GetSubtitleFromStream(string filePath, Stream stream)
        {
            Subtitle subtitle;
            var parser = new SubParser();

            try
            {
                var items = parser.ParseStream(stream);
                subtitle = new Subtitle(filePath, items);
            }
            catch (ArgumentException e)
            {
                throw new FormatException(e.Message, e);
            }

            return subtitle;
        }
    }
}