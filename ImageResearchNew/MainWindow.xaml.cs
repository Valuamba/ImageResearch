using ImageResearchNew.AppWindows;
using ImageResearchNew.Classes;
using ImageResearchNew.Helpers;
using ImageResearchNew.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageResearchNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += UnhandledException.DispatcherUnhandledException1;
        }

        private void Settings_ClickMenuItem(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow();

            settings.DataContext = Settings.Instance;

            settings.ShowDialog();
        }
    }
}
