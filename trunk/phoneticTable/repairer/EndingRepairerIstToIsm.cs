using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerIstToIsm : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            if (shortHomophone.EndsWith("ist"))
                if (shortHomophone.Substring(0,shortHomophone.Length - 1) + "m" == wordVariant)
                    return true;

            return false;
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            return "[ibreve] [zreg] [lprime] [schwa] [mreg]";
        }

        public override string RemoveUndesiredEnding(string originalString)
        {
            if (originalString.EndsWith("[ibreve] [sreg] [treg]"))
                originalString = originalString.Substring(0, originalString.Length - 22);

            return originalString.Trim();
        }

        public override bool IsMatchPhoneticEnding(string phoneticValue)
        {
            return phoneticValue.EndsWith("[ibreve] [sreg] [treg]");
        }

        public override bool IsMatchWordVariantEnding(string wordVariant)
        {
            return wordVariant.EndsWith("ism");
        }
    }
}
