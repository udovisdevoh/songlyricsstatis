using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class PhoneticTableTrimmer
    {
        #region Constants
        private const int beginingLength = 3;
        #endregion

        #region Public Methods
        public void Trim(PhoneticTable phoneticTable)
        {
            foreach (HomophoneGroup homophoneGroup in phoneticTable)
                Trim(homophoneGroup);
        }
        #endregion

        #region Private Methods
        private void Trim(HomophoneGroup homophoneGroup)
        {
            if (homophoneGroup.Count < 2)
                return;

            HashSet<string> beginingList = new HashSet<string>();

            IEnumerable<string> sortedWordListByLength = from word in homophoneGroup orderby word.Length select word;

            foreach (string word in sortedWordListByLength)
            {
                if (word.Length > beginingLength)
                {
                    if (word.StartsWith(beginingList))
                    {
                        homophoneGroup.Remove(word);
                    }
                    else
                    {
                        beginingList.Add(word.Substring(0, 3));
                    }
                }  
            }
        }
        #endregion
    }
}
