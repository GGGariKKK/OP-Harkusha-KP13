using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Read_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Read_student_data_window().Show();
        }

        private void Game_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Noughts_and_crosses().Show();
        }

        private void Custom_calc_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Calculator().Show();
        }

        private void sys_calc_butt_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Calc");
        }

        private void Dev_info_butt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new dev_info().Show();
        }

        private void Exit_butt_clicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

public class Initialize
{
    public static TextBox initialize_textbox(Grid grid, String name, HorizontalAlignment h, VerticalAlignment v, int font_size, int height, int width, int margin1, int margin2, String text, String background, bool isEnabled)
    {
        TextBox curr = new TextBox();
        curr.Name = name;
        curr.Height = height;
        curr.Width = width;
        curr.Margin = new Thickness(margin1, margin2, 0, 0);
        curr.Text = text;
        var converter = new System.Windows.Media.BrushConverter();

        curr.Background = (System.Windows.Media.Brush)converter.ConvertFromString(background);
        curr.IsEnabled = isEnabled;
        curr.HorizontalAlignment = h;
        curr.VerticalAlignment = v;
        curr.FontSize = font_size;
        grid.Children.Add(curr);
        return curr;
    }
    public static Button initialize_button(Grid grid, String name, String content, HorizontalAlignment h, VerticalAlignment v, int height, int width, String background, String border_brush, int margin1, int margin2, int font_size, bool isEnabled, RoutedEventHandler handler)
    {
        Button curr = new Button();
        curr.Name = name;
        curr.Content = content;
        curr.HorizontalAlignment = h;
        curr.VerticalAlignment = v;
        curr.Height = height; curr.Width = width;
        curr.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(background);
        curr.BorderBrush = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(border_brush);
        curr.Margin = new Thickness(margin1, margin2, 0, 0);
        curr.FontSize = font_size;
        curr.IsEnabled = isEnabled;
        curr.Click += handler;
        grid.Children.Add(curr);
        return curr;
    }
    public static Button initialize_button(Grid grid, String name, String content, HorizontalAlignment h, VerticalAlignment v, int height, int width, String background, int margin1, int margin2, int font_size, bool isEnabled, String foreground, RoutedEventHandler handler)
    {
        Button curr = new Button();
        curr.Name = name;
        curr.Content = content;
        curr.HorizontalAlignment = h;
        curr.VerticalAlignment = v;
        curr.Height = height; curr.Width = width;
        curr.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(background);
        curr.Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(foreground);
        curr.Margin = new Thickness(margin1, margin2, 0, 0);
        curr.FontSize = font_size;
        curr.IsEnabled = isEnabled;
        curr.Click += handler;
        curr.FontFamily = new System.Windows.Media.FontFamily("Ink Free");
        grid.Children.Add(curr);
        return curr;
    }
    public static Button initialize_button(Grid grid, String name, String content, HorizontalAlignment h, VerticalAlignment v, int height, int width, double opacity, String background, String foreground, String border_brush, int margin1, int margin2, int font_size, bool isEnabled, RoutedEventHandler handler)
    {
        Button curr = new Button();
        curr.Name = name;
        curr.Content = content;
        curr.HorizontalAlignment = h;
        curr.VerticalAlignment = v;
        curr.Height = height; curr.Width = width;
        curr.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(background);
        curr.BorderBrush = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(border_brush);
        curr.Margin = new Thickness(margin1, margin2, 0, 0);
        curr.FontSize = font_size;
        curr.IsEnabled = isEnabled;
        curr.Click += handler;
        curr.Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(foreground);
        curr.Opacity = opacity;
        grid.Children.Add(curr);
        return curr;
    }
    public static Button initialize_button(Grid grid, int grid_column, int grid_row, String name, String content, HorizontalAlignment h, VerticalAlignment v, int height, int width, double opacity, String foreground, String border_brush, int font_size, RoutedEventHandler handler)
    {
        Button curr = new Button();
        curr.Name = name;
        curr.Content = content;
        curr.HorizontalAlignment = h;
        curr.VerticalAlignment = v;
        curr.Height = height; curr.Width = width;
        curr.BorderBrush = System.Windows.Media.Brushes.Black;
        curr.FontSize = font_size;
        curr.Click += handler;
        curr.Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString(foreground);
        curr.Background = new SolidColorBrush(Colors.Gray);//Для присвоєння всім кнопкам як фон(немає різниці який колір, адже фон прозорий)
        curr.Background.Opacity = opacity;
        Grid.SetColumn(curr, grid_column);
        Grid.SetRow(curr, grid_row);
        grid.Children.Add(curr);
        return curr;
    }
    public static ComboBox initialize_combobox(Grid grid, String name, String[] item_names, HorizontalAlignment h, VerticalAlignment v, int height, int width, int margin1, int margin2, SelectionChangedEventHandler handler)
    {
        ComboBox curr = new ComboBox();
        curr.Name = name;
        curr.HorizontalAlignment = h;
        curr.VerticalAlignment = v;
        curr.Height = height;
        curr.Width = width;
        curr.Margin = new Thickness(margin1, margin2, 0, 0);
        List<ListBoxItem> items = new List<ListBoxItem>();
        for(int i = 0; i < item_names.Length; i++)
        {
            ListBoxItem temp = new ListBoxItem();
            temp.Content = item_names[i];
            items.Add(temp);
        }
        curr.ItemsSource = items;
        curr.SelectionChanged += handler;
        grid.Children.Add(curr);
        return curr;
    }
}
