using System.Windows;
using System.Windows.Controls;

namespace Lab_04
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void anyButtonClick(object sender, RoutedEventArgs e)
        {
            new DataWin(((Button)sender).Name.ToString()).Show();
            Close();
        }
    }
}
