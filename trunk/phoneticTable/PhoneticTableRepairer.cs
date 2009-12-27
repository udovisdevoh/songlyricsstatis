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
            #warning Implement Repair()
            RepairEndingS(phoneticTable);            
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
        private void RepairEndingS(PhoneticTable phoneticTable)
        {
            //Must take care of word identical to shortes homophone + ending
            #warning Implement RepairEnding()
            throw new NotImplementedException();
        }
        #endregion
    }
}
