using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_04
{
    /// <summary>
    /// Interaction logic for StreetInfo.xaml
    /// </summary>
    public partial class StreetInfo : Page
    {
        public StreetInfo()
        {
            InitializeComponent();
            fillStreetInfo();
        }

        private void fillStreetInfo()
        {
            String query =
                "SELECT " +
                    "dbo.Region.RegionName AS 'Region', " +
                    "dbo.City.CityName AS 'City', " +
                    "dbo.Street.Name AS 'Street' " +
                "FROM dbo.Region " +
                    "INNER JOIN dbo.City " +
                    "ON dbo.Region.IDRegion = dbo.City.IDRegion " +
                    "INNER JOIN dbo.Street " +
                    "ON dbo.City.IDCity = dbo.Street.IDCity";

            DBManager.retrieveAndFill(query, streetData);
        }

        changeStreet changeStreet;
        public StreetInfo(changeStreet change) : this()
        {
            changeStreet = change;
        }
        private void streetData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!(Window.GetWindow(this) as DataWin).open)
            {
                var row = e.Row.Item as DataRowView;
                changeStreet(row);
                Window.GetWindow(this).Close();
            }
        }
    }
}
