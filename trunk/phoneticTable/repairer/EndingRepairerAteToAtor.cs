using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerAteToAtor : EndingRepairer
    {
        public override bool IsMatchEndingType(string wordVariant, string shortHomophone)
        {
            return wordVariant.EndsWith("ator") && shortHomophone.EndsWith("ate");
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            return "[schwa] [dash] [treg] [schwa] [rreg]";
        }

        public override string RemoveUndesiredEnding(string originalString)
        {
            if (originalString.EndsWith("[amacr] [treg] [lprime]"))
                originalString = originalString.Substring(0, originalString.Length - 23);

            return originalString.Trim();
        }
    }
}
