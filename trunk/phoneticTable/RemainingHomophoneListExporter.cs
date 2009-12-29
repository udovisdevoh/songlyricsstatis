using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    class RemainingHomophoneListExporter
    {
        #region Public Methods
        public void Export(string fileName, IEnumerable<HomophoneGroup> homophoneGroupList)
        {
            IEnumerable<HomophoneGroup> sortedList = from homophoneGroup in homophoneGroupList orderby homophoneGroup.Count descending select homophoneGroup;

            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                foreach (HomophoneGroup homophoneGroup in sortedList)
                {
                    if (homophoneGroup.Count > 1)
                    {
                        foreach (string name in homophoneGroup)
                        {
                            streamWriter.WriteLine(name);
                        }
                        streamWriter.WriteLine("");
                    }
                }
            }
        }
        #endregion
    }
}
