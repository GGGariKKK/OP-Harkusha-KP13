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
    /// Interaction logic for InstitutionInfo.xaml
    /// </summary>
    public partial class InstitutionInfo : Page
    {
        public InstitutionInfo()
        {
            InitializeComponent();
            fillEducInfo();
        }
        private void fillEducInfo()
        {
            educInfo.Visibility = Visibility.Visible;
            String query = "SELECT " +
                "dbo.EducatInstitutionInfo.InstitutionName AS 'Institution', " +
                "dbo.EducatInstitutionInfo.InstitutionNumber AS 'Number', " +
                "dbo.City.CityName AS 'City', " +
                "dbo.Region.RegionName AS 'Region' " +
                "FROM dbo.City " +
                "INNER JOIN dbo.EducatInstitutionInfo " +
                "ON dbo.City.IDCity = dbo.EducatInstitutionInfo.IDCity " +
                "INNER JOIN dbo.Region " +
                "ON dbo.City.IDRegion = dbo.Region.IDRegion";
            DBManager.retrieveAndFill(query, educData);
        }

        changeInstitution changeInstitution;
        public InstitutionInfo(changeInstitution change) : this()
        {
            changeInstitution = change;
        }
        private void educData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (!(Window.GetWindow(this) as DataWin).open)
            {
                var row = e.Row.Item as DataRowView;
                changeInstitution(row);
                Window.GetWindow(this).Close();
            }
        }
    }
}
