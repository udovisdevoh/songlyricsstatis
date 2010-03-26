using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Remotely extract synonyms and antonyms
    /// </summary>
    internal class SynonymExtractor
    {
        #region Constants
        /// <summary>
        /// Site's url
        /// </summary>
        private const string siteUrl = "http://www.synonym.com";
        #endregion

        #region Parts
        private XmlMatrixSaverLoader xmlMatrixSaverLoader = new XmlMatrixSaverLoader();
        #endregion

        #region Protected methods
        /// <summary>
        /// Remotely extract synonyms and antonyms to a file
        /// </summary>
        /// <param name="remoteFolder">which remove folder (synonyms or antonym)</param>
        /// <param name="xmlFileName">output xml file</param>
        internal void Extract(string remoteFolder, string xmlFileName)
        {
            Matrix matrix;

            if (File.Exists(xmlFileName))
                matrix = xmlMatrixSaverLoader.Load(xmlFileName);
            else
                matrix = new Matrix();

            List<string> browsingLetterList = GetBrowsingLetterList(remoteFolder);

            foreach (string browsingLetter in browsingLetterList)
            {
                List<string> wordList = GetWordList(remoteFolder, browsingLetter);
                foreach (string word in wordList)
                {
                    if (!matrix.ContainsKey(word))
                    {
                        HashSet<string> synonymOrAntonymList = GetSynonymOrAntonymList(remoteFolder, word);

                        foreach (string otherWord in synonymOrAntonymList)
                        {
                            matrix.AddStatistics(word, otherWord);
                        }
                    }
                }
            }

            xmlMatrixSaverLoader.Save(matrix, xmlFileName);
        }
        #endregion

        #region Private Methods
        private List<string> GetBrowsingLetterList(string remoteFolder)
        {
            string pageContent = GetPageContent(siteUrl + "/" + remoteFolder + "/browse");

            pageContent = pageContent.Replace("\t", " ");
            pageContent = pageContent.Replace("\r", " ");
            pageContent = pageContent.Replace("\n", " ");

            while (pageContent.Contains("  "))
                pageContent = pageContent.Replace("  ", " ");

            pageContent = pageContent.Substring(pageContent.IndexOf("<h1 class=\"Title\">"));

            pageContent = pageContent.Substring(0,pageContent.IndexOf("<div"));

            List<string> browsingLetterList = new List<string>();

            string[] rawData = pageContent.Split('<');

            
            foreach (string linkInfo in rawData)
            {
                if (linkInfo.Contains("a href="))
                {
                    string trimmedData = linkInfo;
                    trimmedData = trimmedData.Substring(trimmedData.IndexOf("a href="));
                    trimmedData = trimmedData.Substring(trimmedData.IndexOf('>') + 1);
                    browsingLetterList.Add(trimmedData);
                }
            }


            return browsingLetterList;
        }

        private HashSet<string> GetSynonymOrAntonymList(string remoteFolder, string sourceWord)
        {
            if (remoteFolder == "antonym")
            {
                return GetAntonymList(remoteFolder, sourceWord);
            }
            else if (remoteFolder == "synonyms")
            {
                return GetSynonymList(remoteFolder, sourceWord);
            }
            else
            {
                throw new NotImplementedException("unsupported remove folder");
            }
        }

        private HashSet<string> GetSynonymList(string remoteFolder, string sourceWord)
        {
            string pageContent = GetPageContent(siteUrl + "/" + remoteFolder + "/" + sourceWord);

            pageContent = pageContent.Replace("\t", " ");
            pageContent = pageContent.Replace("\r", " ");
            pageContent = pageContent.Replace("\n", " ");

            while (pageContent.Contains("  "))
                pageContent = pageContent.Replace("  ", " ");

            pageContent = pageContent.Substring(pageContent.IndexOf("<div class=\"result_set\">"));

            HashSet<string> synonymOrAntonymList = new HashSet<string>();

            string previousChunk = null;
            string[] chunkList = pageContent.Split('<');
            string formatedChunk = null;

            foreach (string chunk in chunkList)
            {
                previousChunk = formatedChunk;
                formatedChunk = chunk;

                if (formatedChunk.StartsWith("span class=\"equals\">"))
                {
                    formatedChunk = formatedChunk.Substring(20);
                }
                else if (previousChunk != null && previousChunk.StartsWith("div class=\"Accent Sense\">Sense ") && formatedChunk.StartsWith("/div>"))
                {
                    formatedChunk = formatedChunk.Substring(formatedChunk.IndexOf('>')+1);
                }
                else
                {
                    continue;
                }

                if (formatedChunk.Contains(","))
                {
                    string[] formatedChunkList = formatedChunk.Split(',');
                    foreach (string subChunk in formatedChunkList)
                    {
                        string formatedSubChunk = subChunk.Trim();
                        if (formatedSubChunk.Length > 0)
                        {
                            formatedSubChunk = formatedSubChunk.ToLower();
                            if (sourceWord != formatedSubChunk)
                                synonymOrAntonymList.Add(formatedSubChunk);
                        }
                    }
                }
                else
                {
                    formatedChunk = formatedChunk.ToLower().Trim();
                    if (sourceWord != formatedChunk)
                        synonymOrAntonymList.Add(formatedChunk);
                }

                
            }

            return synonymOrAntonymList;
        }

        private HashSet<string> GetAntonymList(string remoteFolder, string sourceWord)
        {
            string pageContent = GetPageContent(siteUrl + "/" + remoteFolder + "/" + sourceWord);

            pageContent = pageContent.Replace("\t", " ");
            pageContent = pageContent.Replace("\r", " ");
            pageContent = pageContent.Replace("\n", " ");
            pageContent = pageContent.Replace("-->", "badArrow");
            pageContent = pageContent.Replace("==>", "badArrow");
            pageContent = pageContent.Replace("-=>", "badArrow");
            pageContent = pageContent.Replace("=->", "badArrow");
            pageContent = pageContent.Replace("->", "[arrow]");
            pageContent = pageContent.Replace("=>", "[arrow]");

            while (pageContent.Contains("  "))
                pageContent = pageContent.Replace("  ", " ");

            pageContent = pageContent.Substring(pageContent.IndexOf("<div class=\"result_set\">"));

            HashSet<string> synonymOrAntonymList = new HashSet<string>();

            string[] chunkList = pageContent.Split('>');

            foreach (string chunk in chunkList)
            {
                string formatedChunk = chunk;

                if (formatedChunk.Contains(" [arrow] "))
                {
                    if (formatedChunk.Contains("<"))
                        formatedChunk = formatedChunk.Substring(0, formatedChunk.LastIndexOf("<"));

                    formatedChunk = formatedChunk.Substring(formatedChunk.IndexOf("[arrow]") + 7);
                    formatedChunk = formatedChunk.Trim();

                    if (formatedChunk.Contains(","))
                    {
                        string[] formatedChunkList = formatedChunk.Split(',');
                        foreach (string subChunk in formatedChunkList)
                        {
                            string formatedSubChunk = subChunk.Trim();
                            if (formatedSubChunk.Length > 0)
                            {
                                synonymOrAntonymList.Add(formatedSubChunk.ToLower());
                            }
                        }
                    }
                    else
                    {
                        synonymOrAntonymList.Add(formatedChunk.ToLower());
                    }
                }
                /*else if (formatedChunk.Contains("(vs. ") && !formatedChunk.Contains("(vs. "+sourceWord))
                {
                    if (formatedChunk.Contains("<"))
                        formatedChunk = formatedChunk.Substring(0, formatedChunk.IndexOf("<"));

                    //"abactinal (vs. actinal)"

                    formatedChunk = formatedChunk.Replace("(vs. ", "");
                    formatedChunk = formatedChunk.Replace(")", "");
                    formatedChunk = formatedChunk.Replace(",", "");
                    formatedChunk = formatedChunk.Trim();

                    string[] wordList = formatedChunk.Split(' ');

                    foreach (string currentWord in wordList)
                    {
                        if (currentWord != sourceWord)
                        {
                            synonymOrAntonymList.Add(currentWord);
                        }
                    }

                }*/
            }

            return synonymOrAntonymList;
        }

        private List<string> GetWordList(string remoteFolder, string browsingLetter)
        {
            string pageContent = GetPageContent(siteUrl + "/" + remoteFolder + "/browse/" + browsingLetter);

            pageContent = pageContent.Replace("\t", " ");
            pageContent = pageContent.Replace("\r", " ");
            pageContent = pageContent.Replace("\n", " ");

            while (pageContent.Contains("  "))
                pageContent = pageContent.Replace("  ", " ");

            pageContent = pageContent.Substring(pageContent.IndexOf("<ul>"));

            pageContent = pageContent.Substring(0, pageContent.IndexOf("</ul>"));

            List<string> wordList = new List<string>();

            string[] rawData = pageContent.Split('<');


            foreach (string linkInfo in rawData)
            {
                if (linkInfo.Contains("a href="))
                {
                    string trimmedData = linkInfo;
                    trimmedData = trimmedData.Substring(trimmedData.IndexOf("a href=\""));
                    trimmedData = trimmedData.Substring(trimmedData.IndexOf("\""));
                    trimmedData = trimmedData.Substring(trimmedData.IndexOf(remoteFolder) + remoteFolder.Length + 1);
                    trimmedData = trimmedData.Substring(0,trimmedData.IndexOf('/'));
                    wordList.Add(trimmedData);
                }
            }


            return wordList;
        }

        /// <summary>
        /// Get page content
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        private string GetPageContent(string url)
        {
            // used to build entire input
            StringBuilder content = new StringBuilder();
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            // used on each read operation
            byte[] buf = new byte[8192];


            request = (HttpWebRequest)WebRequest.Create(url);
            if (request != null)
            {
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch
                {
                }
            }

            if (response != null)
            {
                Stream resStream = response.GetResponseStream();


                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        //tempString = Encoding.ASCII.GetString(buf, 0, count);
                        tempString = Encoding.UTF8.GetString(buf, 0, count);

                        // continue building the string
                        content.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?
            }

            return content.ToString();
        }
        #endregion
    }
}
