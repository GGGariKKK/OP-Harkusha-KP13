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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prac_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void admin_Click(object sender, RoutedEventArgs e)
        {
            new AdminWin().Show();
            this.Close();
        }

        private void user_Click(object sender, RoutedEventArgs e)
        {
            new UserWin().Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e) => this.Close();

        private void devInfo_Click(object sender, RoutedEventArgs e)
        {
            new DevInfo().Show();
            this.Close();           
        }
    }
}
