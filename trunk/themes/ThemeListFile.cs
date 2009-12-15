using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// This class represents a theme list file
    /// </summary>
    public class ThemeListFile : IEnumerable<string>
    {
        #region Fields
        /// <summary>
        /// Theme list
        /// </summary>
        private Dictionary<string, HashSet<string>> themeList = new Dictionary<string,HashSet<string>>();

        /// <summary>
        /// File name
        /// </summary>
        private string fileName;
        #endregion

        #region Constructors
        /// <summary>
        /// Create an empty theme list file
        /// </summary>
        public ThemeListFile()
        {
        }

        /// <summary>
        /// Load a theme list file from file name
        /// </summary>
        /// <param name="fileName">file name</param>
        public ThemeListFile(string fileName)
        {
            this.fileName = fileName;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Save a theme list file
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                string themeName;
                HashSet<string> themeWordList;
                foreach (KeyValuePair<string, HashSet<string>> theme in themeList)
                {
                    themeName = theme.Key;
                    themeWordList = theme.Value;

                    streamWriter.WriteLine("<theme name=\"" + themeName + "\">");

                    int counter = 0;
                    foreach (string word in themeWordList)
                    {
                        streamWriter.Write(word);

                        if (counter < themeWordList.Count - 1)
                            streamWriter.Write(", ");

                        counter++;
                        if (counter % 5 == 0 && counter < themeWordList.Count - 1)
                        {
                            streamWriter.Write("\r\n");
                        }
                    }

                    streamWriter.WriteLine("\r\n</theme>\r\n");
                }
            }
        }

        /// <summary>
        /// Whether the theme list contains the word in one of its themes
        /// </summary>
        /// <param name="word">word to test</param>
        /// <returns>Whether the theme list contains the word in one of its themes</returns>
        public bool ContainsWord(string word)
        {
            foreach (HashSet<string> theme in themeList.Values)
                if (theme.Contains(word))
                    return true;
            return false;
        }

        /// <summary>
        /// Get or create theme name
        /// </summary>
        /// <param name="themeName">theme name</param>
        /// <returns>New or existing theme</returns>
        public HashSet<string> GetOrCreateTheme(string themeName)
        {
            HashSet<string> theme;
            if (!themeList.TryGetValue(themeName, out theme))
            {
                theme = new HashSet<string>();
                themeList.Add(themeName, theme);
            }
            return theme;
        }

        /// <summary>
        /// Add a word to specified theme
        /// </summary>
        /// <param name="themeName">theme name</param>
        /// <param name="wordToAdd">word to addd</param>
        public void AddWord(string themeName, string wordToAdd)
        {
            GetOrCreateTheme(themeName).Add(wordToAdd);
        }
        #endregion

        #region Properties
        /// <summary>
        /// File name
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// All available words from every themes
        /// </summary>
        public ICollection<string> AllAvailableWords
        {
            get
            {
                HashSet<string> availableWords = new HashSet<string>();
                IEnumerable<string> currentTheme;
                string themeName;
                foreach (KeyValuePair<string, HashSet<string>> nameAndTheme in themeList)
                {
                    themeName = nameAndTheme.Key;
                    currentTheme = nameAndTheme.Value;
                    if (themeName.ToLower().Trim() != "none")
                    {
                        availableWords.UnionWith(currentTheme);
                    }
                }
                return availableWords;
            }
        }
        #endregion

        #region IEnumerable<string> Members
        public IEnumerator<string> GetEnumerator()
        {
            return themeList.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return themeList.Keys.GetEnumerator();
        }
        #endregion
    }
}