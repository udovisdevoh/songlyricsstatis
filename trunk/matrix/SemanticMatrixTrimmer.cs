using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    class SemanticMatrixTrimmer
    {
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
            HashSet<string> themeCheckList = new HashSet<string>(themeListFile.ThemeNameList);
            string firstTargetWord = null;
            bool couldAddTargetWord = false;
            float firstTargetWordValue = 0.0f;

            string targetWord;
            float value;
            HashSet<string> targetWordThemeList;
            foreach (KeyValuePair<string, float> targetWordAndValue in row)
            {
                couldAddTargetWord = false;

                targetWord = targetWordAndValue.Key;
                value = targetWordAndValue.Value;

                targetWordThemeList = themeListFile.GetThemeList(targetWord);

                if (HasCommonValue(themeCheckList, targetWordThemeList))
                {
                    if (firstTargetWord == null)
                    {
                        firstTargetWord = targetWord;
                        firstTargetWordValue = value;
                    }

                    //if (!totalAvailableWordCheckList.Contains(targetWord))
                    //{
                    //  totalAvailableWordCheckList.Add(targetWord);
                        trimmedMatrix.SetStatistics(sourceWord, targetWord, value);
                        removeThemeNameFromCheckList(targetWordThemeList, themeCheckList);
                        couldAddTargetWord = true;
                    //}
                }
            }

            if (!couldAddTargetWord && firstTargetWord != null)
            {
                trimmedMatrix.SetStatistics(sourceWord, firstTargetWord, firstTargetWordValue);
            }
        }

        private void removeThemeNameFromCheckList(HashSet<string> needle, HashSet<string> haystack)
        {
            foreach (string name in needle)
                haystack.Remove(name);
        }
        
        /// <summary>
        /// Returns true if sets have at least one common value
        /// </summary>
        /// <param name="set1">set 1</param>
        /// <param name="set2">set 2</param>
        /// <returns>true if sets have at least one common value</returns>
        private bool HasCommonValue(HashSet<string> set1, HashSet<string> set2)
        {
            foreach (string value in set1)
                if (set2.Contains(value))
                    return true;
            return false;
        }
        #endregion
    }
}
