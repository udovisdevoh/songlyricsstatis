using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerAteToAtion : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            return wordVariant.EndsWith("ation") && shortHomophone.EndsWith("ate");
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            return "[amacr] [prime] [sreg] [hreg] [schwa] [nreg]";
        }

        public override string RemoveUndesiredEnding(string originalString)
        {
            if (originalString.EndsWith("[amacr] [treg] [lprime]"))
                originalString = originalString.Substring(0, originalString.Length - 23);

            return originalString.Trim();
        }
    }
}
