using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_02
{
    public partial class dev_info : Window
    {

        public dev_info()
        {
            Height = 460; Width = 800;
            Grid grid = new Grid();
            grid.ContextMenuClosing += Exit_butt_clicked;
            LinearGradientBrush myLinearGradientBrush =
            new LinearGradientBrush();
            myLinearGradientBrush.StartPoint = new Point(0.5, 0);
            myLinearGradientBrush.EndPoint = new Point(0.5, 1);
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF5D2AFF"), 0.0));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"), 1.0));
            grid.Background = myLinearGradientBrush;

            Label labl = new Label();
            labl.Content = "Студент групи КП-13\nКиївського політехнічного університету\nімені Ігоря Сікорського\nГаркуша Ілля Сергійович\n\n2022";
            labl.Margin = new Thickness(0, 105, 0, 0);
            labl.FontSize = 20;
            labl.FontFamily = new FontFamily("Segoe Print");
            labl.Foreground = Brushes.Black;
            labl.Background = (Brush)new BrushConverter().ConvertFromString("#FF837DAD");
            labl.Height = 258; labl.Width = 462;
            labl.HorizontalAlignment = HorizontalAlignment.Center;
            labl.VerticalAlignment = VerticalAlignment.Top;
            labl.HorizontalContentAlignment = HorizontalAlignment.Center;
            labl.VerticalContentAlignment = VerticalAlignment.Center;
            grid.Children.Add(labl);

            Label labl1 = new Label();
            labl1.Content = "Про розробника";
            labl1.Margin = new Thickness(0, 10, 0, 0);
            labl1.FontSize = 36;
            labl1.FontFamily = new FontFamily("Segoe Print");
            labl1.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFC054FF");
            labl1.Background = (Brush)new BrushConverter().ConvertFromString("#FF837DAD");
            labl1.Background.Opacity = 0;
            labl1.Height = 95; labl1.Width = 398;
            labl1.HorizontalAlignment = HorizontalAlignment.Center;
            labl1.VerticalAlignment = VerticalAlignment.Top;
            labl1.HorizontalContentAlignment = HorizontalAlignment.Center;
            labl1.VerticalContentAlignment = VerticalAlignment.Center;

            Button back_to_main_butt = new Button();
            back_to_main_butt.Content = "Back to main menu";
            back_to_main_butt.Height = 34;
            back_to_main_butt.Width = 196;
            back_to_main_butt.Margin = new Thickness(0, 377, 0, 0);
            back_to_main_butt.Background = (Brush)new BrushConverter().ConvertFromString("#FF837DAD");
            back_to_main_butt.HorizontalAlignment = HorizontalAlignment.Center;
            back_to_main_butt.VerticalAlignment = VerticalAlignment.Top;
            back_to_main_butt.Click += to_main_butt_Click;
            grid.Children.Add(back_to_main_butt);


            grid.Children.Add(labl1);


            Content = grid;
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
    }
}
