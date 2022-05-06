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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_04
{
    /// <summary>
    /// Interaction logic for OlimpiadAndPrivelegeInfo.xaml
    /// </summary>
    public partial class OlimpiadAndPrivelegeInfo : Page
    {
        public OlimpiadAndPrivelegeInfo()
        {
            InitializeComponent();
            fillOlimpiadPrivelegeInfo();
        }
        private void fillOlimpiadPrivelegeInfo()
        {
            olimpiadPrivelegeInfo.Visibility = Visibility.Visible;
            String query = "SELECT " +
                "dbo.ApplicantInfo.ID, " +
                "dbo.ApplicantInfo.Name, " +
                "dbo.ApplicantInfo.Surname, " +
                "dbo.Applicant_Olimpiad.IDOlimpiad AS 'Olimpiad type', " +
                "dbo.Applicant_Olimpiad.Prize_place AS 'Prize place'" +
                "FROM dbo.Applicant_Olimpiad " +
                "LEFT OUTER JOIN dbo.ApplicantInfo " +
                "ON dbo.Applicant_Olimpiad.IDApplicant = dbo.ApplicantInfo.ID ";
            DBManager.retrieveAndFill(query, appOlimpiad);
            query = "SELECT " +
                "dbo.ApplicantInfo.ID, " +
                "dbo.ApplicantInfo.Name, " +
                "dbo.ApplicantInfo.Surname, " +
                "dbo.Applicant_Privilege.IDPrivelege AS 'Privilege type' " +
                "FROM dbo.ApplicantInfo RIGHT OUTER JOIN dbo.Applicant_Privilege " +
                "ON dbo.Applicant_Privilege.IDApplicant = dbo.ApplicantInfo.ID";
            DBManager.retrieveAndFill(query, appPrivilege);
            query = "SELECT ID, Level_Name AS 'Olimpiad type' FROM dbo.Olimpiad_Level";
            DBManager.retrieveAndFill(query, olimpiad);
            query = "SELECT ID, Description AS 'Privilege description' FROM dbo.Privilege";
            DBManager.retrieveAndFill(query, privilege);
        }

    }
}
