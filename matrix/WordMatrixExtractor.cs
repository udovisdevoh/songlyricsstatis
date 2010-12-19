using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Word matrix extractor
    /// </summary>
    class WordMatrixExtractor
    {
        #region Public Methods
        /// <summary>
        /// Create a word pair occurence matrix from text file
        /// </summary>
        /// <param name="textFileName">text file name</param>
        /// <returns>word pair occurence matrix from text file</returns>
        public Matrix BuildMatrixFromTextFile(string textFileName)
        {
            return BuildMatrixFromTextFile(textFileName, null);
        }

        public Matrix BuildMatrixFromTextFile(string textFileName, ICollection<string> desiredWordList)
        {
            return BuildMatrixFromTextFile(textFileName, desiredWordList, 1);
        }

        /// <summary>
        /// Create a word pair occurence matrix from text file
        /// </summary>
        /// <param name="textFileName">text file name</param>
        /// <param name="desiredWordList">desired word list</param>
        /// <param name="wordSequenceLength">how many "from words", default: 1</param>
        /// <returns>word pair occurence matrix from text file</returns>
        public Matrix BuildMatrixFromTextFile(string textFileName, ICollection<string> desiredWordList, int wordSequenceLength)
        {
            Matrix matrix = new Matrix();

            string line;
            using (StreamReader file = new StreamReader(textFileName))
            {
                while ((line = file.ReadLine()) != null)
                    LearnFromLine(matrix, line, desiredWordList, wordSequenceLength);
            }

            return matrix;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Learn from line
        /// </summary>
        /// <param name="matrix">matrix to add information to</param>
        /// <param name="line">line to learn from</param>
        /// <param name="desiredWordList">desired word list</param>
        private void LearnFromLine(Matrix matrix, string line, ICollection<string> desiredWordList)
        {
            LearnFromLine(matrix, line, desiredWordList, 1);
        }

        /// <summary>
        /// Learn from line
        /// </summary>
        /// <param name="matrix">matrix to add information to</param>
        /// <param name="line">line to learn from</param>
        /// <param name="desiredWordList">desired word list</param>
        /// /// <param name="wordSequenceLength">word sequence length. Default: 1</param>
        private void LearnFromLine(Matrix matrix, string line, ICollection<string> desiredWordList, int wordSequenceLength)
        {
            line = line.Replace("-", "_ ");
            line = line.Replace("?", " ");
            line = line.Replace("(", " ");
            line = line.Replace(")", " ");
            line = line.Replace("’", "'");
            //line = line.Replace("'", " ");

            while (line.Contains("__"))
                line = line.Replace("__", "_");

            line = line.RemoveWord("_");

            line = line.FixStringForHimmlStatementParsing();

            IEnumerable<string> wordList = line.SplitWords();

            string previousPreviousWord = null;
            string previousWord = null;
            foreach (string currentWord in wordList)
            {
                if (wordSequenceLength == 1)
                {
                    if (previousWord != null)
                        if (desiredWordList == null || desiredWordList.Contains(currentWord) || desiredWordList.Contains(previousWord))
                            matrix.AddStatistics(previousWord, currentWord);
                }
                else if (wordSequenceLength == 2)
                {
                    if (previousWord != null && previousPreviousWord != null)
                        if (desiredWordList == null || desiredWordList.Contains(currentWord) || desiredWordList.Contains(previousPreviousWord + " " + previousWord))
                            matrix.AddStatistics(previousPreviousWord + " " + previousWord, currentWord);
                }

                previousPreviousWord = previousWord;
                previousWord = currentWord;
            }
        }
        #endregion
    }
}
