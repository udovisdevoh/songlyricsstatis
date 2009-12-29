using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingRepairerS
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
        public void Repair(PhoneticTable phoneticTable)
        {
            string phoneticEnding;
            foreach (HomophoneGroup homophoneGroup in new List<HomophoneGroup>(phoneticTable))
            {
                foreach (string wordVariant in new HashSet<string>(homophoneGroup))
                {
                    if (wordVariant == homophoneGroup.ShortestVariant + "s" || wordVariant == homophoneGroup.ShortestVariant + "es" ||  wordVariant == homophoneGroup.ShortestVariant + "ses")
                    {
                        phoneticEnding = BuildPhoneticEnding(homophoneGroup.PhoneticValue);
                        homophoneGroup.Remove(wordVariant);
                        phoneticTable.Add(wordVariant, homophoneGroup.PhoneticValue + " " + phoneticEnding);
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private string BuildPhoneticEnding(string phoneticValue)
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
