using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Loads theme list file
    /// </summary>
    static class ThemeLoader
    {
        #region Public Methods
        /// <summary>
        /// Loads theme list file
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>Theme list file</returns>
        public static ThemeListFile LoadThemeList(string fileName)
        {
            ThemeListFile themeListFile = new ThemeListFile(fileName);

            String currentThemeName = null;
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string line = null;
                while (true)
                {
                    line = streamReader.ReadLine();
                    if (line == null)
                        break;

                    currentThemeName = TrySwitchTheme(line, currentThemeName);

                    if (IsWordList(line) && currentThemeName != null)
                        AddWordListToTheme(line.ExtractWordList(), themeListFile.GetOrCreateTheme(currentThemeName));
                }
            }

            return themeListFile;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Whether the line is a list of words for a theme
        /// </summary>
        /// <param name="line">text line</param>
        /// <returns>whether the line is a list of words for a theme</returns>
        private static bool IsWordList(string line)
        {
            return !line.Contains('<');
        }

        /// <summary>
        /// Add word list to theme
        /// </summary>
        /// <param name="wordList">word list</param>
        /// <param name="theme">theme</param>
        private static void AddWordListToTheme(IEnumerable<string> wordList, HashSet<string> theme)
        {
            theme.UnionWith(wordList);
        }

        /// <summary>
        /// Try to switch to another theme from line
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="currentThemeName">current theme</param>
        /// <returns>old theme or new theme</returns>
        private static string TrySwitchTheme(string line, string currentThemeName)
        {
            line = line.Trim();
            line = line.Replace(" ", "");
            if (!line.StartsWith("<") || line.StartsWith("</"))
                return currentThemeName;
            else
            {
                line = line.Substring(line.IndexOf("\"") + 1);
                line = line.Substring(0, line.IndexOf("\""));
                return line;
            }
        }
        #endregion
    }
}
