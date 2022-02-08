using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();        
        }
        private void Exit_butt_clicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void to_main_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        List<object> operands = new List<object>();//Список для збору двох операндів та одного оператору. Ідея списку полягає в тому, що як тільки в ньому набирається 2 числа і один оператор, то значення рахується та виводиться(Приклад: якщо юзер вводитиме "34+45" нічого не відбудеться, але як тільки користувач введе ще один оператор("34+45-"), то значення рядку "34+45" зміниться на 79

        private void butt_Clicked(object sender, RoutedEventArgs e)
        {
            Button clicked_butt = (Button)sender;
            Button[] operators = new Button[] { add_butt, subtract_butt, multiply_butt, divide_butt };

            if (operators.Contains(clicked_butt)) // Якщо натиснута кнопка - оператор(+ або - і тд)
            {
                if (main_textbox.Text.ToString().Length == 0) return;
                if (new String("+-*/").Contains(main_textbox.Text.ToString()[main_textbox.Text.ToString().Length - 1])) //Перевіряємо щоб користувач не натиснув підряд декілька раз на кнопку оператора
                    return;
                String text = main_textbox.Text.ToString();
                try
                {
                    operands.Add(text.Substring(text.IndexOfAny(new char[] { '+', '-', '*', '/' }, 1) + 1));
                }
                catch (Exception ex) { main_textbox.Text = ex.Message; }
                if(operands.Count == 3)
                {
                    String temp = compute(operands).ToString();
                    main_textbox.Text = temp;
                    operands.Clear();
                    operands.Add(temp);
                }
                operands.Add(clicked_butt);
                main_textbox.Text += clicked_butt.Content;
            }
            else if (clicked_butt.Equals(equals_butt)) //Якщо натиснули "Дорівнює"
            {
                if (new String("+-*/").Contains(main_textbox.Text.ToString()[main_textbox.Text.ToString().Length - 1])) //Перевіряємо щоб користувач не натиснув "дорівнює" після оператора
                    return;
                String text = main_textbox.Text.ToString();//Для зручності вставляємо текст з калькулятора у змінну
                operands.Add(text.Substring(text.IndexOfAny(new char[] { '+', '-', '*', '/' }, 1) + 1)); //Додаємо до списку число, яке йде після оператора і до кінця, починаючи з позиції 1(тобто "-24+35" додасться число 35 у змінній типу String)
                String temp_value = compute(operands).ToString(); //Пораховане вже значення
                main_textbox.Text = temp_value;
                operands.Clear();
                //text.IndexOfAny(new char[] { '+', '-', '*', '/' }) + 1)
            }
            else if (clicked_butt.Equals(clear_all_butt)) //Стерти все
            {
                main_textbox.Text = "";
                operands.Clear();
            }
            else if (clicked_butt.Equals(clear_butt)) //Стерти лише останній знак
            {
                if (main_textbox.Text.ToString().Length == 0) return;
                main_textbox.Text = main_textbox.Text.ToString().Substring(0, main_textbox.Text.ToString().Length - 1);
            }
            else //Якщо вводиться цифра
            {
                main_textbox.Text += clicked_butt.Content;
            }

        }
        private Double compute(List<object> action)
        {
            Console.WriteLine((String)action[0] + " " + (String)action[2]);
            Button temp = (Button)action[1];
            switch (temp.Content)
            {
                case "+":
                    return Double.Parse((String)action[0]) + Double.Parse((String)action[2]);
                case "-":
                    return Double.Parse((String)action[0]) - Double.Parse((String)action[2]);
                case "*":
                    return Double.Parse((String)action[0]) * Double.Parse((String)action[2]);
                case "/":
                    return Double.Parse((String)action[0]) / Double.Parse((String)action[2]);
            }
            return 0;
        }

    }
}
