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
    /// Interaction logic for ExamInfo.xaml
    /// </summary>
    public partial class ExamInfo : Page
    {
        public ExamInfo()
        {
            InitializeComponent();
            fillExamInfo();
        }
        private void fillExamInfo()
        {
            testInfo.Visibility = Visibility.Visible;
            String query = "SELECT dbo.ApplicantInfo.ID, dbo.ApplicantInfo.Name, dbo.ApplicantInfo.Surname, dbo.TestSchedule.Name AS Subject, dbo.Applicant_Test.Result FROM dbo.Applicant_Test INNER JOIN dbo.ApplicantInfo ON dbo.Applicant_Test.IDApplicant = dbo.ApplicantInfo.ID INNER JOIN dbo.TestInfo ON dbo.Applicant_Test.IDTestInfo = dbo.TestInfo.ID INNER JOIN dbo.TestSchedule ON dbo.TestInfo.IDSubject = dbo.TestSchedule.ID";
            DBManager.retrieveAndFill(query, testInfoData);

            query = "SELECT ID, Name AS Subject, Date, StartTime, EndTime FROM dbo.TestSchedule";
            DBManager.retrieveAndFill(query, subjects);
        }

    }
}
