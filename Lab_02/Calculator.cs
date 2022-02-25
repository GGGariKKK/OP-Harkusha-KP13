using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_02
{
    public partial class Calculator : Window
    {
        public Calculator()
        {
            Init();
        }
        public void Init()
        {
            Height = 665; Width = 480;
            Title = "Custom calculator";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Background = (Brush)new BrushConverter().ConvertFromString("#FF100C14");
            Grid grid = new Grid();
            grid.ContextMenuClosing += Exit_butt_clicked;

            grid.Height = 620; grid.Width = 460;
            grid.Background = (Brush)new BrushConverter().ConvertFromString("#FF100C14");
            grid.Opacity = 0.75;
            grid.Margin = new Thickness(5, 2, 5, 18);

            RowDefinition[] rows = new RowDefinition[5];
            ColumnDefinition[] columns = new ColumnDefinition[4];

            initialize_grid(grid, rows, columns);
            create_buttons(grid, rows, columns);

            main_textbox = Initialize.initialize_textbox(grid, "main_textbox", HorizontalAlignment.Left, VerticalAlignment.Center, 72, 120, 445, 0, 0, "", "#FF313131", true);
            main_textbox.HorizontalContentAlignment = HorizontalAlignment.Right;
            main_textbox.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFFFFFFF");
            Grid.SetColumn(main_textbox, 0);
            Grid.SetRow(main_textbox, 0);
            Grid.SetColumnSpan(main_textbox, 4);

            Grid equals_or_main = new Grid();
            two_buttons_in_grid(equals_or_main, new string[] { "=", "  Main\nwindow" }, new RoutedEventHandler[] { butt_Clicked, to_main_butt_Click });
            Grid.SetRow(equals_or_main, 4);
            Grid.SetColumn(equals_or_main, 0);
            grid.Children.Add(equals_or_main);

            Grid whole_cell = new Grid();
            Grid clear_or_all = new Grid();
            Grid dot_grid = new Grid();
            initialize_grid(whole_cell, new RowDefinition[2], new ColumnDefinition[1]);
            initialize_grid(clear_or_all, new RowDefinition[1], new ColumnDefinition[2]);
            clear_butt = create_button(butt_Clicked, "C");
            clear_all_butt = create_button(butt_Clicked, "CA");
            Grid.SetColumn(clear_butt, 1);
            Grid.SetColumn(clear_all_butt, 0);

            clear_or_all.Children.Add(clear_butt);
            clear_or_all.Children.Add(clear_all_butt);
            Grid.SetRow(clear_or_all, 1);
            Grid.SetColumn(clear_or_all, 0);

            Button dot = create_button(butt_Clicked, ".");
            dot_grid.Children.Add(dot);

            Grid.SetRow(dot, 0);
            Grid.SetColumn(dot, 0);
            whole_cell.Children.Add(dot_grid);
            whole_cell.Children.Add(clear_or_all);

            Grid.SetRow(whole_cell, 4);
            Grid.SetColumn(whole_cell, 2);
            grid.Children.Add(whole_cell);
            
            this.Content = grid;
        }
        public void initialize_grid(Grid grid, RowDefinition[] rows, ColumnDefinition[] columns)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new RowDefinition();
                grid.RowDefinitions.Add(rows[i]);
            }
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columns[i]);
            }
        }
        public void create_buttons(Grid grid, RowDefinition[] rows, ColumnDefinition[] columns)
        {
            char[] symbs_arr = new char[] { '1', '2', '3', '+', '4', '5', '6', '-', '7', '8', '9', '*', '0', '/' };
            SolidColorBrush temp = new SolidColorBrush(Colors.Gray);//Для присвоєння всім кнопкам як фон(немає різниці який колір, адже фон прозорий)
            int counter = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 4 && (j == 0 || j == 2)) continue;
                        Button current = new Button();
                        current.FontSize = 70;
                        current.Foreground = Brushes.White;
                        current.Click += butt_Clicked;
                        temp.Opacity = 0;
                        current.Background = temp;
                        current.BorderBrush = temp;
                    current.Content = symbs_arr[counter].ToString();
                        counter++;
                        Grid.SetRow(current, i);
                        Grid.SetColumn(current, j);

                        grid.Children.Add(current);
                }
            }
        }

        public void two_buttons_in_grid(Grid grid, String[] content, RoutedEventHandler[] handler)
        {
            SolidColorBrush temp = new SolidColorBrush(Colors.Gray);//Для присвоєння всім кнопкам як фон(немає різниці який колір, адже фон прозорий)
            RowDefinition[] rows = new RowDefinition[2];
            ColumnDefinition[] cols = new ColumnDefinition[1];
            initialize_grid(grid, rows, cols);
            for (int i = 0; i < content.Length; i++)
            {
                Button current = new Button();
                current.FontSize = Math.Abs(i-1) * 20 + 16;
                current.Foreground = Brushes.White;
                current.Click += handler[i];
                temp.Opacity = 0;
                current.Background = temp;
                current.BorderBrush = temp;
                current.Content = content[i].ToString();
                Grid.SetRow(current, i);
                if (i == 0)
                {
                    equals_butt = current;
                }
                else
                {
                    current.HorizontalContentAlignment = HorizontalAlignment.Center;
                    current.VerticalContentAlignment = VerticalAlignment.Top;
                } 
                  
                grid.Children.Add(current);
            }
        }
        public Button create_button(RoutedEventHandler handler, String content)
        {
            SolidColorBrush temp = new SolidColorBrush(Colors.Gray);
            Button current = new Button();
            current.FontSize = 30;
            current.Foreground = Brushes.White;
            current.Click += handler;
            temp.Opacity = 0;
            current.Background = temp;
            current.BorderBrush = temp;
            current.Content = content;
            return current;
        }

        List<object> operands = new List<object>();//Список для збору двох операндів та одного оператору. Ідея списку полягає в тому, що як тільки в ньому набирається 2 числа і один оператор, то значення рахується та виводиться(Приклад: якщо юзер вводитиме "34+45" нічого не відбудеться, але як тільки користувач введе ще один оператор("34+45-"), то значення рядку "34+45" зміниться на 79

        TextBox main_textbox;
        Button equals_butt;
        Button clear_all_butt;
        Button clear_butt;
            public void butt_Clicked(object sender, RoutedEventArgs e)
        {
            Button clicked_butt = (Button)sender;
            //Button[] operators = new Button[] { add_butt, subtract_butt, multiply_butt, divide_butt };

            if ("+-*/".Contains(clicked_butt.Content.ToString())) // Якщо натиснута кнопка - оператор(+ або - і тд)
            {
                if (main_textbox.Text.ToString().Length == 0) return;
                if ("+-*/".Contains(main_textbox.Text.ToString()[main_textbox.Text.ToString().Length - 1])) //Перевіряємо щоб користувач не натиснув підряд декілька раз на кнопку оператора
                    return;
                String text = main_textbox.Text.ToString();
                try
                {
                    operands.Add(text.Substring(text.IndexOfAny(new char[] { '+', '-', '*', '/' }, 1) + 1));
                }
                catch (Exception ex) { main_textbox.Text = ex.Message; }
                if (operands.Count == 3)
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
                if ("+-*/".Contains(main_textbox.Text.ToString()[main_textbox.Text.ToString().Length - 1])) //Перевіряємо щоб користувач не натиснув "дорівнює" після оператора
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
                String text = main_textbox.Text.ToString();
                if (text.Length == 0) return;
                main_textbox.Text = text.Substring(0, text.Length - 1);

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

        private void Exit_butt_clicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void to_main_butt_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().Show();
        }

    }
}
