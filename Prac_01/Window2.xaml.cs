using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Prac_01
{
    public partial class Window2 : Window
    {
        double[,] ethalon_intervals;//інтервали з режиму навчання
        public Window2()
        {
            InitializeComponent();
            read_intervals_info();
        }
        public void read_intervals_info()//Зчитування еталонних даних з файлу
        {
            String all_data = File.ReadAllText("test.txt");//Зчитуємо з файлу еталонних даних
            String[] records = all_data.Split("\n");//Розділяємо на рядки
            ethalon_intervals = new double[records.Length, records[0].Split("\t").Length];//Створюємо матрицю з отриманих з файлу даних(еталонні дані)
            for (int i = 0; i < ethalon_intervals.GetLength(0); i++)
            {
                String[] temp = records[i].Split("\t");//Розділяємо кожен рядок на  інтервали
                for (int j = 0; j < ethalon_intervals.GetLength(1); j++)
                {
                    ethalon_intervals[i, j] = double.Parse(temp[j]) * 1.0 / 1000;//Записуємо в матрицю в секундах
                }
            }
        }
        private void back_to_main_Click(object sender, RoutedEventArgs e)
        {//Повернення до головного меню
            Hide();
            new MainWindow().Show();
        }
        private void enter_info_butt_Click(object sender, RoutedEventArgs e)
        {
            attempts = Int32.Parse(enter_attempts1.Text.ToString());
            alpha = Double.Parse(alpha_textbox1.Text.ToString());
            main_input1.IsEnabled = true;
            the_word = code_word1.Text.ToString();
            user_intervals = new double[attempts, the_word.Length - 1];
            disps1.Content = ""; P_.Content = ""; P1.Content = ""; P2.Content = "";
        }

        private void calculate_fisher_coeff()
        {
            (List<double> ethalon_dispersion, List<double> temp) = Calculation.calculate_full_dispersion(ethalon_intervals);
            (List<double> user_dispersion, List<double> temp1) = Calculation.calculate_full_dispersion(user_intervals);
            int good_disps = 0;
            foreach (double eth in ethalon_dispersion)
                foreach (double user in user_dispersion)
                    if ((Math.Max(eth, user) / Math.Min(eth, user)) < Calculation.get_fisher_coeff(ethalon_intervals.GetLength(1) - 1))
                        good_disps++;

            if (good_disps >= (ethalon_dispersion.Count * user_dispersion.Count / 2)){
                disps1.Content = "Однорідні";
            }
            else
               disps1.Content = "Не однорідні";
            check_student_coeff();
        }
        private void check_student_coeff()
        {//Розраховуємо коефіцієнт Стьюдента матриці інтервалів, яка задана як параметр в методі

            (List<double> disps_eth, List<double> eth_m_e) = Calculation.calculate_full_dispersion(ethalon_intervals);
            (List<double> disps_user, List<double> user_m_e) = Calculation.calculate_full_dispersion(user_intervals);

            int positive_results = 0;

            for (int i = 0; i < disps_eth.Count; i++)
            {
                for (int j = 0; j < disps_user.Count; j++)
                {
                    double S = Math.Sqrt((disps_eth[i] + disps_user[j]) * (user_intervals.GetLength(1) - 1) / (2 * user_intervals.GetLength(1) - 1));
                    double stud_coeff = Math.Abs(eth_m_e[i] - user_m_e[j]) / (S * Math.Sqrt(2.0 / user_intervals.GetLength(1)));
                    if (stud_coeff < Calculation.get_student_table(user_intervals.GetLength(1) - 1, alpha)) positive_results++;
                }
            }
            P_.Content = Math.Round(1.0 * positive_results / (disps_eth.Count * disps_user.Count), 1).ToString();
            if (user_textbox.Text.ToString() == "Розробник")
            {
                P1.Content = Math.Round(1.0 * (disps_eth.Count * disps_user.Count - positive_results) / (disps_eth.Count * disps_user.Count), 2);
            }
            else
            {
                P2.Content = Math.Round(1.0 * positive_results / (disps_eth.Count * disps_user.Count), 2).ToString();
            }
        }


        //Код нижче - аналог коду с першого вікна(зчитування даних про інтервали та їх запис у матрицю
        double[,] user_intervals;//теперішні інтервали(не еталонні)
        int attempts;//Кількість спроб в режимі аутентифікації
        double alpha;
        String the_word;//кодове слово
        int current_symb_index = 0;//теперішній індекс-лічильник
        Stopwatch main_stopwatch = new Stopwatch();
        private void main_input_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {//Зчитуємо інформацію про інтервали в р. аутентиф. та записуємо в матрицю

            if (e.Key.ToString().ToLower() == the_word[current_symb_index].ToString())
            {//Якщо введений символ збігається зі символом кодового слова

                if (current_symb_index == 0)//Якщо перший символ(запустити таймер)
                {
                    main_stopwatch = new Stopwatch();
                    main_stopwatch.Start();
                }
                else//Не перший символ(зупинити таймер, записати та запустити)
                {
                    main_stopwatch.Stop();
                    user_intervals[attempts - 1, current_symb_index - 1] = main_stopwatch.ElapsedMilliseconds * 1.0 / 1000;
                    main_stopwatch.Restart();
                }
                current_symb_index++; //Теперішній індекс символу++

                if (current_symb_index == the_word.Length)
                    //Якщо це останній введений символ
                {
                    MessageBox.Show("Done with this attempt!");
                    main_input1.Text = "";
                    current_symb_index = 0;
                    attempts--;
                    e.Handled = true;
                    if (attempts == 0)//Остання спроба - зупиняємо процес навчання та вмикаємо кнопку "Зберегти результат"
                    {
                        main_input1.IsEnabled = false;
                        show_results(user_intervals);
                        calculate_fisher_coeff();
                    }
                }


            }
            else
            {//Якщо не збігається введений символ зі символом в кодовому слові
                MessageBox.Show("You have made a mistake. Try again.");
                main_input1.Text = "";
                current_symb_index = 0;
                e.Handled = true;
            }
        }
        private void show_results(double [,] matrix)
        {
            String to_show = "";
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    to_show += matrix[i, j] + " ";
                }
                to_show += "\n";
            }
            MessageBox.Show(to_show);
        }
    }
}
