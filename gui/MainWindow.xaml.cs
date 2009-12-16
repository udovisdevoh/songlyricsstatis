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