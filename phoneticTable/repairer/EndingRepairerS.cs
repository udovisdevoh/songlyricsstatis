using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerS : EndingRepairer
    {
        #region Fields
        private List<string> endingsForEs;

        private List<string> endingsForZ;
        #endregion

        #region Constructors
        public EndingRepairerS()
        {
            endingsForEs = new List<string>();
            endingsForZ = new List<string>();

            endingsForEs.Add("[sreg] [hreg]");
            endingsForEs.Add("[creg] [hreg]");
            endingsForEs.Add("[sreg]");
            endingsForEs.Add("[jreg]");

            endingsForZ.Add("[breg]");
            endingsForZ.Add("[dreg]");
            endingsForZ.Add("[greg]");
            endingsForZ.Add("[lreg]");
            endingsForZ.Add("[mreg]");
            endingsForZ.Add("[nreg]");
            endingsForZ.Add("[rreg]");
            endingsForZ.Add("[vreg]");
        }
        #endregion

        #region Public Methods
        public override bool IsMatchEndingType(string wordVariant, HomophoneGroup homophoneGroup)
        {
            return wordVariant == homophoneGroup.ShortestVariant + "s" || wordVariant == homophoneGroup.ShortestVariant + "es" || wordVariant == homophoneGroup.ShortestVariant + "ses";
        }

        public override string BuildPhoneticEnding(string phoneticValue)
        {
            phoneticValue = phoneticValue.Replace("[lprime]", "");
            phoneticValue = phoneticValue.Replace("[prime]", "");

            while (phoneticValue.Contains("  "))
                phoneticValue = phoneticValue.Replace("  ", " ");

            phoneticValue = phoneticValue.Trim();

            string phoneticEnding;

            if (phoneticValue.EndsWith(endingsForEs))
            {
                phoneticEnding = "[schwa] [sreg]";
            }
            else if (phoneticValue.EndsWith(endingsForZ))
            {
                phoneticEnding = "[zreg]";
            }
            else
            {
                phoneticEnding = "[sreg]";
            }

            return phoneticEnding;
        }
        #endregion
    }
}
