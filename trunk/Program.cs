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

        private PhoneticTableFormater phoneticTableFormater = new PhoneticTableFormater();

        private PhoneticTableRepairer phoneticTableRepairer = new PhoneticTableRepairer();

        private RemainingHomophoneListExporter remainingHomophoneListExporter = new RemainingHomophoneListExporter();

        private PhoneticTableTrimmer phoneticTableTrimmer = new PhoneticTableTrimmer();

        private PhoneticTableExpander phoneticTableExpander = new PhoneticTableExpander();

        private RhymeChartBuilder rhymeChartBuilder = new RhymeChartBuilder();

        private SynonymExtractor synonymExtractor = new SynonymExtractor();

        private LyricsTrimmer lyricsTrimmer = new LyricsTrimmer();

        private LyricsSorter lyricsSorter = new LyricsSorter();

        private string lyricsFileName = null;

        private CompressedMatrixSaverLoader compressedMatrixSaverLoader = new CompressedMatrixSaverLoader();
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
            mainWindow.OnBuildPhoneticTable += BuildPhoneticTableHandler;
            mainWindow.OnFormatPhoneticTable += ReformatPhoneticTableHandler;
            mainWindow.OnRepairPhoneticTable += RepairPhoneticTableHandler;
            mainWindow.OnTrimPhoneticTable += TrimPhoneticTableHandler;
            mainWindow.OnExpandPhoneticTable += ExpandPhoneticTableHandler;
            mainWindow.OnBuildRhymeChart += BuildRhymeChartHandler;
            mainWindow.OnExtractSynonymsFromWeb += ExtractSynonymFromWebHandler;
            mainWindow.OnExtractAntonymsFromWeb += ExtractAntonymFromWebHandler;
            mainWindow.OnSelectLyricsFile += SelectLyricsFileHandler;
            mainWindow.OnTrimLyricsFile += TrimLyricsFileHandler;
            mainWindow.OnSortLyricsFile += SortLyricsFileHandler;
            mainWindow.OnSortByLengthLyricsFile += SortByLengthLyricsFileHandler;
            mainWindow.OnShuffleLyricsFile += ShuffleLyricsFileHandler;
            mainWindow.OnTranslateLyricsFile += TranslateLyricsFileHandler;
            mainWindow.OnBuildCompressedMarkovWordStatsMatrix += BuildCompressedMarkovWordStatsMatrixHandler;
            mainWindow.OnBuildStatsFromText += BuildStatsFromTextHandler;
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

        private void BuildStatsFromTextHandler(object sender, EventArgs e)
        {
            string sourceTextFileName = mainWindow.GetInputFile("SOURCE TEXT FILE|*.txt");
            string outputFileName = mainWindow.GetOutputFile("WORD STATS MATRIX FILE|*.wordStatMatrix.xml");

            if (sourceTextFileName != null && outputFileName != null)
            {
                Matrix matrix = wordMatrixExtractor.BuildMatrixFromTextFile(sourceTextFileName, null);
                xmlMatrixSaverLoader.Save(matrix, outputFileName);
            }
        }

        private void BuildStatsOnThemeWordsInTextHandler(object sender, EventArgs e)
        {
            if (currentThemeListFile == null)
                OpenThemesHandler(sender, e);
            string sourceTextFileName = mainWindow.GetInputFile("SOURCE TEXT FILE|*.txt");
            string outputFileName = mainWindow.GetOutputFile("WORD STATS MATRIX FILE|*.wordStatMatrix.xml");

            if (sourceTextFileName != null && outputFileName != null)
            {
                Matrix matrix = wordMatrixExtractor.BuildMatrixFromTextFile(sourceTextFileName, currentThemeListFile.AllAvailableWords, 1);
                xmlMatrixSaverLoader.Save(matrix, outputFileName);
            }
        }

        private void BuildCompressedMarkovWordStatsMatrixHandler(object sender, EventArgs e)
        {
            string sourceTextFileName = mainWindow.GetInputFile("SOURCE TEXT FILE|*.txt");
            string outputFileName = mainWindow.GetOutputFile("WORD STATS MATRIX FILE|*.wordStatMatrix.ini");

            if (sourceTextFileName != null && outputFileName != null)
            {
                Matrix matrix = wordMatrixExtractor.BuildMatrixFromTextFile(sourceTextFileName, null, 1);
                compressedMatrixSaverLoader.Save(matrix, outputFileName);
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
        
        private void BuildPhoneticTableHandler(object sender, EventArgs e)
        {
            if (frequentWordListFile == null)
                OpenFrequentWordListHandler(sender, e);

            string phoneticTableFile = mainWindow.GetOutputFile("PHONETIC TABLE FILE|*.phoneticTable.txt",false);

            if (phoneticTableFile != null)
                PhoneticTableBuilder.Build(frequentWordListFile, phoneticTableFile);
        }

        private void ReformatPhoneticTableHandler(object sender, EventArgs e)
        {
            string unformatedPhoneticTableFile = mainWindow.GetInputFile("PHONETIC TABLE FILE|*.phoneticTable.txt");
            string formatedPhoneticTableFile = mainWindow.GetOutputFile("FORMATED PHONETIC TABLE FILE|*.phoneticTable.txt");

            phoneticTableFormater.Reformat(unformatedPhoneticTableFile, formatedPhoneticTableFile);
        }

        private void RepairPhoneticTableHandler(object sender, EventArgs e)
        {
            string formatedPhoneticTableFile = mainWindow.GetInputFile("FORMATED PHONETIC TABLE FILE|*.phoneticTable.txt");
            string repairedPhoneticTableFile = mainWindow.GetOutputFile("REPAIRED PHONETIC TABLE FILE|*.phoneticTable.txt");
            PhoneticTable phoneticTable = new PhoneticTable(formatedPhoneticTableFile);
            phoneticTableRepairer.Repair(phoneticTable);
            phoneticTable.Save(repairedPhoneticTableFile);
            remainingHomophoneListExporter.Export(mainWindow.GetOutputFile("REMAINING HOMOPHONE LIST|*.remainingHomophoneList.txt"), phoneticTable);
        }

        private void TrimPhoneticTableHandler(object sender, EventArgs e)
        {
            string phoneticTableFile = mainWindow.GetInputFile("REPAIRED PHONETIC TABLE FILE|*.phoneticTable.txt");
            string trimmedPhoneticTableFile = mainWindow.GetOutputFile("TRIMMED PHONETIC TABLE FILE|*.phoneticTable.txt");

            PhoneticTable phoneticTable = new PhoneticTable(phoneticTableFile);
            phoneticTableTrimmer.Trim(phoneticTable);
            phoneticTable.Save(trimmedPhoneticTableFile);
        }

        private void ExpandPhoneticTableHandler(object sender, EventArgs e)
        {
            if (frequentWordListFile == null)
                OpenFrequentWordListHandler(sender, e);

            string phoneticTableFile = mainWindow.GetInputFile("TRIMMED PHONETIC TABLE FILE|*.phoneticTable.txt");
            string expandedPhoneticTableFile = mainWindow.GetOutputFile("EXPANDED PHONETIC TABLE FILE|*.phoneticTable.txt");

            PhoneticTable phoneticTable = new PhoneticTable(phoneticTableFile);
            phoneticTableExpander.Expand(phoneticTable, frequentWordListFile);
            phoneticTable.Save(expandedPhoneticTableFile);
        }

        private void BuildRhymeChartHandler(object sender, EventArgs e)
        {
            if (frequentWordListFile == null)
                OpenFrequentWordListHandler(sender, e);

            string phoneticTableFile = mainWindow.GetInputFile("PHONETIC TABLE FILE|*.phoneticTable.txt");
            string rhymeChartFile = mainWindow.GetOutputFile("RHYME CHART FILE|*.rhymeChart.txt");

            PhoneticTable phoneticTable = new PhoneticTable(phoneticTableFile);
            rhymeChartBuilder.Build(phoneticTable, frequentWordListFile, rhymeChartFile);
        }

        private void ExtractSynonymFromWebHandler(object sender, EventArgs e)
        {
            string fileName = mainWindow.GetOutputFile("XML file|*.xml");
            if (fileName != null)
                synonymExtractor.Extract("synonyms", fileName);
        }

        private void ExtractAntonymFromWebHandler(object sender, EventArgs e)
        {
            string fileName = mainWindow.GetOutputFile("XML file|*.xml");
            if (fileName != null)
                synonymExtractor.Extract("antonym", fileName);
        }

        private void SelectLyricsFileHandler(object sender, EventArgs e)
        {
            string fileName = mainWindow.GetInputFile("Text file|*.txt");
            if (fileName != null)
                lyricsFileName = fileName;
        }

        private void TrimLyricsFileHandler(object sender, EventArgs e)
        {
            if (lyricsFileName == null)
            {
                SelectLyricsFileHandler(sender, e);

                if (lyricsFileName == null)
                    return;
            }

            currentThemeListFile = new ThemeListFile();

            while (currentThemeListFile == null || currentThemeListFile.FileName == null)
                currentThemeListFile = ThemeLoader.LoadThemeList(mainWindow.GetInputFile("THEME FILE|*.themes.txt"));

            string lyricsOutputFileName;
            do
            {
                lyricsOutputFileName = mainWindow.GetOutputFile("Text file|*.txt");
            } while (lyricsOutputFileName == lyricsFileName);

            lyricsTrimmer.Trim(currentThemeListFile, lyricsFileName, lyricsOutputFileName);
        }

        private void SortLyricsFileHandler(object sender, EventArgs e)
        {
            if (lyricsFileName == null)
            {
                SelectLyricsFileHandler(sender, e);

                if (lyricsFileName == null)
                    return;
            }

            string lyricsOutputFileName;
            do
            {
                lyricsOutputFileName = mainWindow.GetOutputFile("Text file|*.txt");
            } while (lyricsOutputFileName == lyricsFileName);

            lyricsSorter.Sort(lyricsFileName, lyricsOutputFileName);
        }

        private void SortByLengthLyricsFileHandler(object sender, EventArgs e)
        {
            if (lyricsFileName == null)
            {
                SelectLyricsFileHandler(sender, e);

                if (lyricsFileName == null)
                    return;
            }

            string lyricsOutputFileName;
            do
            {
                lyricsOutputFileName = mainWindow.GetOutputFile("Text file|*.txt");
            } while (lyricsOutputFileName == lyricsFileName);

            lyricsSorter.SortByLength(lyricsFileName, lyricsOutputFileName);
        }

        private void ShuffleLyricsFileHandler(object sender, EventArgs e)
        {
            if (lyricsFileName == null)
            {
                SelectLyricsFileHandler(sender, e);

                if (lyricsFileName == null)
                    return;
            }

            string lyricsOutputFileName;
            do
            {
                lyricsOutputFileName = mainWindow.GetOutputFile("Text file|*.txt");
            } while (lyricsOutputFileName == lyricsFileName);

            lyricsSorter.Shuffle(lyricsFileName, lyricsOutputFileName);
        }

        private void TranslateLyricsFileHandler(object sender, EventArgs e)
        {
            if (lyricsFileName == null)
                SelectLyricsFileHandler(this, e);
            if (lyricsFileName == null)
                return;

            string translatedLyricsFile = mainWindow.GetOutputFile("Text file|*.txt");
            if (translatedLyricsFile == null)
                return;

            RemoteTranslator.Translate(lyricsFileName, translatedLyricsFile, "en", "fr");
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