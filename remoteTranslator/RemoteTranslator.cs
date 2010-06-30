using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using ArtificialArt.WebServices;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Remote translator
    /// </summary>
    internal static class RemoteTranslator
    {
        #region Constants
        /// <summary>
        /// Line count chunk size
        /// </summary>
        private const int defaultChunkSize = 100;

        /// <summary>
        /// Default from language code
        /// </summary>
        private const string fromLanguageCode = "en";

        /// <summary>
        /// Default to language code
        /// </summary>
        private const string toLanguageCode = "fr";

        /// <summary>
        /// How many retry when couldn't translate
        /// </summary>
        private const int timeOutRetry = 1;

        /// <summary>
        /// Minimum sleep time
        /// </summary>
        private const int minSleepTime = 10000;

        /// <summary>
        /// Sleep time variation
        /// </summary>
        private const int sleepTimeVariation = 1000;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Translation bot
        /// </summary>
        private static BabelFishTranslationBot babelFishTranslationBot = new BabelFishTranslationBot();

        /// <summary>
        /// Random number generator
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Current chunk size
        /// </summary>
        private static int chunkSize = defaultChunkSize;
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
            int lineIndex = GetLineIndex(targetFile + ".index");
            int currentIndex = 0;

            List<string> chunk = new List<string>();
            using (StreamReader streamReader = new StreamReader(sourceFile))
            {
                while (streamReader.Peek() >= 0)
                {
                    string line = streamReader.ReadLine().Trim().Replace(".", "").Replace("-", "");
                    line = line.Replace('�','\'');

 
                    if (currentIndex >= lineIndex)
                    {
                        chunk.Add(line);
                        if (chunk.Count >= chunkSize || streamReader.Peek() < 0)
                        {
                            IList<string> translatedChunk = null;

                            while (chunk.Count > 0)
                            {
                                try
                                {
                                    Thread.Sleep(random.Next(sleepTimeVariation) + minSleepTime);
                                    translatedChunk = babelFishTranslationBot.Translate(chunk, fromLanguageCode, toLanguageCode, true);
                                    AppendChunk(translatedChunk, targetFile);
                                    chunk.Clear();
                                }
                                catch (TranslationException)
                                {
                                    try
                                    {
                                        Thread.Sleep(random.Next(sleepTimeVariation) + minSleepTime);
                                        string sourceLine = chunk[0];
                                        chunk.RemoveAt(0);
                                        string translatedLine = babelFishTranslationBot.Translate(sourceLine, fromLanguageCode, toLanguageCode);
                                        AppendLine(translatedLine, targetFile);
                                    }
                                    catch (TranslationException)
                                    {
                                        //skip line
                                    }
                                }
                            }



                            SetLineIndex(targetFile + ".index", currentIndex);
                        }
                    }
                    currentIndex++;
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Append chunk to file
        /// </summary>
        /// <param name="translatedChunk">translated chunk list</param>
        /// <param name="targetFile">target file</param>
        private static void AppendChunk(IList<string> translatedChunk, string targetFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(targetFile, true))
            {
                foreach (string line in translatedChunk)
                {
                    streamWriter.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Append line to file
        /// </summary>
        /// <param name="translatedLine">translated line</param>
        /// <param name="targetFile">target file</param>
        private static void AppendLine(string translatedLine, string targetFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(targetFile, true))
            {
                streamWriter.WriteLine(translatedLine);
            }
        }

        private static int GetLineIndex(string indexFile)
        {
            int lineIndex = 0;

            if (File.Exists(indexFile))
            {
                using (StreamReader streamReader = new StreamReader(indexFile))
                {
                    lineIndex = int.Parse(streamReader.ReadLine());
                }
            }

            return lineIndex;
        }

        private static void SetLineIndex(string indexFile, int index)
        {
            using (StreamWriter streamWriter = new StreamWriter(indexFile))
            {
                streamWriter.WriteLine(index.ToString());
            }
        }
        #endregion
    }
}