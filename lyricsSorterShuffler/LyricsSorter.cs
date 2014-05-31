using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    public class LyricsSorter
    {
        public void Sort(string sourceLyricsFileName, string targetLyricsFileName)
        {
            List<string> lines = new List<string>();

            using (TextReader textReader = new StreamReader(sourceLyricsFileName))
            {
                using (TextWriter textWriter = new StreamWriter(targetLyricsFileName))
                {

                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        lines.Add(line);
                    }

                    lines.Sort();

                    foreach (string currentLine in lines)
                    {
                        textWriter.WriteLine(currentLine);
                    }
                }
            }
        }

        public void Shuffle(string sourceLyricsFileName, string targetLyricsFileName)
        {
            throw new NotImplementedException();
        }
    }
}
