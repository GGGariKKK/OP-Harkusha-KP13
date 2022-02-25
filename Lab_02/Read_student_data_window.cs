using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab_02
{
    public partial class Read_student_data_window : Window
    {
        public Read_student_data_window()
        {
            Initialization();
        }
        TextBox stud_id;
        TextBox f_name;
        TextBox l_name;
        Button write_remove;
        ComboBox combo_data;
        Window win;
        public void Initialization()
        {
            Height = 450; Width = 800;
            Title = "Read and edit student info";
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Grid grid = new Grid();
            grid.Height = 450; grid.Width = 800;
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            stud_id = Initialize.initialize_textbox(grid, "stud_id", HorizontalAlignment.Left, VerticalAlignment.Top, 18, 33, 208, 25, 61, "Student ID",  "#FF7FDEFA", false);
            f_name = Initialize.initialize_textbox(grid, "f_name", HorizontalAlignment.Left, VerticalAlignment.Top, 18, 33, 208, 25, 122, "Last name", "#FF7FDEFA", false);
            l_name = Initialize.initialize_textbox(grid, "l_name", HorizontalAlignment.Left, VerticalAlignment.Top, 18, 33, 208, 25, 187, "Student ID", "#FF7FDEFA", false);
            write_remove = Initialize.initialize_button(grid, "write_remove", "Write to a file", HorizontalAlignment.Left, VerticalAlignment.Top, 66, 167, "#FF64DBFF", "#FF4BC3CA", 344, 137, 16, false, write_remove_Click);
            combo_data = Initialize.initialize_combobox(grid, "combo_data", new string[] { "Add student data", "Remove data(with ID)" }, HorizontalAlignment.Left, VerticalAlignment.Top, 28, 167, 344, 78, ComboBox_SelectionChanged);
            Initialize.initialize_button(grid, "Back_to_main_butt_Click", "Back to main menu", HorizontalAlignment.Left, VerticalAlignment.Top, 37, 187, 0.5, "#FF00191E", "#FFFFFFFF", "#FF00191E", 46, 345, 16, true, Back_to_main_butt_Click);
            Initialize.initialize_button(grid, "Exit_butt", "Exit", HorizontalAlignment.Left, VerticalAlignment.Top, 37, 164, 0.5, "#FF00191E", "#FFFFFFFF", "#FF00191E", 586, 345, 16, true, Exit_butt_Click);

            LinearGradientBrush myLinearGradientBrush =
                new LinearGradientBrush();
            myLinearGradientBrush.StartPoint = new System.Windows.Point(0, 0);
            myLinearGradientBrush.EndPoint = new System.Windows.Point(1, 1);
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.LimeGreen, 0.0));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Blue, 0.25));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Red, 0.75));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Yellow, 1.0));

            grid.Background = myLinearGradientBrush;
            Content = grid;
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo_data.SelectedIndex == 0)
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
        public void write_remove_Click(object sender, RoutedEventArgs e)
        {
            if (combo_data.SelectedIndex == 0) //додаємо нового студента
            {
                StreamWriter writer = File.AppendText("C:\\Users\\User\\Desktop\\StudentData.txt");
                String data = stud_id.Text + "/" + f_name.Text + "/" +
                    l_name.Text + "\n"; //рядок для запису у файл
                writer.Write(data);
                writer.Close();
            }
            else //видаляємо студента з файла через його ID
            {
                StreamReader reader = new StreamReader("C:\\Users\\User\\Desktop\\StudentData.txt");
                String data = reader.ReadToEnd(); //Читаємо з файлу, щоб знайти, що видалити
                reader.Close();
                List<String> records = new List<String>(data.Split('\n')); //Список тому що зручніше буде видалити запис про студента
                for (int i = 0; i < records.Count; i++)
                {
                    if (stud_id.Text == records[i].Split('/')[0]) //Якщо запис той, що потрібний
                    {
                        records.RemoveAt(i);
                        break; //Видаляємо запис та завершуємо роботу циклу
                    }
                }
                String to_write = "";
                foreach (String temp in records)
                {
                    to_write += temp + "\n"; //Знову готуємо текст для запису у файл
                }
                StreamWriter writer = new StreamWriter("C:\\Users\\User\\Desktop\\StudentData.txt");
                writer.Write(to_write.Trim()); //Та записуємо, не забуваючи закрити writer
                writer.Close();
            }
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
    }
   
}
