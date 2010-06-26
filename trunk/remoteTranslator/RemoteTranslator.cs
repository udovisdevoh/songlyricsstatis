using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ArtificialArt.WebServices;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Remote translator
    /// </summary>
    internal static class RemoteTranslator
    {
        #region Fields and parts
        /// <summary>
        /// Translation bot
        /// </summary>
        private static BabelFishTranslationBot babelFishTranslationBot = new BabelFishTranslationBot();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Translate
        /// </summary>
        /// <param name="sourceFile">source file</param>
        /// <param name="targetFile">target file</param>
        /// <param name="sourceLanguage">from language code</param>
        /// <param name="targetLanguage">to language code</param>
        internal static void Translate(string sourceFile, string targetFile, string sourceLanguage, string targetLanguage)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}