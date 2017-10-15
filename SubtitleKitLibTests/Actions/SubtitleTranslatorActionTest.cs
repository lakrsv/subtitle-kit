using SubtitleKitLib.Actions;
using SubtitleKitLib.Azure;
using SubtitleKitLib.Subtitle;
using SubtitleKitLibTests.Subtitle;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace SubtitleKitLibTests.Actions
{
    public class SubtitleTranslatorActionTest
    {
        [Fact (Skip = "Requires Secret keys, not available")]
        public void Translating_ValidSub_Passes()
        {
            var validSub = SubtitleContainer.GetSubtitleFromFile(SubtitleContainer.ValidSubtitleName);
            var validSubClone = (ISubtitle)validSub.Clone();
            var subtitleTranslator = new SubtitleTranslatorAction(validSub, new CultureInfo("no"));

            subtitleTranslator.PerformAction(()=>
            {
                Assert.True(validSub.Items.Count == validSubClone.Items.Count, "Item count after translation changed");

                bool hasDifferentLines = false;
                for (int i = 0; i < validSub.Items.Count; i++)
                {
                    if(!string.Equals(validSub.Items[i].ToString(), validSubClone.Items[i].ToString()))
                    {
                        hasDifferentLines = true;
                        break;
                    }
                }
                Assert.True(hasDifferentLines);
            });
        }
    }
}
