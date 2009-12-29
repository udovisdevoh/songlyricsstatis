using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerIsmToIst : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            if (shortHomophone.EndsWith("ism"))
                if (shortHomophone.Substring(0,shortHomophone.Length - 1) + "t" == wordVariant)
                    return true;

            return false;
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            return "[ibreve] [dash] [sreg] [treg]";
        }

        public override string RemoveUndesiredEnding(string originalString)
        {
            if (originalString.EndsWith("[ibreve] [zreg] [lprime] [schwa] [mreg]"))
                originalString = originalString.Substring(0, originalString.Length - 39);

            return originalString.Trim();
        }

        public override bool IsMatchPhoneticEnding(string phoneticValue)
        {
            return phoneticValue.EndsWith("[ibreve] [zreg] [lprime] [schwa] [mreg]");
        }

        public override bool IsMatchWordVariantEnding(string wordVariant)
        {
            return wordVariant.EndsWith("ist");
        }
    }
}
