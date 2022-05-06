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
    /// Interaction logic for ApplicantInfo.xaml
    /// </summary>
    public partial class ApplicantInfo : Page
    {
        public ApplicantInfo()
        {
            InitializeComponent();
            fillGenInfo();
        }
        private void fillGenInfo()
        {
            genInfo.Visibility = Visibility.Visible;
            String query = "SELECT " +
                "dbo.ApplicantInfo.ID, " +
                "dbo.ApplicantInfo.Name, " +
                "dbo.ApplicantInfo.Surname, " +
                "dbo.EducatInstitutionInfo.InstitutionName AS 'Institution type', " +
                "dbo.EducatInstitutionInfo.InstitutionNumber AS 'Inst number'," +
                "dbo.Applicant_Institution.GraduateYear AS 'Grad year', " +
                "dbo.Applicant_Institution.AvgGrade AS 'Avg grade', " +
                "dbo.ContactInfo.PhoneNumber " +
                "FROM dbo.Applicant_Institution " +
                "INNER JOIN dbo.ApplicantInfo " +
                "ON dbo.Applicant_Institution.ApplicantID = dbo.ApplicantInfo.ID " +
                "INNER JOIN dbo.ContactInfo " +
                "ON dbo.ApplicantInfo.ID = dbo.ContactInfo.IDApplicant " +
                "INNER JOIN dbo.EducatInstitutionInfo " +
                "ON dbo.Applicant_Institution.InstitutionID = dbo.EducatInstitutionInfo.ID";
            DBManager.retrieveAndFill(query, genInfoData);
        }

        private void modifyApplicant(object sender, DataGridBeginningEditEventArgs e)
        {           
            var row = e.Row.Item as DataRowView;
            var value = row["ID"];

            Applicant applicant;

            if (value.ToString() == "")
                applicant = Applicant.getEmptyApplicant();
            else
                applicant = Applicant.getApplicant((int)value);

            new ModifyWin(applicant).Show();
            Window.GetWindow(this).Close();
            (Window.GetWindow(this) as DataWin).win.Close();
        }

    }
}
