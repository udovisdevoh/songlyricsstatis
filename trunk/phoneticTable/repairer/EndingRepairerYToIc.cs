using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerYToIc : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            return wordVariant.EndsWith("ic") && shortHomophone.EndsWith("y");
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            return "[ibreve] [kreg]";
        }

        public override string RemoveUndesiredEnding(string originalString)
        {
            if (originalString.EndsWith("[emacr]"))
                originalString = originalString.Substring(0, originalString.Length - 7);

            return originalString.Trim();
        }
    }
}
