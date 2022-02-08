using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Lab_01
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Button[,] buttons_matrix = new Button[5, 5];
        public Window2()
        {
            InitializeComponent();

            SolidColorBrush temp = new SolidColorBrush(Colors.Gray);//Для присвоєння всім кнопкам як фон(немає різниці який колір, адже фон прозорий)


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons_matrix[i, j] = new Button();
                    buttons_matrix[i, j].FontSize = 80;
                    buttons_matrix[i, j].FontFamily = new FontFamily("Ravie");
                    buttons_matrix[i, j].Foreground = Brushes.Red;
                    buttons_matrix[i, j].Click += butt1_Click;
                    temp.Opacity = 0;
                    buttons_matrix[i, j].Background = temp;
                    buttons_matrix[i, j].Content = "";
                    Grid.SetRow(buttons_matrix[i, j], i);
                    Grid.SetColumn(buttons_matrix[i, j], j);
                    grid.Children.Add(buttons_matrix[i, j]);
                }
            }
            
        }
        private void Exit_butt_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void to_main_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        bool computer_first;
        private void Start_butt_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button curr in buttons_matrix)
                curr.Content = ""; 
            computer_first = new Random().Next(2) == 0 ? false : true;
            if (computer_first)
                computers_turn();
        }
        private void butt1_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            String content = clicked.Content.ToString();

            if (content == "")
            {
                clicked.Content = computer_first == true ? "O" : "X";
                computers_turn();
            }
        }
        private void computers_turn()
        {
            for (int i = 0; i < buttons_matrix.GetLength(0); i++)
                for (int j = 0; j < buttons_matrix.GetLength(1); j++)
                {
                        if (buttons_matrix[i, j].Content.ToString() == "")
                        {
                            buttons_matrix[i, j].Content = computer_first == true ? "X" : "O";
                            return;
                        }
                    }
            }
        }
    }
