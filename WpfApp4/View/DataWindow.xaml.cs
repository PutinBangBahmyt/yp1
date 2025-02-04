using System;
using System.Windows;
using System.IO;

namespace WpfApp4.View
{
    /// <summary>
    /// Логика взаимодействия для DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public DataWindow()
        {
            InitializeComponent();
            DataContext = new DataViewModel(fd);
<<<<<<< HEAD
=======
            ThemeService.ThemeChanged += OnThemeChanged;
        }
        private void OnThemeChanged(object sender, string style)
        {
            // Обновить тему в данном окне
            ChangeTheme(style);
        }
        private void ChangeTheme(string style)
        {
            string themeFilePath = @"C:\Users\katrovskiiEM\Documents\Project\WpfApp4\Themes\" + style + ".xaml";

            if (File.Exists(themeFilePath))
            {
                var uri = new Uri(themeFilePath, UriKind.Absolute);
                ResourceDictionary resourceDict = new ResourceDictionary();
                resourceDict.Source = uri;

                Resources.Clear();
                Resources.MergedDictionaries.Add(resourceDict);
            }
            else
            {
                MessageBox.Show("Тема не найдена");
            }
>>>>>>> gfgd
        }
    }
}
