using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ArtificialArt.Linguistics;

namespace LyricThemeClassifier
{
    class LyricsTrimmer
    {
        internal void Trim(ThemeListFile themeListFile, string lyricsInputFileName, string lyricsOutputFileName)
        {
            using (TextReader textReader = new StreamReader(lyricsInputFileName))
            {
                using (TextWriter TextWriter = new StreamWriter(lyricsOutputFileName))
                {
                    string currentLine;
                    while (true)
                    {
                        currentLine = textReader.ReadLine();

                        if (currentLine == null)
                            break;

                        currentLine = currentLine.Trim().ToLowerInvariant();

                        if (currentLine.Length > 0)
                        {

                            WordStringStream wordStringStream = new WordStringStream(currentLine);

                            bool isInTheme = false;
                            foreach (string word in wordStringStream)
                            {
                                if (themeListFile.ContainsWord(word,true))
                                {
                                    isInTheme = true;
                                    TextWriter.WriteLine(currentLine);
                                    break;
                                }
                            }
                            if (!isInTheme)
                            {
                            }
                        }
                    }
                }
            }
        }
    }
}
