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
            #warning Implement TryConcatenate() (return null value if fails)
            throw new NotImplementedException();
        }
        #endregion
    }
}
