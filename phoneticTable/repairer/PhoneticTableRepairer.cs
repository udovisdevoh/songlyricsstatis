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

        private EndingRepairerLy endingRepairerLy = new EndingRepairerLy();

        private EndingRepairerIsmToIst endingRepairerIsmToIst = new EndingRepairerIsmToIst();

        private EndingRepairerIstToIsm endingRepairerIstToIsm = new EndingRepairerIstToIsm();
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
                endingRepairerLy.Repair(phoneticTable);
                endingRepairerIsmToIst.Repair(phoneticTable);
                endingRepairerIstToIsm.Repair(phoneticTable);

                RepairEnding(phoneticTable, "ing", "[ibreve] [nreg] [greg]");

                RepairEnding(phoneticTable, "er", "[schwa] [rreg]");
                RepairEnding(phoneticTable, "r", "[schwa] [rreg]");
                RepairEnding(phoneticTable, "or", "[schwa] [rreg]");

                RepairEnding(phoneticTable, "ic", "[ibreve] [kreg]");
                RepairEnding(phoneticTable, "ity", "[ibreve] [dash] [treg] [emacr]");

                RepairEnding(phoneticTable, "ism", "[ibreve] [zreg] [lprime] [schwa] [mreg]");
                RepairEnding(phoneticTable, "ist", "[ibreve] [dash] [sreg] [treg]");

                RepairEnding(phoneticTable, "ness", "[nreg] [ebreve] [sreg]");
                RepairEnding(phoneticTable, "less", "[lreg] [ebreve] [sreg]");
                RepairEnding(phoneticTable, "est", "[ebreve] [sreg] [treg]");
                RepairEnding(phoneticTable, "st", "[ebreve] [sreg] [treg]");

                RepairEnding(phoneticTable, "ation", "[amacr] [prime] [sreg] [hreg] [schwa] [nreg]");

                RepairEnding(phoneticTable, "al", "[lreg]");
                RepairEnding(phoneticTable, "ally", "[schwa] [dash] [lreg] [emacr]");
                
                RepairEnding(phoneticTable, "ment", "[mreg] [ebreve] [nreg] [prime] [treg]");
            } while (phoneticTable.Count != countBeforeRepair);

            #warning Implement Repair() for other cases

            //endingRepairerLy (l or no l at the end)

            //ist to ism and vice versae even when no ist or ism counterpart available

            //involuntary ->involuntarily

            //invulnerable -> invulnerability

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

            //militant -> militance
            //militant -> militancy
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
