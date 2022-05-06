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

namespace Lab_04.DataWindowPages
{
    /// <summary>
    /// Interaction logic for ExamLocation.xaml
    /// </summary>
    public partial class ExamLocation : Page
    {
        public ExamLocation()
        {
            InitializeComponent();
        }

        changeInstitution changeInstitution;
        public ExamLocation(changeInstitution change, String exam):this()
        {
            var table = DBManager.extractTable
                (
                $"SELECT " +
                $"TestInfo.ID, " +
                $"InstitutionName AS 'Institution', " +
                $"InstitutionNumber AS 'Number', " +
                $"City.CityName AS 'City', " +
                $"Region.RegionName AS 'Region', " +
                $"RoomNumber, " +
                $"Responsible " +
                $"FROM TestInfo LEFT OUTER JOIN EducatInstitutionInfo ON IDInstitution = EducatInstitutionInfo.ID LEFT OUTER JOIN TestSchedule ON IDSubject = TestSchedule.ID LEFT OUTER JOIN City ON EducatInstitutionInfo.IDCity = City.IDCity LEFT OUTER JOIN Region ON City.IDRegion = Region.IDRegion WHERE IDSubject = (SELECT ID FROM TestSchedule WHERE Name = '{exam}');");
            examLocation.ItemsSource = table.DefaultView;
            changeInstitution = change;
        }
        private void choosing(object sender, DataGridBeginningEditEventArgs e)
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
