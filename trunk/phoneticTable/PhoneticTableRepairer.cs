using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class PhoneticTableRepairer
    {
        #region Public Methods
        public void Repair(PhoneticTable phoneticTable)
        {
            RepairEnding(phoneticTable, "s", "[sreg]");
            RepairEnding(phoneticTable, "ing", "[ibreve] [nreg] [greg]");

            #warning Implement Repair() for other cases
            /*
            RepairEnding(phoneticTable,"ing");
            RepairEnding(phoneticTable, "ed");
            RepairEnding(phoneticTable, "er");
            //RepairEnding(phoneticTable, "ator");
            RepairEnding(phoneticTable, "ly");
            RepairEnding(phoneticTable, "ion");
            RepairEnding(phoneticTable, "es");

            RepairEnding(phoneticTable, "y","ies");
            RepairEnding(phoneticTable, "ate","ation");
            RepairEnding(phoneticTable, "tion", "tive");
            RepairEnding(phoneticTable, "ate", "ating");
            //RepairEnding(phoneticTable, "ence", "ent");*/
        }
        #endregion

        #region Private Methods
        private void RepairEnding(PhoneticTable phoneticTable, string englishEnding, string phoneticEnding)
        {
            phoneticEnding = phoneticEnding.Trim();

            foreach (HomophoneGroup homophoneGroup in new List<HomophoneGroup>(phoneticTable))
            {
                foreach (string wordVariant in  new HashSet<string>(homophoneGroup))
                {
                    if (wordVariant == homophoneGroup.ShortestVariant + englishEnding)
                    {
                        homophoneGroup.Remove(wordVariant);
                        phoneticTable.Add(wordVariant, homophoneGroup.PhoneticValue + " " + phoneticEnding);
                    }
                }
            }
        }
        #endregion
    }
}
