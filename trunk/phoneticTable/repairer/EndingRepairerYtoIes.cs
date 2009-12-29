using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerYtoIes : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            if (shortHomophone.EndsWith("y"))
                if (wordVariant.EndsWith("ies"))
                    return true;

            return false;
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            return "[zreg] [lprime]";
        }
    }
}
