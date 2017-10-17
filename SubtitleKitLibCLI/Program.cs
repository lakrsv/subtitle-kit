// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" author="Lars-Kristian Svenoy">
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

namespace SubtitleKitLibCLI
{
    internal class Program
    {
        // TODO - Clean this up. This is for testing only now.
        private static void Main(string[] args)
        {
            /*
            // Usage: -f "file" -l "Language" -o "output"
            // Usage: -f "file" -t seconds -o "output"
            args = new[]
                       {
                           "-f",
                           @"D:\Documents\Visual Studio Projects\SubtitleKit\SubtitleKitLibCLI\bin\Debug\netcoreapp2.0\lalaland.srt",
                           "-l", "pl", "-o",
                           @"D:\Documents\Visual Studio Projects\SubtitleKit\SubtitleKitLibCLI\bin\Debug\netcoreapp2.0\lalalando.srt"
                       };
                       */
            var action = new Action();
            for (var i = 0; i < args.Length; i += 2)
            {
                var actionParam = args[i];
                var argumentParam = args[i + 1];

                action.AddParameter(actionParam, argumentParam);
            }

            action.PerformAction();
        }
    }
}