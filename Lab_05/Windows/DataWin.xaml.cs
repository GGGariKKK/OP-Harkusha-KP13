using System.Windows;
using System.Windows.Controls;


namespace Lab_04
{
    public partial class DataWin : Window
    {
        public bool open;
        public DataWin(Page current, bool open = true)
        {
            this.open = open;
            InitializeComponent();
            Data.Content = current;
        }

        public MainWindow win;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (open)
            {
                win = new MainWindow();
                win.Show();
            }
        }
     
    }
}
