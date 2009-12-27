using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Phonetic table
    /// </summary>
    class PhoneticTable
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
            #warning Implement Load()
            throw new NotImplementedException();
        }

        public void Save(string fileName)
        {
            #warning Implement Save()
            throw new NotImplementedException();
        }
        #endregion
    }
}
