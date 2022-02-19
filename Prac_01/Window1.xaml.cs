using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Prac_01
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
        private void Grid_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private String the_word;
        private int attempts;

        private void Enter_data_Click(object sender, RoutedEventArgs e)
        {
            the_word = main_word.Text.ToString();
            ComboBoxItem temp = (ComboBoxItem)attempts_selection.SelectedItem;
            attempts = Int32.Parse((string)temp.Content);
            result_matrix = new double[attempts, the_word.Length - 1];
            attempting_textbox.IsEnabled = true;
            Enter_data.IsEnabled = false;
            attempts_selection.IsEnabled = false;
            main_word.IsEnabled = false;
        }

        private int current_symb_index = 0;
        Stopwatch main_stopwatch;
        double[,] result_matrix;
        private void attempting_textbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key.ToString().ToLower() == the_word[current_symb_index].ToString())
            {//Якщо введений символ збігається зі символом зі слова-прикладу

                if (current_symb_index == 0)//Якщо перший символ
                {
                    main_stopwatch = new Stopwatch();
                    main_stopwatch.Start();
                }
                else//Не перший символ(зупинити таймер, записати та запустити)
                {
                    main_stopwatch.Stop();
                    result_matrix[attempts - 1, current_symb_index - 1] = main_stopwatch.ElapsedMilliseconds;
                    main_stopwatch.Restart();
                }
                current_symb_index++;

                if (current_symb_index == the_word.Length)//Якщо це останній введений символ
                {
                    bool failed = false;//Не пройшли спробу
                    List<double> temp_student_coeffs = Calculation.check_student_coeff(to_one_row_array(result_matrix, attempts - 1));
                    for (int i = 0; i < temp_student_coeffs.Count; i++)
                        if (temp_student_coeffs[i] > Calculation.get_student_table(result_matrix.GetLength(1) - 2, 0.01))//Якщо порахований коефіціент більший за табличний додаємо одну спробу
                        { 
                            attempts++;
                            failed = true; 
                        }
                    MessageBox.Show(failed == true? "Failed attempt" : "Done with this attempt!");

                    attempting_textbox.Text = "";
                    current_symb_index = 0;
                    attempts--;
                    e.Handled = true;
                    if(attempts == 0)//Остання спроба - зупиняємо процес навчання та вмикаємо кнопку "Зберегти результат"
                    {
                        attempting_textbox.IsEnabled = false;
                        save_results_butt.IsEnabled = true;
                    }
                }
                   

            }
            else
            {//Якщо не збігається введений символ зі символом в головному слові
                MessageBox.Show("You have made a mistake. Try again.");
                attempting_textbox.Text = "";
                current_symb_index = 0;
                e.Handled = true;
            }
        }
        public double[,] to_one_row_array(double[,] array, int row)
        {
            double[,] matrix = new double[1, array.GetLength(1)];
            for(int i = 0; i < array.GetLength(1); i++)
            {
                matrix[0, i] = array[row, i];
            }
            return matrix;
        }
        private void save_results_butt_Click(object sender, RoutedEventArgs e)
        {
            String to_write = "";
            for (int row = 0; row < result_matrix.GetLength(0); row++)
            {
                for (int column = 0; column < result_matrix.GetLength(1); column++)
                {
                    to_write += result_matrix[row, column];
                    if (column != result_matrix.GetLength(1) - 1)
                        to_write += "\t";
                }
                to_write += "\n";
            }
            to_write = to_write.Substring(0, to_write.Length - 1);
            MessageBox.Show(to_write);
            File.WriteAllText("test.txt", to_write);
        }
        private void clear_all_butt_Click(object sender, RoutedEventArgs e)
        {
            main_word.Text = "";
            attempting_textbox.Text = "";
            attempting_textbox.IsEnabled = false;
            save_results_butt.IsEnabled = false;
            main_word.IsEnabled = true;
            Enter_data.IsEnabled = true;
            attempts_selection.IsEnabled = true;
            current_symb_index = 0;
            
        }
        private void back_to_main_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }
    }

        
    }

