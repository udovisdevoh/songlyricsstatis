using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    class PhoneticTableFormater
    {
        #region Fields
        private Dictionary<string, string> translationTable;
        #endregion

        #region Constructors
        public PhoneticTableFormater()
        {
            translationTable = BuildTranslationTable();
        }

        private Dictionary<string, string> BuildTranslationTable()
        {
            Dictionary<string, string> translationTable = new Dictionary<string,string>();

            for (char letter = 'a'; letter <= 'z'; letter++)
                translationTable.Add(letter.ToString(),"[" + letter + "reg]");

            translationTable.Add("-","[dash]");

            translationTable.Add("â", "[asup]");
            translationTable.Add("ê", "[esup]");
            translationTable.Add("î", "[isup]");
            translationTable.Add("ô", "[osup]");
            translationTable.Add("û", "[usup]");

            translationTable.Add("ä", "[aumlaut]");
            translationTable.Add("ë", "[eumlaut]");
            translationTable.Add("ï", "[iumlaut]");
            translationTable.Add("ö", "[oumlaut]");
            translationTable.Add("ü", "[uumlaut]");

            return translationTable;
        }
        #endregion

        #region Public Methods
        public void Reformat(string sourceFile, string targetFile)
        {
            #warning take care of words containing ' chars
            #warning Implement expanding by splitting and recombining

            IEnumerable<string> sourceLineList = GetSourceLineList(sourceFile);

            IEnumerable<string> targetLineList = Reformat(sourceLineList);

            IEnumerable<string> sortedLineList = Sort(targetLineList);

            WriteLineList(sortedLineList, targetFile);
        }
        #endregion

        #region Private Methods
        private IEnumerable<string> Reformat(IEnumerable<string> sourceLineList)
        {
            List<string> targetLineList = new List<string>();
            string newLine;
            foreach (string line in sourceLineList)
            {
                newLine = Reformat(line);
                if (newLine != null)
                {
                    targetLineList.Add(Reformat(line));
                }
            }
            return targetLineList;
        }

        private string Reformat(string line)
        {
            bool isDefinition = false;

            if (line.Contains('?') || line.Contains('#') || line.Contains('&'))
                return null;

            line = line.Replace(".gif","");

            int dept = 0;

            string newLine = string.Empty;

            foreach (char letter in line)
            {
                if (letter == ':')
                    isDefinition = true;

                if (letter == '[')
                {
                    dept++;
                    newLine += letter;
                }
                else if (letter == ']')
                {
                    dept--;
                    newLine += letter;
                }
                else
                {
                    if (dept == 0 && isDefinition)
                    {
                        newLine += GetTranslatedChar(letter.ToString());
                    }
                    else
                    {
                        newLine += letter;
                    }
                }
            }

            newLine = newLine.Replace("]","] ");
            newLine = newLine.Replace("[", " [");

            while (newLine.Contains("  "))
                newLine = newLine.Replace("  ", " ");

            newLine = newLine.Trim();

            return newLine;
        }

        private string GetTranslatedChar(string letter)
        {
            if (translationTable.ContainsKey(letter))
                letter = translationTable[letter];
            return letter;
        }

        private IEnumerable<string> GetSourceLineList(string sourceFile)
        {
            List<string> lineList = new List<string>();
            using (StreamReader streamReader = new StreamReader(sourceFile))
            {
                string line = null;

                while (true)
                {
                    line = streamReader.ReadLine();
                    if (line == null)
                        break;
                    lineList.Add(line);
                }
            }
            return lineList;
        }

        private IEnumerable<string> Sort(IEnumerable<string> targetLineList)
        {
            IEnumerable<KeyValuePair<string, string>> sortableDictionary = BuildDictionary(targetLineList);

            sortableDictionary = from item in sortableDictionary orderby item.Value select item;

            return Flatten(sortableDictionary);
        }

        private IEnumerable<KeyValuePair<string, string>> BuildDictionary(IEnumerable<string> lineList)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (string line in lineList)
            {
                string key = line.Substring(0, line.IndexOf(':')).Trim();
                string value = line.Substring(line.IndexOf(':') + 1).Trim();

                dictionary.Add(key, value);
            }
            return dictionary;
        }

        private IEnumerable<string> Flatten(IEnumerable<KeyValuePair<string, string>> dictionary)
        {
            return from item in dictionary select item.Key + " : " + item.Value;
        }

        private void WriteLineList(IEnumerable<string> targetLineList, string targetFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(targetFile))
            {
                foreach (string line in targetLineList)
                {
                    streamWriter.WriteLine(line);
                }
            }
        }
        #endregion
    }
}
