using System;
using System.Collections.Generic;
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

namespace KursachVarlamov
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        public Play()
        {
            InitializeComponent();
        }

        private void DoPlay(object sender, RoutedEventArgs e)
        {

        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("**В разработке**");
        }

        private void AboutGame(object sender, RoutedEventArgs e)
        {
            AboutPlay aboutPlay = new AboutPlay();
            aboutPlay.Show(); 
            this.Close(); 
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
