using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Phonetic table
    /// </summary>
    class PhoneticTable : IEnumerable<HomophoneGroup>
    {
        #region Fields
        /// <summary>
        /// Key: phonetic word
        /// Value: homophone group
        /// </summary>
        private Dictionary<string, HomophoneGroup> homophoneDictionary;

        /// <summary>
        /// Key: english word
        /// Value: homophone groupe
        /// </summary>
        private Dictionary<string, HomophoneGroup> phonologicDictionary;
        #endregion

        #region Constructors
        /// <summary>
        /// Create phonetic table
        /// </summary>
        public PhoneticTable()
        {
            homophoneDictionary = new Dictionary<string, HomophoneGroup>();
            phonologicDictionary = new Dictionary<string, HomophoneGroup>();
        }

        /// <summary>
        /// Create phonetic table from file
        /// </summary>
        /// <param name="fileName">file name</param>
        public PhoneticTable(string fileName) : this()
        {
            Load(fileName);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load file
        /// </summary>
        /// <param name="fileName">file name</param>
        public void Load(string fileName)
        {
            IEnumerable<string> sourceLineList = GetSourceLineList(fileName);

            string key, value;
            foreach (string line in sourceLineList)
            {
                key = line.Substring(0, line.IndexOf(':')).Trim();
                value = line.Substring(line.IndexOf(':') + 1).Trim();
                Add(key,value);
            }
        }

        public void Save(string fileName)
        {
            #warning Implement Save()
            throw new NotImplementedException();
        }

        public void Add(string englishValue, string phoneticValue)
        {
            HomophoneGroup homophoneGroup = GetOrCreateHomophoneGroup(phoneticValue);
            homophoneGroup.Add(englishValue);
            if (phonologicDictionary.ContainsKey(englishValue))
                phonologicDictionary[englishValue] = homophoneGroup;
            else
                phonologicDictionary.Add(englishValue, homophoneGroup);
        }
        #endregion

        #region Private Methods
        private HomophoneGroup GetOrCreateHomophoneGroup(string phoneticValue)
        {
            HomophoneGroup homophoneGroup;
            if (!homophoneDictionary.TryGetValue(phoneticValue, out homophoneGroup))
            {
                homophoneGroup = new HomophoneGroup(phoneticValue);
                homophoneDictionary.Add(phoneticValue, homophoneGroup);
            }
            return homophoneGroup;
        }

        private IEnumerable<string> GetSourceLineList(string fileName)
        {
            List<string> lineList = new List<string>();
            using (StreamReader streamReader = new StreamReader(fileName))
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
        #endregion

        #region IEnumerable<HomophoneGroup> Members
        public IEnumerator<HomophoneGroup> GetEnumerator()
        {
            return this.homophoneDictionary.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.homophoneDictionary.Values.GetEnumerator();
        }
        #endregion
    }
}
