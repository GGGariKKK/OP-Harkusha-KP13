using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab_01
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Exit_butt_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Back_to_main_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(combo_data.SelectedIndex == 0)
            {
                write_remove.Content = "Write data";
                f_name.IsEnabled = true;
                f_name.Text = "";
                l_name.IsEnabled = true;
                l_name.Text = "";
                stud_id.IsEnabled = true;
                stud_id.Focusable = true;
                stud_id.Text = "";
                Keyboard.Focus(stud_id);
                write_remove.IsEnabled = true;
            }
            else
            {
                stud_id.IsEnabled = true;
                stud_id.Focusable = true;
                stud_id.Text = "";
                Keyboard.Focus(stud_id);
                f_name.IsEnabled = false;
                f_name.Text = "First name";
                l_name.IsEnabled = false;
                l_name.Text = "Last name";
                write_remove.Content = "Remove data";
                write_remove.IsEnabled = true;
            }
        }

        private void write_remove_Click(object sender, RoutedEventArgs e)
        {
            if(combo_data.SelectedIndex == 0) //додаємо нового студента
            {
                StreamWriter writer = File.AppendText("C:\\Users\\User\\Desktop\\StudentData.txt");
                String data = stud_id.Text + "/" + f_name.Text + "/" +
                    l_name.Text  + "\n"; //рядок для запису у файл
                writer.Write(data);
                writer.Close();
            }
            else //видаляємо студента з файла через його ID
            {
                StreamReader reader = new StreamReader("C:\\Users\\User\\Desktop\\StudentData.txt");
                String data = reader.ReadToEnd(); //Читаємо з файлу, щоб знайти, що видалити
                reader.Close();
                List<String> records = new List<String>(data.Split("\n")); //Список тому що зручніше буде видалити запис про студента
                for(int i = 0; i < records.Count; i++)
                {
                    if (stud_id.Text == records[i].Split("/")[0]) //Якщо запис той, що потрібний
                    {
                        records.RemoveAt(i);
                        break; //Видаляємо запис та завершуємо роботу циклу
                    }
                }
                String to_write = "";
                foreach(String temp in records) {
                    to_write += temp + "\n"; //Знову готуємо текст для запису у файл
                }
                StreamWriter writer = new StreamWriter("C:\\Users\\User\\Desktop\\StudentData.txt");
                writer.Write(to_write.Trim()); //Та записуємо, не забуваючи закрити writer
                writer.Close();
            }
        }
    }
}
