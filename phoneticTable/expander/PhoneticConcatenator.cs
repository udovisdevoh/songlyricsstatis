using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Used to concatenate words and their phonetic counterparts
    /// </summary>
    class PhoneticConcatenator
    {
        #region Public Methods
        /// <summary>
        /// Get phonetic value by concatenation or null if fails
        /// </summary>
        /// <param name="word">word for which we need phonetic value</param>
        /// <param name="phoneticTable">phonetic table</param>
        /// <returns>phonetic value by concatenation or null if fails</returns>
        public string TryConcatenate(string word, PhoneticTable phoneticTable)
        {
            List<string> listWordStartsWith = GetListStartsWith(word, phoneticTable);
            List<string> listWordEndsWith = GetListEndsWith(word, phoneticTable);

            string concatenation = null;

            foreach (string wordStart in listWordStartsWith)
            {
                foreach (string wordEnd in listWordEndsWith)
                {
                    concatenation = TryConcatenate(word, wordStart, wordEnd, phoneticTable);
                    if (concatenation != null)
                        return concatenation;
                }
            }
            return null;
        }
        #endregion

        #region Private Methods
        private List<string> GetListStartsWith(string word, PhoneticTable phoneticTable)
        {
            List<string> listStartsWith = new List<string>();
            foreach (string currentWord in phoneticTable.EnglishWordList)
            {
                if (currentWord.StartsWith(word))
                {
                    listStartsWith.Add(currentWord);
                }
            }
            return listStartsWith;
        }

        private List<string> GetListEndsWith(string word, PhoneticTable phoneticTable)
        {
            List<string> listEndsWith = new List<string>();
            foreach (string currentWord in phoneticTable.EnglishWordList)
            {
                if (currentWord.EndsWith(word))
                {
                    listEndsWith.Add(currentWord);
                }
            }
            return listEndsWith;
        }

        private string TryConcatenate(string word, string wordStart, string wordEnd, PhoneticTable phoneticTable)
        {
            if (word == wordStart + wordEnd)
            {
                return phoneticTable.GetPhoneticValueOf(wordStart) + " [dash] " + phoneticTable.GetPhoneticValueOf(wordEnd);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
