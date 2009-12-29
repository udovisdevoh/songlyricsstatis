using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerEd : EndingRepairer
    {
        #region Public Methods
        public override bool IsMatchEndingType(string wordVariant, HomophoneGroup homophoneGroup)
        {
            return wordVariant == homophoneGroup.ShortestVariant + "ed" || (wordVariant == homophoneGroup.ShortestVariant + "d" && homophoneGroup.ShortestVariant.EndsWith("e"));
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            /*
             * ending with: [treg] or [dreg] + facultative [*prime] -> [ibreve] [dreg]
             * ending with: [preg], [freg], [sreg], [sreg] [hreg], [creg] [hreg] or [kreg]  + facultative [*prime] -> [treg] [lprime]
             * else: [dreg] [lprime]
             */

            phoneticValue = phoneticValue.Replace("[lprime]", "");
            phoneticValue = phoneticValue.Replace("[prime]", "");

            while (phoneticValue.Contains("  "))
                phoneticValue = phoneticValue.Replace("  ", " ");

            phoneticValue = phoneticValue.Trim();

            string phoneticEnding;

            if (phoneticValue.EndsWith("[treg]") || phoneticValue.EndsWith("[dreg]"))
            {
                phoneticEnding = "[ibreve] [dreg]";
            }
            else if (phoneticValue.EndsWith("[preg]") || phoneticValue.EndsWith("[freg]") || phoneticValue.EndsWith("[sreg]") || phoneticValue.EndsWith("[sreg] [hreg]") || phoneticValue.EndsWith("[creg] [hreg]") || phoneticValue.EndsWith("[kreg]"))
            {
                phoneticEnding = "[treg]";
            }
            else
            {
                phoneticEnding = "[dreg]";
            }

            return phoneticEnding;
        }
        #endregion
    }
}
