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

namespace Lab_01
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

        private void Exit_butt_clicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Read_butt_Click(object sender, RoutedEventArgs e)
        {//Зчитування та виделення даних про студентів
            Hide();
            new Window1().Show();
        }

        private void Game_butt_Click(object sender, RoutedEventArgs e)
        {//Хрестики нулики
            Hide();
            new Window2().Show();
        }

        private void Custom_calc_butt_Click(object sender, RoutedEventArgs e)//Створений калькулятор
        {
            Hide();
            new Window3().Show();
        }
        private void sys_calc_butt_Click(object sender, RoutedEventArgs e)//Системний калькулятор
        {
            //Hide();
            System.Diagnostics.Process.Start("Calc");
        }

        private void Dev_info_butt_Click(object sender, RoutedEventArgs e)//Інформація про розробника
        {
            Hide();
            new Window4().Show();
        }
    }
}
