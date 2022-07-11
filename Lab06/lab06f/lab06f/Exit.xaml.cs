using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab06f
{
    /// <summary>
    /// Логика взаимодействия для Exit.xaml
    /// </summary>
    public partial class Exit : Window
    {
        string startPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        public Exit()
        {
            InitializeComponent();
        }
        private void Loop_MediaEnded(object sender, RoutedEventArgs e)
        {
            Loop.Position = TimeSpan.FromSeconds(0);
        }
        private void Question_MediaEnded(object sender, RoutedEventArgs e)
        {
            Question.Position = TimeSpan.FromSeconds(0);
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NO_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
    
}
