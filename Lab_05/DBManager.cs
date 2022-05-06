using Lab_04.Applicant_structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab_04
{
    static class DBManager
    {
        static string connectionString = "Data Source=DESKTOP-E96R6SM;Initial Catalog=ZNO_DB;Integrated Security=True";
        static SqlConnection connection = null;
        static DBManager()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        public static void executeQuery(String query)
        {           
            new SqlCommand(query, connection).ExecuteNonQuery();           
        }

        #region Getting Data

        public static DataTable extractTable(String query)
        {
            DataTable table = new DataTable();
            new SqlDataAdapter(new SqlCommand(query, connection)).Fill(table);
            return table;
        }
        public static T extractCell<T>(String query)
        {
            DataTable table = new DataTable();
            new SqlDataAdapter(new SqlCommand(query, connection)).Fill(table);
            return (T)table.Rows[0][0];
        }
        public static void retrieveAndFill(string SQLQuery, DataGrid dataGrid)
        {
            var Table = DBManager.extractTable(SQLQuery);

            //if (dataGrid.Name.ToString() == "subjects")
            //    Table = cropDate(Table);

            dataGrid.ItemsSource = Table.DefaultView;
        }


        #endregion

        #region Writing Data

        public static void modifyApplicant(Applicant applicant)
        {
            ScrollableMessage.Show(applicant.ToString());
            removeApplicant(applicant.ID);
            insertApplicant(applicant);
        }
        public static void removeApplicant(int IDApplicant)
        {
            executeQuery("DELETE FROM ApplicantInfo WHERE ID = " + IDApplicant + ";");
        }
        public static void insertApplicant(Applicant applicant)
        {
            insertApplicantInfo(applicant);

            insertContactInfo(applicant);

            if (applicant.Privilege != null)
                insertPrivilege(applicant);

            if (applicant.Olimpiad != null)
                insertOlimpiad(applicant);

            insertInstitution(applicant);

            insertExams(applicant);
        }

        #region Insertion sections
        public static void insertApplicantInfo(Applicant applicant)
        {
            executeQuery
                (
                "INSERT INTO ApplicantInfo " +
                "VALUES " +
                "('" +
                applicant.ID +
                "', '" +
                applicant.FullName.Name +
                "', '" +
                applicant.FullName.Surname +
                "', '" +
                applicant.FullName.SecName +
                "', '" +
                applicant.BirthDate +
                "');"
                );
        }
        public static void insertContactInfo(Applicant applicant)
        {
            executeQuery
                (
                "INSERT INTO ContactInfo " +
                "VALUES " +
                "('" +
                applicant.ID +
                "', '" +
                extractCell<int>("SELECT ID FROM Street WHERE Name = '" + applicant.Address.Street + "';") +
                "', '" +
                applicant.Address.BuildingNum +
                "', '" +
                applicant.PhoneNumber +
                "');"
                );
        }
        public static void insertPrivilege(Applicant applicant)
        {
            executeQuery
                (
                "INSERT INTO Applicant_Privilege " +
                "VALUES " +
                "('" +
                applicant.ID +
                "', '" +
                extractCell<Int16>("SELECT ID FROM Privilege WHERE Description = '" + applicant.Privilege.Description + "';") +
                "');"
                );
        }
        public static void insertOlimpiad(Applicant applicant)
        {
            executeQuery
                (
                "INSERT INTO Applicant_Olimpiad " +
                "VALUES " +
                "('" +
                applicant.ID +
                "', '" +
                extractCell<Int16>("SELECT ID FROM Olimpiad_Level WHERE Level_Name = '" + Olimpiad.stringOlimpiadType[(int)applicant.Olimpiad.Type] + "';") +
                "', '" +
                applicant.Olimpiad.PrizePlace +
                "');"
                );
        }
        public static void insertInstitution(Applicant applicant)
        {
            string temp =

                "INSERT INTO Applicant_Institution " +
                "VALUES " +
                "('" +
                applicant.ID +
                "', '"
                +
                extractCell<int>
                (
                    "SELECT ID " +
                    "FROM EducatInstitutionInfo " +
                    "WHERE " +
                    "InstitutionName = '" + Institution.stringType[(int)applicant.GradInfo.institution.Type] +
                    "' " +
                    "AND InstitutionNumber = '" + applicant.GradInfo.institution.Number + "' " +
                    "AND IDCity = " +
                    "(" +
                    "SELECT IDCity " +
                    "FROM City " +
                    "WHERE CityName = '" + applicant.GradInfo.institution.City + "' " +
                    ");"
                )
                +
                "', '" +
                applicant.GradInfo.gradYear +
                "', '" +
                applicant.GradInfo.avgGrade +
                "');";
            executeQuery(temp);
                
        }
        public static void insertExams(Applicant applicant)
        {
            foreach(Exam ex in applicant.Exams)
            {
                var idTestInfo = extractCell<int>
                    (
                    "SELECT ID " +
                    "FROM TestInfo " +
                    "WHERE " +
                    "IDSubject = '"
                    +
                    extractCell<Int16> //Id of subject
                    (
                        "SELECT ID " +
                        "FROM TestSchedule " +
                        "WHERE " +
                        "Name = '" + SubjectDetails.stringSubjects[(int)ex.SubjDetails.Subject] + "'"
                    )
                    + 
                    "' AND " +
                    "IDInstitution = '"
                    +
                    extractCell<int> // id of institution
                    (
                    "SELECT ID " +
                    "FROM EducatInstitutionInfo " +
                    "WHERE InstitutionName = '" + ex.LocatDetails.Institution.Type + "' " +
                    "AND InstitutionNumber = '" + ex.LocatDetails.Institution.Number + "' " +
                    "AND IDCity = " +
                    "(" +
                    "SELECT IDCity " +
                    "FROM City " +
                    "WHERE CityName = '" + ex.LocatDetails.Institution.City + "' " +
                    ");"
                    )
                    + 
                    "' AND " +
                    "RoomNumber = '"+ ex.LocatDetails.RoomNumber + 
                    "' AND " +
                    "Responsible = '" + ex.LocatDetails.Responsible + "';"
                    );

                executeQuery
                (
                "INSERT INTO Applicant_Test " +
                "VALUES " +
                "('" +
                applicant.ID +
                "', '" + 
                idTestInfo +
                "', '" +
                ex.Result +
                "');"
                );
            }
        }
        #endregion

        #endregion

        #region AdditionalMethods

        #endregion

    }
}
