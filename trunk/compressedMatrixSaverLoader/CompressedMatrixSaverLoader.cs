using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Saves matrix to compress files
    /// </summary>
    class CompressedMatrixSaverLoader
    {
        private const int predefinedMaxToWord = 9999;

        internal void Save(Matrix matrix, string outputFileName)
        {
            StreamWriter writer = new StreamWriter(outputFileName);


            IEnumerable<string> sortedFromWordList = from word in matrix.NormalData.Keys orderby word ascending select word;

            foreach (string fromWord in sortedFromWordList)
            {
                Dictionary<string, float> wordInfo = matrix.NormalData[fromWord];

                if (wordInfo.Count > 0)
                {
                    writer.Write(fromWord + "|");

                    int toWordCounter = 0;

                    IEnumerable<KeyValuePair<string, float>> sortedWordInfo = from entry in wordInfo orderby entry.Value descending select entry;

                    foreach (KeyValuePair<string, float> toWordAndStat in sortedWordInfo)
                    {
                        string toWord = toWordAndStat.Key;
                        float stat = toWordAndStat.Value;

                        writer.Write(toWord + ":" + stat);

                        if (toWordCounter < wordInfo.Count - 1 && toWordCounter < predefinedMaxToWord)
                        {
                            writer.Write(",");
                        }

                        toWordCounter++;

                        if (toWordCounter > predefinedMaxToWord)
                            break;
                    }

                    writer.WriteLine();
                }
            }
        }
    }
}
