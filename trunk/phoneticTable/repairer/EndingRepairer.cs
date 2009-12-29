using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    abstract class EndingRepairer
    {
        /// <summary>
        /// Repair phonetic table
        /// </summary>
        /// <param name="phoneticTable">phonetic table</param>
        public void Repair(PhoneticTable phoneticTable)
        {
            string phoneticEnding;
            foreach (HomophoneGroup homophoneGroup in new List<HomophoneGroup>(phoneticTable))
            {
                foreach (string wordVariant in new HashSet<string>(homophoneGroup))
                {
                    if (IsMatchEndingType(wordVariant, homophoneGroup))
                    {
                        phoneticEnding = BuildPhoneticEnding(homophoneGroup.PhoneticValue);
                        homophoneGroup.Remove(wordVariant);
                        phoneticTable.Add(wordVariant, homophoneGroup.PhoneticValue + " " + phoneticEnding);
                    }
                }
            }
        }

        /// <summary>
        /// Whether word variant is matching ending type
        /// </summary>
        /// <param name="wordVariant">word variant</param>
        /// <param name="homophoneGroup">homophone group</param>
        /// <returns>Whether word variant is matching ending type</returns>
        public abstract bool IsMatchEndingType(string wordVariant, HomophoneGroup homophoneGroup);

        /// <summary>
        /// Build phonetic ending
        /// </summary>
        /// <param name="phoneticValue">current phonetic value</param>
        /// <returns>phonetic ending</returns>
        public abstract string BuildPhoneticEnding(string phoneticValue);
    }
}
