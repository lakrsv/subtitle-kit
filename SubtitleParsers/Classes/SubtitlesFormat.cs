namespace SubtitleParsers.Classes
{
    using System.Collections.Generic;

    public class SubtitlesFormat
    {
        public static readonly SubtitlesFormat MicroDvdFormat =
            new SubtitlesFormat { Name = "MicroDvd", Extension = @"\.sub" };

        // Predefined instances -------------------------------
        public static readonly SubtitlesFormat SubRipFormat =
            new SubtitlesFormat { Name = "SubRip", Extension = @"\.srt" };

        public static readonly SubtitlesFormat SubStationAlphaFormat =
            new SubtitlesFormat { Name = "SubStationAlpha", Extension = @"\.ssa" };

        public static readonly SubtitlesFormat SubViewerFormat =
            new SubtitlesFormat { Name = "SubViewer", Extension = @"\.sub" };

        public static readonly SubtitlesFormat TtmlFormat = new SubtitlesFormat { Name = "TTML", Extension = @"\.ttml" }
            ;

        public static readonly SubtitlesFormat WebVttFormat =
            new SubtitlesFormat { Name = "WebVTT", Extension = @"\.vtt" };

        public static readonly SubtitlesFormat YoutubeXmlFormat = new SubtitlesFormat
                                                                      {
                                                                          Name = "YoutubeXml"

                                                                          // Extension = @"\.*"
                                                                      };

        public static readonly List<SubtitlesFormat> SupportedSubtitlesFormats =
            new List<SubtitlesFormat>
                {
                    SubRipFormat,
                    MicroDvdFormat,
                    SubViewerFormat,
                    SubStationAlphaFormat,
                    TtmlFormat,
                    WebVttFormat,
                    YoutubeXmlFormat
                };

        // Private constructor to avoid duplicates ------------
        private SubtitlesFormat()
        {
        }

        public string Extension { get; set; }

        // Properties -----------------------------------------
        public string Name { get; set; }
    }
}