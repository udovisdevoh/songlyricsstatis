using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    public class HomophoneGroup : IEnumerable<string>
    {
        #region Fields
        private string phoneticValue;

        private HashSet<string> wordVariantList;
        #endregion

        #region Constructor
        public HomophoneGroup(string phoneticValue)
        {
            this.phoneticValue = phoneticValue;
            wordVariantList = new HashSet<string>();
        }
        #endregion

        #region Public Methods
        public bool Remove(string wordVariant)
        {
            return wordVariantList.Remove(wordVariant);
        }

        public bool Add(string wordVariant)
        {
            return wordVariantList.Add(wordVariant);
        }

        public string GetShortestVariant(string wordWithSameLetters)
        {
            string shortestVariant = null;
            string begining = null;

            if (wordWithSameLetters.Length >= 2)
                begining = wordWithSameLetters.Substring(0, 2);

            if (begining != null && shortestVariant == null)
            {
                foreach (string wordVariant in wordVariantList)
                {
                    if (shortestVariant == null || wordVariant.Length < shortestVariant.Length)
                    {
                        if (wordVariant.StartsWith(begining))
                        {
                            shortestVariant = wordVariant;
                        }
                    }
                }
            }

            if (shortestVariant == null)
            {
                foreach (string wordVariant in wordVariantList)
                {
                    if (shortestVariant == null || wordVariant.Length < shortestVariant.Length)
                    {
                        shortestVariant = wordVariant;
                    }
                }
            }

            return shortestVariant;
        }
        #endregion

        #region Properties
        public string PhoneticValue
        {
            get { return phoneticValue; }
        }

        public int Count
        {
            get { return wordVariantList.Count; }
        }
        #endregion

        #region IEnumerable<string> Members
        public IEnumerator<string> GetEnumerator()
        {
            return wordVariantList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wordVariantList.GetEnumerator();
        }
        #endregion
    }
}