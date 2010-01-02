using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    class RhymeChartBuilder
    {
        #region Constants
        private const int howManyPhoneticSymbolForEnding = 2;

        private const int howManyEnglishLetterForEnding = 4;
        #endregion

        #region Public Methods
        public void Build(PhoneticTable phoneticTable, WordListFile frequentWordListFile, string rhymeChartFile)
        {
            string phoneticValue;

            using (StreamWriter streamWriter = new StreamWriter(rhymeChartFile))
            {
                foreach (string word in frequentWordListFile)
                {
                    phoneticValue = GetPhoneticValue(word,phoneticTable);
                    if (phoneticValue != null)
                    {
                        streamWriter.WriteLine(word + " : " + GetPhoneticEnding(phoneticValue));
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private string GetPhoneticValue(string word, PhoneticTable phoneticTable)
        {
            string phoneticValue = phoneticTable.GetPhoneticValueOf(word);
            if (phoneticValue == null)
                phoneticValue = GetPhoneticValueOfWordUsingSimilarWordEnding(word, phoneticTable);
            return phoneticValue;
        }

        private string GetPhoneticEnding(string phoneticValue)
        {
            if (!phoneticValue.Contains(' '))
                return phoneticValue;

            string[] wordList = phoneticValue.Split(' ');

            string ending = string.Empty;

            int key;
            for (int i = 0; i < howManyPhoneticSymbolForEnding; i++)
            {
                key = wordList.Length - i - 1;
                if (key > 0 && key < wordList.Length)
                    ending = wordList[key] + " " + ending;
            }

            return ending.Trim();
        }

        private string GetPhoneticValueOfWordUsingSimilarWordEnding(string word, PhoneticTable phoneticTable)
        {
            string englishEnding;
            if (word.Length >= howManyEnglishLetterForEnding)
                englishEnding = word.Substring(word.Length - howManyEnglishLetterForEnding);
            else
                englishEnding = word;

            string similarWord = GetWordEndsWith(englishEnding, phoneticTable);

            if (similarWord == null)
                return null;

            return phoneticTable.GetPhoneticValueOf(similarWord);
        }

        private string GetWordEndsWith(string englishEnding, PhoneticTable phoneticTable)
        {
            foreach (HomophoneGroup homophoneGroup in phoneticTable)
            {
                foreach (string currentWord in homophoneGroup)
                {
                    if (currentWord.EndsWith(englishEnding))
                    {
                        return phoneticTable.GetPhoneticValueOf(currentWord);
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
