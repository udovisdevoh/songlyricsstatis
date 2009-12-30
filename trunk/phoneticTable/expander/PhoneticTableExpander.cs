using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class PhoneticTableExpander
    {
        #region Parts
        private PhoneticConcatenator phoneticConcatenator = new PhoneticConcatenator();

        private PhoneticSplitter phoneticSplitter = new PhoneticSplitter();
        #endregion

        #region Public Methods
        public void Expand(PhoneticTable phoneticTable, WordListFile frequentWordListFile)
        {
            string phoneticValue;
            foreach (string word in frequentWordListFile)
            {
                if (!phoneticTable.Contains(word))
                {
                    phoneticValue = null;

                    if (phoneticValue == null)
                        phoneticValue = phoneticConcatenator.TryConcatenate(word, phoneticTable);

                    if (phoneticValue == null)
                        phoneticValue = phoneticSplitter.TrySplit(word, phoneticTable);

                    if (phoneticValue != null)
                        phoneticTable.Add(word, phoneticValue);
                }
            }
        }
        #endregion
    }
}
