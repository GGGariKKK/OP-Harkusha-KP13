using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_02
{
    public partial class Noughts_and_crosses : Window
    {
        public Noughts_and_crosses()
        {
            Init();
        }
        public void Init()
        {
            Height = 770; Width = 755;
            Title = "Tic tac toe";
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Grid grid = new Grid();
            grid.Height = 750; grid.Width = 750;
            grid.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF5971F3");
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Top;

            Button[,] playing_butts = new Button[5, 5];
            RowDefinition[] rows = new RowDefinition[6];
            ColumnDefinition[] columns = new ColumnDefinition[5];

            initialize_grid(grid, rows, columns);

            create_buttons(grid, rows, columns);//Гральні кнопки

            Button to_menu = Initialize.initialize_button(grid, "to_main", "Back\n to menu", HorizontalAlignment.Center, VerticalAlignment.Top, 86, 122, "#FF4461FF", 0, 13, 30, true, "#FFFF8383", to_main_butt_Click);
            Grid.SetRow(to_menu, 5);
            Grid.SetColumn(to_menu, 0);

            Button exit = Initialize.initialize_button(grid, "exit", "Exit", HorizontalAlignment.Center, VerticalAlignment.Top, 86, 122, "#FF4461FF", 0, 13, 30, true, "#FFFF8383", Exit_butt_Click);
            Grid.SetRow(exit, 5);
            Grid.SetColumn(exit, 4);

            Button start_game = Initialize.initialize_button(grid, "Start_butt", "Start", HorizontalAlignment.Center, VerticalAlignment.Top, 86, 440, "#FF4461FF", 0, 13, 36, true, "#FFFF8383", Start_butt_Click);
            Grid.SetRow(start_game, 5);
            Grid.SetColumn(start_game, 1);
            Grid.SetColumnSpan(start_game, 3);

            Content = grid;
        }
        public void initialize_grid(Grid grid, RowDefinition[] rows, ColumnDefinition[] columns)
        {
            for(int i = 0; i < rows.Length; i++)
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
        Button[,] playing_butts;
        public void create_buttons(Grid grid, RowDefinition[] rows, ColumnDefinition[] columns)
        {
            playing_butts = new Button[5, 5];
            SolidColorBrush temp = new SolidColorBrush(Colors.Gray);//Для присвоєння всім кнопкам як фон(немає різниці який колір, адже фон прозорий)
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    playing_butts[i, j] = new Button();
                    playing_butts[i, j].FontSize = 80;
                    playing_butts[i, j].FontFamily = new System.Windows.Media.FontFamily("Ravie");
                    playing_butts[i, j].Foreground = System.Windows.Media.Brushes.Red;
                    playing_butts[i, j].Click += butt1_Click;
                    temp.Opacity = 0;
                    playing_butts[i, j].Background = temp;
                    playing_butts[i, j].Content = "";
                    Grid.SetRow(playing_butts[i, j], i);
                    Grid.SetColumn(playing_butts[i, j], j);

                    Grid.SetRow(playing_butts[i, j], i);
                    Grid.SetColumn(playing_butts[i, j], j);

                    grid.Children.Add(playing_butts[i, j]);
                }
            }
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
        private void Exit_butt_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void to_main_butt_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().Show();
        }
        bool computer_first;
        private void Start_butt_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button curr in playing_butts)
                curr.Content = "";
            computer_first = new Random().Next(2) == 0 ? false : true;
            if (computer_first)
                computers_turn();
        }
        private void computers_turn()
        {
            for (int i = 0; i < playing_butts.GetLength(0); i++)
                for (int j = 0; j < playing_butts.GetLength(1); j++)
                {
                    if (playing_butts[i, j].Content.ToString() == "")
                    {
                        playing_butts[i, j].Content = computer_first == true ? "X" : "O";
                        return;
                    }
                }
        }
    }
}
