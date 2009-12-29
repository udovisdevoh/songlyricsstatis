using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Semantic matrix trimmer
    /// </summary>
    class SemanticMatrixTrimmer
    {
        #region Constants
        /// <summary>
        /// Desired target word count
        /// </summary>
        private const int desiredTargetWordCount = 5;
        #endregion

        #region Public Methods
        /// <summary>
        /// Return matrix with only one output word per theme
        /// </summary>
        /// <param name="rawSemanticMatrix">raw semantic matrix</param>
        /// <param name="currentThemeListFile">theme file</param>
        /// <returns>trimmed matrix</returns>
        public Matrix Trim(Matrix rawSemanticMatrix, ThemeListFile currentThemeListFile)
        {
            Matrix trimmedMatrix = new Matrix();
            HashSet<string> totalAvailableWordCheckList = new HashSet<string>();

            foreach (KeyValuePair<string, Dictionary<string, float>> sourceWordAndRow in rawSemanticMatrix.NormalData)
            {
                LearnFromRow(trimmedMatrix, sourceWordAndRow.Key, sourceWordAndRow.Value, currentThemeListFile, totalAvailableWordCheckList);
            }

            return trimmedMatrix;
        }
        #endregion

        #region Private Methods
        private void LearnFromRow(Matrix trimmedMatrix, string sourceWord, Dictionary<string, float> row, ThemeListFile themeListFile, HashSet<string> totalAvailableWordCheckList)
        {
            string targetWord;
            float value;
            HashSet<string> targetWordThemeList;
            Dictionary<string, int> themeCount = new Dictionary<string, int>();
            foreach (string themeName in themeListFile.ThemeNameList)
                themeCount.Add(themeName, 0);

            foreach (KeyValuePair<string, float> targetWordAndValue in row)
            {
                targetWord = targetWordAndValue.Key;
                value = targetWordAndValue.Value;

                targetWordThemeList = themeListFile.GetThemeList(targetWord);

                if (HasCommonValue(themeListFile.ThemeNameList, targetWordThemeList))
                {
                    if (HasAtLeastOneThemeWithLessThanCount(themeCount, desiredTargetWordCount, targetWordThemeList))
                    {
                        trimmedMatrix.SetStatistics(sourceWord, targetWord, value);
                        themeCount = addThemeCount(targetWordThemeList, themeCount);
                    }
                }
            }
        }

        private bool HasAtLeastOneThemeWithLessThanCount(Dictionary<string, int> themeCount, int minimum, HashSet<string> targetWordThemeList)
        {
            foreach (string themeName in targetWordThemeList)
                if (themeCount[themeName] < minimum)
                    return true;
            return false;
        }

        private Dictionary<string, int> addThemeCount(HashSet<string> targetWordThemeList, Dictionary<string, int> themeCount)
        {
            int count;
            foreach (string themeName in targetWordThemeList)
            {
                if (!themeCount.TryGetValue(themeName, out count))
                    themeCount.Add(themeName, 0);

                themeCount[themeName]++;
            }
            return themeCount;
        }
        
        /// <summary>
        /// Returns true if sets have at least one common value
        /// </summary>
        /// <param name="set1">set 1</param>
        /// <param name="set2">set 2</param>
        /// <returns>true if sets have at least one common value</returns>
        private bool HasCommonValue(ICollection<string> set1, HashSet<string> set2)
        {
            foreach (string value in set1)
                if (set2.Contains(value))
                    return true;
            return false;
        }
        #endregion
    }
}
