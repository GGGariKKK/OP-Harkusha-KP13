using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class DataWin : Window
    {
        public DataWin(string section)
        {
            InitializeComponent();
            switch (section)//Sections 1 through 4 according to button names on the main window
            {
                case "genInfo":
                    fillGenInfo();
                    break;
                case "testInfo":
                    fillTestInfo();
                    break;
                case "olimpPrivInfo":
                    fillOlimpiadPrivelegeInfo();
                    break;
                case "educInfo":
                    fillEducInfo();
                    break;
            }
        }
        string connectionString = "Data Source=DESKTOP-E96R6SM;Initial Catalog=ZNO_DB;Integrated Security=True";
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        private void retrieveAndFill(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();
        }
        private void retrieveAndFillTest(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);

            DataTable dtCloned = Table.Clone();
            dtCloned.Columns[2].DataType = typeof(string);
            foreach (DataRow row in Table.Rows)
            {
                DataRow temp = dtCloned.NewRow();
                temp[0] = row[0]; 
                temp[1] = row[1]; 
                temp[2] = row[2].ToString().Split(' ')[0];
                temp[3] = row[3]; 
                temp[4] = row[4];
                dtCloned.Rows.Add(temp);
            }

            dataGrid.ItemsSource = dtCloned.DefaultView;
            connection.Close();
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
            retrieveAndFill(query, genInfoData);
        }
        private void fillTestInfo()
        {
            testInfo.Visibility = Visibility.Visible;
            String query = "SELECT dbo.ApplicantInfo.ID, dbo.ApplicantInfo.Name, dbo.ApplicantInfo.Surname, dbo.TestSchedule.Name AS Subject, dbo.Applicant_Test.Result FROM dbo.Applicant_Test INNER JOIN dbo.ApplicantInfo ON dbo.Applicant_Test.IDApplicant = dbo.ApplicantInfo.ID INNER JOIN dbo.TestInfo ON dbo.Applicant_Test.IDTestInfo = dbo.TestInfo.ID INNER JOIN dbo.TestSchedule ON dbo.TestInfo.IDSubject = dbo.TestSchedule.ID";
            retrieveAndFill(query, testInfoData);

            query = "SELECT ID, Name AS Subject, Date, StartTime, EndTime FROM dbo.TestSchedule";
            retrieveAndFillTest(query, subjects);
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
            retrieveAndFill(query, appOlimpiad);
            query = "SELECT " +
                "dbo.ApplicantInfo.ID, " +
                "dbo.ApplicantInfo.Name, " +
                "dbo.ApplicantInfo.Surname, " +
                "dbo.Applicant_Privilege.IDPrivelege AS 'Privilege type' " +
                "FROM dbo.ApplicantInfo RIGHT OUTER JOIN dbo.Applicant_Privilege " +
                "ON dbo.Applicant_Privilege.IDApplicant = dbo.ApplicantInfo.ID";
            retrieveAndFill(query, appPrivilege);
            query = "SELECT ID, Level_Name AS 'Olimpiad type' FROM dbo.Olimpiad_Level";
            retrieveAndFill(query, olimpiad);
            query = "SELECT ID, Description AS 'Privilege description' FROM dbo.Privilege";
            retrieveAndFill(query, privilege);
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
            retrieveAndFill(query, educData);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();
        }

    }
}
