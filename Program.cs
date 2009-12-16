using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LyricThemeClassifier
{
    /// <summary>
    /// Program's main controller
    /// </summary>
    class Program
    {
        #region Fields
        /// <summary>
        /// Program's main window
        /// </summary>
        private MainWindow mainWindow = new MainWindow();

        /// <summary>
        /// Current theme list file
        /// </summary>
        private ThemeListFile currentThemeListFile;

        /// <summary>
        /// File that contains list of most frequent words
        /// </summary>
        private WordListFile frequentWordListFile = null;

        /// <summary>
        /// Word matrix extractor
        /// </summary>
        private WordMatrixExtractor wordMatrixExtractor = new WordMatrixExtractor();

        /// <summary>
        /// Xml matrix saver / loader
        /// </summary>
        private XmlMatrixSaverLoader xmlMatrixSaverLoader = new XmlMatrixSaverLoader();

        /// <summary>
        /// Semantic likeness matrix builder
        /// </summary>
        private SemanticLikenessMatrixBuilder semanticLikenessMatrixBuilder = new SemanticLikenessMatrixBuilder();

        /// <summary>
        /// Semantic likeness matrix trimmer
        /// </summary>
        private SemanticMatrixTrimmer semanticMatrixTrimmer = new SemanticMatrixTrimmer();
        #endregion

        #region Constructor
        /// <summary>
        /// Create new program instance
        /// </summary>
        public Program()
        {
            mainWindow.OnExit += ExitHandler;
            mainWindow.OnOpenThemes += OpenThemesHandler;
            mainWindow.OnSaveThemes += SaveThemesHandler;
            mainWindow.OnBuildFrequentWordList += BuildFrequentWordListHandler;
            mainWindow.OnContinueSorting += ContinueSortingHandler;
            mainWindow.OnOpenFrequentWordList += OpenFrequentWordListHandler;
            mainWindow.OnSkip += ContinueSortingHandler;
            mainWindow.OnNext += NextHandler;
            mainWindow.OnBuildStatsOnThemeWordsInText += BuildStatsOnThemeWordsInTextHandler;
            mainWindow.OnBuildSemanticLikenessMatrix += BuildSemanticLikenessMatrixHandler;
            mainWindow.OnTrimSemanticLikenessMatrix += TrimSemanticLikenessMatrixHandler;
        }
        #endregion

        #region Handlers
        private void ExitHandler(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenThemesHandler(object sender, EventArgs e)
        {
            string fileName = mainWindow.GetInputFile("THEME FILE|*.themes.txt");
            if (fileName != null)
            {
                currentThemeListFile = ThemeLoader.LoadThemeList(fileName);
            }
        }

        private void SaveThemesHandler(object sender, EventArgs e)
        {
            if (currentThemeListFile.FileName == null)
                currentThemeListFile.FileName = mainWindow.GetOutputFile("THEME FILE|*.themes.txt");

            if (currentThemeListFile.FileName != null)
                currentThemeListFile.Save(currentThemeListFile.FileName);
        }

        private void BuildFrequentWordListHandler(object sender, EventArgs e)
        {
            string sourceFileName = mainWindow.GetInputFile("SOURCE TEXT FILE|*.txt");
            if (sourceFileName != null)
            {
                string wordListFileName = mainWindow.GetOutputFile("WORD LIST FILE|*.wordList.txt");

                if (wordListFileName != null)
                {
                    frequentWordListFile = WordListBuilder.Build(sourceFileName, wordListFileName);
                    frequentWordListFile.Save(wordListFileName);
                }
            }
        }

        private void ContinueSortingHandler(object sender, EventArgs e)
        {
            if (currentThemeListFile == null)
                OpenThemesHandler(sender, e);

            if (frequentWordListFile == null)
                OpenFrequentWordListHandler(sender, e);

            if (currentThemeListFile != null && frequentWordListFile != null)
            {
                mainWindow.ShowThemeSortingForm(currentThemeListFile, frequentWordListFile.GetNextWordNotInTheme(currentThemeListFile));
            }
        }

        private void OpenFrequentWordListHandler(object sender, EventArgs e)
        {
            string fileName = mainWindow.GetInputFile("WORD LIST FILE|*.wordList.txt");
            if (fileName != null)
            {
                frequentWordListFile = new WordListFile(fileName);
                frequentWordListFile.Load(fileName);
            }
        }

        private void NextHandler(object sender, EventArgs e)
        {
            foreach (string themeName in mainWindow.SelectedThemeNameList)
            {
                currentThemeListFile.AddWord(themeName, mainWindow.CurrentWord);
            }
            ContinueSortingHandler(sender, e);
        }

        private void BuildStatsOnThemeWordsInTextHandler(object sender, EventArgs e)
        {
            if (currentThemeListFile == null)
                OpenThemesHandler(sender, e);
            string sourceTextFileName = mainWindow.GetInputFile("SOURCE TEXT FILE|*.txt");
            string outputFileName = mainWindow.GetOutputFile("WORD STATS MATRIX FILE|*.wordStatMatrix.xml");

            if (sourceTextFileName != null && outputFileName != null)
            {
                Matrix matrix = wordMatrixExtractor.BuildMatrixFromTextFile(sourceTextFileName, currentThemeListFile.AllAvailableWords);
                xmlMatrixSaverLoader.Save(matrix, outputFileName);
            }
        }

        private void BuildSemanticLikenessMatrixHandler(object sender, EventArgs e)
        {
            if (currentThemeListFile == null)
                OpenThemesHandler(sender, e);
            string sourceWordStatMatrixFile = mainWindow.GetInputFile("WORD STATS MATRIX FILE|*.wordStatMatrix.xml");

            string semanticMatrixFile = mainWindow.GetOutputFile("SEMANTIC MATRIX FILE|*.semanticMatrix.xml");

            if (sourceWordStatMatrixFile != null)
            {
                Matrix wordStatMatrix = xmlMatrixSaverLoader.Load(sourceWordStatMatrixFile);
                Matrix semanticLikenessMatrix = semanticLikenessMatrixBuilder.Build(wordStatMatrix, currentThemeListFile.AllAvailableWords);

                xmlMatrixSaverLoader.Save(semanticLikenessMatrix, semanticMatrixFile);
            }
        }

        private void TrimSemanticLikenessMatrixHandler(object sender, EventArgs e)
        {
            if (currentThemeListFile == null)
                OpenThemesHandler(sender, e);
            string rawSemanticMatrixFile = mainWindow.GetInputFile("SEMANTIC MATRIX FILE|*.semanticMatrix.xml");
            string trimmedSemanticMatrixFile = mainWindow.GetOutputFile("TRIMMED SEMANTIC MATRIX FILE|*.trimmedSemanticMatrix.xml");

            if (currentThemeListFile != null && rawSemanticMatrixFile != null && trimmedSemanticMatrixFile != null)
            {
                Matrix rawSemanticMatrix = xmlMatrixSaverLoader.Load(rawSemanticMatrixFile);
                Matrix trimmedSemanticMatrix = semanticMatrixTrimmer.Trim(rawSemanticMatrix,currentThemeListFile);

                xmlMatrixSaverLoader.Save(trimmedSemanticMatrix, trimmedSemanticMatrixFile);
            }
        }
        #endregion

        #region Main
        [STAThread]
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        private void Start()
        {
            Application application = new Application();
            application.Run(mainWindow);
        }
        #endregion
    }
}