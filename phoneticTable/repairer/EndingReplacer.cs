using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class EndingReplacer
    {
        #region Public Methods
        public void ReplaceEnding(PhoneticTable phoneticTable, string fromEnglish, string fromPhonetic, string toEnglish, string toPhonetic)
        {
            string phoneticValue;
            foreach (HomophoneGroup homophoneGroup in new List<HomophoneGroup>(phoneticTable))
            {
                foreach (string wordVariant in new HashSet<string>(homophoneGroup))
                {
                    if (wordVariant.EndsWith(toEnglish))
                    {
                        if (homophoneGroup.ShortestVariant.EndsWith(fromEnglish) && wordVariant != homophoneGroup.ShortestVariant)
                        {
                            if (homophoneGroup.PhoneticValue.EndsWith(fromPhonetic))
                            {
                                phoneticValue = ReplaceEnding(homophoneGroup.PhoneticValue, fromPhonetic, toPhonetic);
                                homophoneGroup.Remove(wordVariant);
                                phoneticTable.Add(wordVariant, phoneticValue);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private string ReplaceEnding(string phoneticValue, string from, string to)
        {
            if (phoneticValue.EndsWith(from))
                phoneticValue = phoneticValue.Substring(0, phoneticValue.Length - from.Length).Trim() + " " + to;
            return phoneticValue;
        }
        #endregion
    }
}
