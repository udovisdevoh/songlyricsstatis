using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    static class PhoneticTableBuilder
    {
        #region Public Methods
        public static void build(WordListFile frequentWordListFile, string phoneticTableFile)
        {
            HashSet<string> wordCache = BuildWordCache(phoneticTableFile);
            string fromWord;
            string toWord;
            
            while (true)
            {
                fromWord = frequentWordListFile.GetNextWordNotIn(wordCache);
                wordCache.Add(fromWord);
                toWord = TranslateToPhoneticWord(fromWord);
                AppendTableElement(fromWord, toWord, phoneticTableFile);
            }
        }
        #endregion
        
        #region Private Methods
        private static HashSet<string> BuildWordCache(string phoneticTableFile)
        {
            HashSet<string> wordCache = new HashSet<string>();

            if (!File.Exists(phoneticTableFile))
                return wordCache;

            string word;
            using (StreamReader streamReader = new StreamReader(phoneticTableFile))
            {
                string line = null;

                if (!File.Exists(phoneticTableFile))
                    File.Create(phoneticTableFile);

                while (true)
                {
                    line = streamReader.ReadLine();
                    if (line == null)
                        break;
                    word = line.Substring(0, line.IndexOf(':'));
                    word = word.Trim();
                    wordCache.Add(word);
                }
            }

            return wordCache;
        }

        private static void AppendTableElement(string fromWord, string toWord, string phoneticTableFile)
        {
            using (StreamWriter streamWriter = File.AppendText(phoneticTableFile))
            {
                streamWriter.WriteLine(fromWord + " : " + toWord);
            }
        }

        private static string TranslateToPhoneticWord(string fromWord)
        {
            #warning Remove dummy code
            return "foo";
        }
        #endregion
    }
}
