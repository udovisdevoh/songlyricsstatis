using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        public event EventHandler OnContinueSorting;

        public event EventHandler OnOpenThemes;

        public event EventHandler OnSaveThemes;

        public event EventHandler OnOpenFrequentWordList;

        public event EventHandler OnBuildFrequentWordList;

        public event EventHandler OnNext;

        public event EventHandler OnSkip;

        public event EventHandler OnExit;

        public event EventHandler OnBuildStatsOnThemeWordsInText;

        public event EventHandler OnBuildSemanticLikenessMatrix;

        public event EventHandler OnTrimSemanticLikenessMatrix;

        public event EventHandler OnBuildPhoneticTable;

        public event EventHandler OnFormatPhoneticTable;

        public event EventHandler OnRepairPhoneticTable;

        public event EventHandler OnTrimPhoneticTable;

        public event EventHandler OnExpandPhoneticTable;

        public event EventHandler OnBuildRhymeChart;

        public event EventHandler OnExtractSynonymsFromWeb;

        public event EventHandler OnExtractAntonymsFromWeb;

        public event EventHandler OnSelectLyricsFile;

        public event EventHandler OnTrimLyricsFile;

        public event EventHandler OnSortLyricsFile;

        public event EventHandler OnShuffleLyricsFile;

        public event EventHandler OnTranslateLyricsFile;

        public event EventHandler OnBuildCompressedMarkovWordStatsMatrix;

        public event EventHandler OnBuildStatsFromText;
        #endregion

        #region Handlers
        private void MenuItemConginueSorting_Click(object sender, RoutedEventArgs e)
        {
            if (OnContinueSorting != null) OnContinueSorting(this, e);
        }

        private void MenuItemOpenThemes_Click(object sender, RoutedEventArgs e)
        {
            if (OnOpenThemes != null) OnOpenThemes(this, e);
        }

        private void MenuItemSaveThemes_Click(object sender, RoutedEventArgs e)
        {
            if (OnSaveThemes != null) OnSaveThemes(this, e);
        }

        private void MenuItemBuildFrequendWordList_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildFrequentWordList != null) OnBuildFrequentWordList(this, e);
        }

        private void MenuItemOpenFrequendWordList_Click(object sender, RoutedEventArgs e)
        {
            if (OnOpenFrequentWordList != null) OnOpenFrequentWordList(this, e);
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            if (OnExit != null) OnExit(this, e);
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (OnNext != null) OnNext(this, e);
        }

        private void ButtonSkip_Click(object sender, RoutedEventArgs e)
        {
            if (OnSkip != null) OnSkip(this, e);
        }

        private void MenuItemBuildMarkovWordStatsMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildStatsFromText != null) OnBuildStatsFromText(this, e);
        }

        private void MenuItemBuildStatsOnThemeWordsInText_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildStatsOnThemeWordsInText != null) OnBuildStatsOnThemeWordsInText(this, e);
        }

        private void MenuItemBuildSemanticLikenessMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildSemanticLikenessMatrix != null) OnBuildSemanticLikenessMatrix(this, e);
        }

        private void MenuItemTrimSemanticLikenessMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (OnTrimSemanticLikenessMatrix != null) OnTrimSemanticLikenessMatrix(this, e);
        }

        private void MenuItemBuildPhoneticTable_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildPhoneticTable != null) OnBuildPhoneticTable(this, e);
        }

        private void MenuItemFormatPhoneticTable_Click(object sender, RoutedEventArgs e)
        {
            if (OnFormatPhoneticTable != null) OnFormatPhoneticTable(this, e);
        }

        private void MenuItemRepairPhoneticTable_Click(object sender, RoutedEventArgs e)
        {
            if (OnRepairPhoneticTable != null) OnRepairPhoneticTable(this, e);
        }

        private void MenuItemTrimPhoneticTable_Click(object sender, RoutedEventArgs e)
        {
            if (OnTrimPhoneticTable != null) OnTrimPhoneticTable(this, e);
        }

        private void MenuItemExpandPhoneticTable_Click(object sender, RoutedEventArgs e)
        {
            if (OnExpandPhoneticTable != null) OnExpandPhoneticTable(this, e);
        }

        private void MenuItemBuildRhymeChart_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildRhymeChart != null) OnBuildRhymeChart(this, e);
        }

        private void MenuItemExtractSynonymsFromWeb_Click(object sender, RoutedEventArgs e)
        {
            if (OnExtractSynonymsFromWeb != null) OnExtractSynonymsFromWeb(this, e);
        }

        private void MenuItemExtractAntonymsFromWeb_Click(object sender, RoutedEventArgs e)
        {
            if (OnExtractAntonymsFromWeb != null) OnExtractAntonymsFromWeb(this, e);
        }

        private void MenuItemSelectLyricsFile_Click(object sender, RoutedEventArgs e)
        {
            if (OnSelectLyricsFile != null) OnSelectLyricsFile(this, e);
        }

        private void MenuItemTrimLyricsFile_Click(object sender, RoutedEventArgs e)
        {
            if (OnTrimLyricsFile != null) OnTrimLyricsFile(this, e);
        }

        private void MenuItemSortLyricsFile_Click(object sender, RoutedEventArgs e)
        {
            if (OnSortLyricsFile != null) OnSortLyricsFile(this, e);
        }

        private void MenuItemShuffleLyricsFile_Click(object sender, RoutedEventArgs e)
        {
            if (OnShuffleLyricsFile != null) OnShuffleLyricsFile(this, e);
        }

        private void MenuItemTranslateLyricsFile_Click(object sender, RoutedEventArgs e)
        {
            if (OnTranslateLyricsFile != null) OnTranslateLyricsFile(this, e);
        }

        private void MenuItemBuildCompressedMarkovWordStatsMatrix_Click(object sender, RoutedEventArgs e)
        {
            if (OnBuildCompressedMarkovWordStatsMatrix != null) OnBuildCompressedMarkovWordStatsMatrix(this, e);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get input file
        /// </summary>
        /// <param name="filter">file filter</param>
        /// <returns>input file</returns>
        public string GetInputFile(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (filter != null)
                dialog.Filter = filter;
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;

            dialog.ShowDialog();

            if (dialog.FileName == "")
                return null;

            return dialog.FileName;
        }

        /// <summary>
        /// Get output file
        /// </summary>
        /// <param name="filter">file filter</param>
        /// <returns>output file</returns>
        public string GetOutputFile(string filter)
        {
            return GetOutputFile(filter, true);
        }

        /// <summary>
        /// Get output file
        /// </summary>
        /// <param name="filter">file filter</param>
        /// <param name="overWritePrompt">ask to overwrite</param>
        /// <returns>output file</returns>
        public string GetOutputFile(string filter, bool overWritePrompt)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (filter != null)
                dialog.Filter = filter;
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            dialog.OverwritePrompt = overWritePrompt;

            dialog.ShowDialog();

            if (dialog.FileName == "")
                return null;

            return dialog.FileName;
        }

        /// <summary>
        /// Show theme sorting form
        /// </summary>
        /// <param name="currentThemeListFile">current theme list file</param>
        /// <param name="word">word</param>
        public void ShowThemeSortingForm(ThemeListFile currentThemeListFile, string word)
        {
            this.currentWord.Text = word;
            if (themeSelectorLayout.Children.Count < 1)
            {
                IEnumerable<string> sortedThemeNameList = from themeName in currentThemeListFile orderby themeName select themeName;

                foreach (string themeName in sortedThemeNameList)
                    addThemeSelector(new ThemeSelector(themeName));
            }
        }
        #endregion

        #region Private Methods
        private void addThemeSelector(ThemeSelector themeSelector)
        {
            themeSelectorLayout.Children.Add(themeSelector);
        }
        #endregion

        #region Properties
        public string CurrentWord
        {
            get { return currentWord.Text; }
        }

        public IEnumerable<string> SelectedThemeNameList
        {
            get
            {
                List<string> selectedThemeNameList = new List<string>();
                ThemeSelector themeSelector;
                foreach (UIElement element in this.themeSelectorLayout.Children)
                {
                    themeSelector = (ThemeSelector)element;

                    if (themeSelector.IsChecked)
                    {
                        selectedThemeNameList.Add(themeSelector.ThemeName);
                    }
                }

                return selectedThemeNameList;
            }
        }
        #endregion
    }
}