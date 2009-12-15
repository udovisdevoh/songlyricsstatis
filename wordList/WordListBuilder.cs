using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Builds sorted word list
    /// </summary>
    static class WordListBuilder
    {
        #region Public Methods
        /// <summary>
        /// Builds sorted word list
        /// </summary>
        /// <param name="sourceFileName">source text file name</param>
        /// <param name="wordListFileName">target word list file name</param>
        /// <returns>word list that can be saved to file</returns>
        public static WordListFile Build(string sourceFileName, string wordListFileName)
        {
            Dictionary<string, int> wordCounter = GetWordCountFromSourceFile(sourceFileName);
            List<string> internalWordList = SortWordList(wordCounter);
            return new WordListFile(wordListFileName, internalWordList);
        }
        #endregion

        #region Private Methods
        private static Dictionary<string, int> GetWordCountFromSourceFile(string sourceFileName)
        {
            List<string> wordList;
            int count;
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            using (StreamReader streamReader = new StreamReader(sourceFileName))
            {
                string line = null;
                while (true)
                {
                    line = streamReader.ReadLine();
                    if (line == null)
                        break;

                    wordList = line.SplitWords().ToList();

                    foreach (string word in wordList)
                    {
                        if (word.Length > 1)
                        {
                            if (!wordCount.TryGetValue(word, out count))
                            {
                                count = 0;
                                wordCount.Add(word, count);
                            }
                            count++;
                            wordCount[word] = count;
                        }
                    }
                }
            }
            return wordCount;
        }

        private static List<string> SortWordList(Dictionary<string, int> wordCounter)
        {
            List<string> sortedList = (from element in wordCounter orderby element.Value descending select element.Key).ToList();
            return sortedList;
        }
        #endregion
    }
}