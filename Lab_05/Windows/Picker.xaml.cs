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
    /// Interaction logic for ExamPicker.xaml
    /// </summary>
    public partial class Picker : Window
    {
        callAction callMethod;
        public Picker()
        {
            InitializeComponent();          
        }
        public static void Show(ComboBox picking, callAction callWhenPicked)
        {
            var curr = new Picker();
            curr.Show();
            foreach (var item in picking.Items)
                curr.pickingComboBox.Items.Add(item);
            curr.callMethod = callWhenPicked;
        }
        private void apply_Click(object sender, RoutedEventArgs e)
        {
            if(pickingComboBox.SelectedItem == null) {
                MessageBox.Show("You must choose a subject or click \"Cancel\"");
                return; 
            }    
            callMethod(pickingComboBox.SelectedItem.ToString());
            Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            callMethod(null);
            Close();
        }

    }
}
