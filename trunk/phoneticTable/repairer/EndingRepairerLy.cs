using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerLy : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            return wordVariant == shortHomophone + "ly";
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            phoneticValue = phoneticValue.Replace("[lprime]", "");
            phoneticValue = phoneticValue.Replace("[prime]", "");

            while (phoneticValue.Contains("  "))
                phoneticValue = phoneticValue.Replace("  ", " ");

            phoneticValue = phoneticValue.Trim();

            string phoneticEnding;

            if (phoneticValue.EndsWith("[lreg]"))
            {
                phoneticEnding = "[emacr]";
            }
            else
            {
                phoneticEnding = "[lreg] [emacr]";
            }

            return phoneticEnding;
        }
    }
}
