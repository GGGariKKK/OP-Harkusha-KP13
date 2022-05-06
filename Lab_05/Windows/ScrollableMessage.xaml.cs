using System;
using System.Collections.Generic;
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

namespace Lab_04
{
    /// <summary>
    /// Interaction logic for ScrollableMessage.xaml
    /// </summary>
    public partial class ScrollableMessage : Window
    {
        private ScrollableMessage(String message)
        {
            InitializeComponent();
            messageBox.Text = message;
        }
        public static void Show(String message)
        {
            new ScrollableMessage(message).Show();
        }

    }
}
