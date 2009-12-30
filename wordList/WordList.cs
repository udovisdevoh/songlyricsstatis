using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Word list file (sorted by occurence count)
    /// </summary>
    class WordListFile : IEnumerable<string>
    {
        #region Fields
        /// <summary>
        /// List of word
        /// </summary>
        private List<string> wordList;

        /// <summary>
        /// Current word pointer
        /// </summary>
        private int counter;
        #endregion

        #region Constructor
        /// <summary>
        /// Create word list file
        /// </summary>
        /// <param name="fileName">file name</param>
        public WordListFile(string fileName)
        {
            wordList = new List<string>();
            counter = 0;
        }

        /// <summary>
        /// Create word list file
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <param name="wordList">word list</param>
        public WordListFile(string fileName, List<string> wordList)
        {
            this.wordList = wordList;
            counter = 0;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get the immediate next word that is not in existing themes
        /// </summary>
        /// <param name="themeListFile">themeListFile</param>
        /// <returns>immediate next word that is not in existing themes</returns>
        public string GetNextWordNotInTheme(ThemeListFile themeListFile)
        {
            string nextWord;
            do
            {
                nextWord = GetNextWord();
            } while (themeListFile.ContainsWord(nextWord));
            return nextWord;
        }

        /// <summary>
        /// Get the immediate next word that is not in provided HashSet
        /// </summary>
        /// <param name="wordList">word list</param>
        /// <returns>immediate next word that is not in provided HashSet</returns>
        public string GetNextWordNotIn(HashSet<string> wordList)
        {
            string nextWord;
            do
            {
                nextWord = GetNextWord();
            } while (wordList.Contains(nextWord));
            return nextWord;
        }

        /// <summary>
        /// Save word list
        /// </summary>
        /// <param name="fileName">fine name</param>
        public void Save(string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                foreach (string word in wordList)
                    streamWriter.WriteLine(word);
            }
        }

        /// <summary>
        /// Load word list
        /// </summary>
        /// <param name="fileName">file name</param>
        public void Load(string fileName)
        {
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string line = null;
                while (true)
                {
                    line = streamReader.ReadLine();
                    if (line == null)
                        break;

                    line = line.Trim();

                    if (line.Length > 0)
                        wordList.Add(line);
                }
            }

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get the immediate next word from list
        /// </summary>
        /// <returns>immediate next word from list</returns>
        private string GetNextWord()
        {
            if (counter >= wordList.Count)
                return null;
            counter++;
            return wordList[counter - 1];
        }
        #endregion

        #region IEnumerable<string> Members
        public IEnumerator<string> GetEnumerator()
        {
            return wordList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wordList.GetEnumerator();
        }
        #endregion
    }
}
