using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class PhoneticTableRepairer
    {
        #region Parts
        private EndingRepairerEd endingRepairerEd = new EndingRepairerEd();

        private EndingRepairerS endingRepairerS = new EndingRepairerS();
        #endregion

        #region Public Methods
        public void Repair(PhoneticTable phoneticTable)
        {
            int countBeforeRepair;
            do
            {
                countBeforeRepair = phoneticTable.Count;

                endingRepairerS.Repair(phoneticTable);
                endingRepairerEd.Repair(phoneticTable);

                RepairEnding(phoneticTable, "ing", "[ibreve] [nreg] [greg]");

                RepairEnding(phoneticTable, "er", "[schwa] [rreg]");
                RepairEnding(phoneticTable, "r", "[schwa] [rreg]");
                RepairEnding(phoneticTable, "or", "[schwa] [rreg]");
                RepairEnding(phoneticTable, "ly", "[lreg] [emacr]");

                RepairEnding(phoneticTable, "ness", "[nreg] [ebreve] [sreg]");
                RepairEnding(phoneticTable, "less", "[lreg] [ebreve] [sreg]");
                RepairEnding(phoneticTable, "est", "[ebreve] [sreg] [treg]");
                RepairEnding(phoneticTable, "st", "[ebreve] [sreg] [treg]");

                RepairEnding(phoneticTable, "ment", "[mreg] [ebreve] [nreg] [prime] [treg]");
            } while (phoneticTable.Count != countBeforeRepair);

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
            RepairEnding(phoneticTable, "ate","ator");
            RepairEnding(phoneticTable, "ate", "ative");
            RepairEnding(phoneticTable, "ate", "ating");
            RepairEnding(phoneticTable, "ence", "ent");*/
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
