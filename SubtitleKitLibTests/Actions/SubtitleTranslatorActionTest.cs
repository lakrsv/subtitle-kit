// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleTranslatorActionTest.cs" author="Lars-Kristian Svenoy">
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

namespace SubtitleKitLibTests.Actions
{
    using System.Globalization;

    using SubtitleKitLib.Actions;
    using SubtitleKitLib.Subtitle;

    using SubtitleKitLibTests.Subtitle;

    using Xunit;

    public class SubtitleTranslatorActionTest
    {
        [Fact]
        public void Translating_ValidSub_Passes()
        {
            var validSub = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidShortSubtitleName);
            var validSubClone = (ISubtitle)validSub.Clone();
            var subtitleTranslator = new SubtitleTranslatorAction(validSub, new CultureInfo("no"));

            subtitleTranslator.PerformAction(
                () =>
                    {
                        Assert.True(
                            validSub.Items.Count == validSubClone.Items.Count,
                            "Item count after translation changed");

                        var hasDifferentLines = false;
                        for (var i = 0; i < validSub.Items.Count; i++)
                            if (!string.Equals(validSub.Items[i].ToString(), validSubClone.Items[i].ToString()))
                            {
                                hasDifferentLines = true;
                                break;
                            }

                        Assert.True(hasDifferentLines);
                    });
        }
    }
}