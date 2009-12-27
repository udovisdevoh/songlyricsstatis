using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class HomophoneGroup : IEnumerable<string>
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
        #endregion

        #region Properties
        public string PhoneticValue
        {
            get { return phoneticValue; }
        }

        public string ShortestVariant
        {
            get
            {
                string shortestVariant = null;

                foreach (string wordVariant in wordVariantList)
                {
                    if (shortestVariant == null || wordVariant.Length < shortestVariant.Length)
                    {
                        shortestVariant = wordVariant;
                    }
                }

                return shortestVariant;
            }
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