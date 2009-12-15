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

namespace LyricThemeClassifier
{
    /// <summary>
    /// Interaction logic for ThemeSelector.xaml
    /// </summary>
    public partial class ThemeSelector : UserControl
    {
        #region Constructors
        public ThemeSelector(string themeName)
        {
            InitializeComponent();
            this.themeName.Text = themeName;
        }
        #endregion

        #region Properties
        public string ThemeName
        {
            get { return themeName.Text; }
        }

        public bool IsChecked
        {
            get { return (bool)checkBoxIsActive.IsChecked; }
        }
        #endregion
    }
}
