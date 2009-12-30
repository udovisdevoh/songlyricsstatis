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

        private EndingRepairerYtoIes endingRepairerYtoIes = new EndingRepairerYtoIes();

        private EndingRepairerAteToAtion endingRepairerAteToAtion = new EndingRepairerAteToAtion();

        private EndingRepairerAteToAting endingRepairerAteToAting = new EndingRepairerAteToAting();

        private EndingRepairerAteToAtive endingRepairerAteToAtive = new EndingRepairerAteToAtive();

        private EndingRepairerAteToAtor endingRepairerAteToAtor = new EndingRepairerAteToAtor();

        private EndingRepairerYToIc endingRepairerYToIc = new EndingRepairerYToIc();

        private EndingReplacer endingReplacer = new EndingReplacer();
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
                endingRepairerYtoIes.Repair(phoneticTable);
                endingRepairerAteToAtion.Repair(phoneticTable);
                endingRepairerAteToAting.Repair(phoneticTable);
                endingRepairerAteToAtive.Repair(phoneticTable);
                endingRepairerAteToAtor.Repair(phoneticTable);
                endingRepairerYToIc.Repair(phoneticTable);


                RepairEnding(phoneticTable, "ing", "[ibreve] [nreg] [greg]");

                RepairEnding(phoneticTable, "er", "[schwa] [rreg]");
                RepairEnding(phoneticTable, "ers", "[schwa] [rreg] [zreg]");
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


                endingReplacer.ReplaceEnding(phoneticTable, "y", "[emacr]", "ically", "[ibreve] [dash] [kreg] [schwa] [lreg] [emacr]");
                endingReplacer.ReplaceEnding(phoneticTable, "ic", "[ibreve] [kreg]", "ically", "[ibreve] [dash] [kreg] [schwa] [lreg] [emacr]");

                endingReplacer.ReplaceEnding(phoneticTable, "ate", "[amacr] [treg] [lprime]", "ations", "[amacr] [prime] [sreg] [hreg] [schwa] [nreg] [zreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ate", "[amacr] [treg] [lprime]", "ators", "[amacr] [lprime] [treg] [schwa] [rreg] [zreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ize", "[imacr] [zreg] [lprime]", "izing", "[imacr] [lprime] [zreg] [ibreve] [nreg] [greg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ize", "[imacr] [zreg] [lprime]", "ised", "[imacr] [zreg] [lprime] [dreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ism", "[ibreve] [zreg] [lprime] [schwa] [mreg]", "istic", "[ibreve] [sreg] [treg] [ibreve] [kreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ism", "[ibreve] [zreg] [lprime] [schwa] [mreg]", "istically", "[ibreve] [sreg] [treg] [ibreve] [kreg] [schwa] [dash] [lreg] [emacr]");

                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg] [prime]", "pped", "[preg] [treg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg] [prime]", "pping", "[preg] [ibreve] [nreg] [greg]");

                endingReplacer.ReplaceEnding(phoneticTable, "fy", "[freg] [imacr] [lprime]", "fied", "[freg] [imacr] [dreg] [lprime]");
                endingReplacer.ReplaceEnding(phoneticTable, "fy", "[freg] [imacr] [lprime]", "fication", "[freg] [ibreve] [dash] [kreg] [amacr] [prime] [sreg] [hreg] [schwa] [nreg]");
            } while (phoneticTable.Count != countBeforeRepair);

            #warning Implement Repair() for other cases

            //endingRepairerLy (l or no l at the end)

            //ist to ism and vice versae even when no ist or ism counterpart available

            //involuntary ->involuntarily

            //invulnerable -> invulnerability

            //evict -> eviction

            /*
            //RepairEnding(phoneticTable, "ator");
            RepairEnding(phoneticTable, "ly");
            RepairEnding(phoneticTable, "ion");
            RepairEnding(phoneticTable, "es");

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
                    if (wordVariant == homophoneGroup.GetShortestVariant(wordVariant) + englishEnding && wordVariant != homophoneGroup.GetShortestVariant(wordVariant))
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
