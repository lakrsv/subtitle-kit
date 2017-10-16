namespace SubtitleParsers.Classes
{
    using System;
    using System.Collections.Generic;

    public class SubtitleItem
    {
        // Constructors-----------------------------------------------------------------

        /// <summary>
        ///     The empty constructor
        /// </summary>
        public SubtitleItem()
        {
            Lines = new List<string>();
        }

        public int EndTime { get; set; }

        public List<string> Lines { get; set; }

        // Properties------------------------------------------------------------------

        // StartTime and EndTime times are in milliseconds
        public int StartTime { get; set; }

        // Methods --------------------------------------------------------------------------
        public override string ToString()
        {
            var startTs = new TimeSpan(0, 0, 0, 0, StartTime);
            var endTs = new TimeSpan(0, 0, 0, 0, EndTime);

            var res = string.Format(
                "{0} --> {1}:\n{2}",
                startTs.ToString(@"hh\:mm\:ss\,fff"),
                endTs.ToString(@"hh\:mm\:ss\,fff"),
                string.Join(Environment.NewLine, Lines));
            return res;
        }
    }
}