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





                endingReplacer.ReplaceEnding(phoneticTable, "y", "[emacr]", "ically", "[ibreve] [dash] [kreg] [schwa] [lreg] [emacr]");
                endingReplacer.ReplaceEnding(phoneticTable, "ic", "[ibreve] [kreg]", "ically", "[ibreve] [dash] [kreg] [schwa] [lreg] [emacr]");

                endingReplacer.ReplaceEnding(phoneticTable, "ate", "[amacr] [treg] [lprime]", "ations", "[amacr] [prime] [sreg] [hreg] [schwa] [nreg] [zreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ate", "[amacr] [treg] [lprime]", "ators", "[amacr] [lprime] [treg] [schwa] [rreg] [zreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ize", "[imacr] [zreg] [lprime]", "izing", "[imacr] [lprime] [zreg] [ibreve] [nreg] [greg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ize", "[imacr] [zreg] [lprime]", "ising", "[imacr] [lprime] [zreg] [ibreve] [nreg] [greg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ize", "[imacr] [zreg] [lprime]", "ised", "[imacr] [zreg] [lprime] [dreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ism", "[ibreve] [zreg] [lprime] [schwa] [mreg]", "istic", "[ibreve] [sreg] [treg] [ibreve] [kreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ism", "[ibreve] [zreg] [lprime] [schwa] [mreg]", "istically", "[ibreve] [sreg] [treg] [ibreve] [kreg] [schwa] [dash] [lreg] [emacr]");

                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg] [prime]", "pped", "[preg] [treg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg] [prime]", "pper", "[preg] [prime] [schwa] [rreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg] [prime]", "ppers", "[preg] [prime] [schwa] [rreg] [zreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg] [prime]", "pping", "[preg] [ibreve] [nreg] [greg]");

                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg]", "pped", "[preg] [treg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg]", "pper", "[preg] [prime] [schwa] [rreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg]", "ppers", "[preg] [prime] [schwa] [rreg] [zreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "p", "[preg]", "pping", "[preg] [ibreve] [nreg] [greg]");

                endingReplacer.ReplaceEnding(phoneticTable, "fy", "[freg] [imacr] [lprime]", "fied", "[freg] [imacr] [dreg] [lprime]");
                endingReplacer.ReplaceEnding(phoneticTable, "fy", "[freg] [imacr] [lprime]", "fication", "[freg] [ibreve] [dash] [kreg] [amacr] [prime] [sreg] [hreg] [schwa] [nreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ist", "[ibreve] [dash] [sreg] [treg]", "istical", "[ibreve] [sreg] [prime] [treg] [ibreve] [dash] [kreg] [schwa] [lreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ist", "[ibreve] [sreg] [treg]", "istical", "[ibreve] [sreg] [prime] [treg] [ibreve] [dash] [kreg] [schwa] [lreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "er", "[schwa] [rreg]", "erous", "[schwa] [rreg] [dash] [schwa] [sreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "er", "[schwa] [rreg]", "ering", "[schwa] [rreg] [ibreve] [nreg] [greg]");
                endingReplacer.ReplaceEnding(phoneticTable, "er", "[schwa] [rreg]", "erings", "[schwa] [rreg] [ibreve] [nreg] [greg] [zreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "y", "[emacr]", "iness", "[emacr] [nreg] [ebreve] [sreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "y", "[emacr]", "ier", "[imacr] [lprime] [schwa] [rreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ant", "[schwa] [nreg] [treg]", "ance", "[schwa] [nreg] [sreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ent", "[schwa] [nreg] [treg]", "ence", "[schwa] [nreg] [sreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ant", "[schwa] [nreg] [treg]", "ancy", "[schwa] [nreg] [dash] [sreg] [emacr]");
                endingReplacer.ReplaceEnding(phoneticTable, "ent", "[schwa] [nreg] [treg]", "ency", "[schwa] [nreg] [dash] [sreg] [emacr]");

                endingReplacer.ReplaceEnding(phoneticTable, "ance", "[schwa] [nreg] [sreg]", "ant", "[schwa] [nreg] [treg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ence", "[schwa] [nreg] [sreg]", "ent", "[schwa] [nreg] [treg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ancy", "[schwa] [nreg] [dash] [sreg] [emacr]", "ant", "[schwa] [nreg] [treg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ency", "[schwa] [nreg] [dash] [sreg] [emacr]", "ent", "[schwa] [nreg] [treg]");

                endingReplacer.ReplaceEnding(phoneticTable, "tion", "[sreg] [hreg] [schwa] [nreg]", "tive", "[treg] [ibreve] [vreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "tive", "[treg] [ibreve] [vreg]", "tion", "[sreg] [hreg] [schwa] [nreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ia", "[emacr] [dash] [schwa]", "ic", "[ibreve] [kreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ic", "[ibreve] [kreg]", "ia", "[emacr] [dash] [schwa]");

                endingReplacer.ReplaceEnding(phoneticTable, "ists", "[ibreve] [sreg] [treg] [sreg]", "isms", "[ibreve] [zreg] [lprime] [schwa] [mreg] [zreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "isms", "[ibreve] [zreg] [lprime] [schwa] [mreg] [zreg]", "ists", "[ibreve] [sreg] [treg] [sreg]");

                endingReplacer.ReplaceEnding(phoneticTable, "ist", "[ibreve] [sreg] [treg]", "isms", "[ibreve] [zreg] [lprime] [schwa] [mreg] [zreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ism", "[ibreve] [zreg] [lprime] [schwa] [mreg]", "ists", "[ibreve] [sreg] [treg] [sreg]");


                endingReplacer.ReplaceEnding(phoneticTable, "ble", "[breg] [schwa] [lreg]", "bility", "[breg] [ibreve] [lreg] [prime] [ibreve] [dash] [treg] [emacr]");

                endingReplacer.ReplaceEnding(phoneticTable, "ize", "[imacr] [zreg] [lprime]", "ization", "[ibreve] [dash] [zreg] [amacr] [prime] [sreg] [hreg] [schwa] [nreg]");


                endingReplacer.ReplaceEnding(phoneticTable, "ia", "[emacr] [dash] [schwa]", "ian", "[emacr] [dash] [schwa] [nreg]");
                endingReplacer.ReplaceEnding(phoneticTable, "ian", "[emacr] [dash] [schwa] [nreg]", "ia", "[emacr] [dash] [schwa]");



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

                RepairEnding(phoneticTable, "istic", "[ibreve] [sreg] [treg] [ibreve] [kreg]");
                RepairEnding(phoneticTable, "ically", "[ibreve] [dash] [kreg] [schwa] [lreg] [emacr]");

                RepairEnding(phoneticTable, "ation", "[amacr] [prime] [sreg] [hreg] [schwa] [nreg]");
                RepairEnding(phoneticTable, "ations", "[amacr] [prime] [sreg] [hreg] [schwa] [nreg] [zreg]");

                RepairEnding(phoneticTable, "al", "[lreg]");
                RepairEnding(phoneticTable, "ally", "[schwa] [dash] [lreg] [emacr]");

                RepairEnding(phoneticTable, "ment", "[mreg] [ebreve] [nreg] [prime] [treg]");

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
