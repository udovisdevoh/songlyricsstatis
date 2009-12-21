using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace LyricThemeClassifier
{
    static class PhoneticBot
    {
        #region Public Methods
        /// <summary>
        /// Translate a word into IPA
        /// </summary>
        /// <param name="fromWord">word to translate</param>
        /// <returns>translation</returns>
        public static string Translate(string fromWord)
        {
            string pageContent = GetPageContent("http://www.thefreedictionary.com/"+fromWord);

            if (!pageContent.Contains("Click for pronunciation key"))
                return null;

            pageContent = pageContent.Substring(pageContent.IndexOf("Click for pronunciation key"));

            pageContent = pageContent.Substring(pageContent.IndexOf(">"));

            pageContent = pageContent.Substring(pageContent.IndexOf("("));

            pageContent = pageContent.Substring(pageContent.IndexOf("(") + 1).Trim();

            pageContent = pageContent.Substring(0, pageContent.IndexOf("> <") +1).Trim();

            if (pageContent.Contains(')'))
                pageContent = pageContent.Substring(0, pageContent.IndexOf(")")).Trim();

            if (pageContent.Contains(';'))
                pageContent = pageContent.Substring(0, pageContent.IndexOf(";")).Trim();

            if (pageContent.Contains(','))
                pageContent = pageContent.Substring(0, pageContent.IndexOf(",")).Trim();

            pageContent = pageContent.Replace("<img align=\"absbottom\" src=\"http://img.tfd.com/hm/GIF/"," [");
            pageContent = pageContent.Replace("\">","] ");

            pageContent = pageContent.Replace("  ", " ");

            return pageContent.Trim();
        }
        #endregion

        #region Private Methods
        private static string GetPageContent(string url)
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
