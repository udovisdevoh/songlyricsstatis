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
                        List<string> synonymOrAntonymList = GetSynonymOrAntonymList(remoteFolder, word);

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

        private List<string> GetSynonymOrAntonymList(string remoteFolder, string word)
        {
            if (remoteFolder != "antonym")
                throw new NotImplementedException("Remove folder not supported yet");

            string pageContent = GetPageContent(siteUrl + "/" + remoteFolder + "/browse/" + word);

            pageContent = pageContent.Replace("\t", " ");
            pageContent = pageContent.Replace("\r", " ");
            pageContent = pageContent.Replace("\n", " ");

            while (pageContent.Contains("  "))
                pageContent = pageContent.Replace("  ", " ");

            pageContent = pageContent.Substring(pageContent.IndexOf("<div class=\"result_set\">"));

            throw new NotImplementedException();
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
