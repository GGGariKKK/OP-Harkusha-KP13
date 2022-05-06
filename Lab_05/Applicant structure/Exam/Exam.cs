using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{
    
    public class Exam
    {
        private SubjectDetails subjDetails;
        private int result;
        private LocationDetails locatDetails;

        public Exam()
        {

        }
        public Exam(SubjectDetails subjDetails, int result, LocationDetails locatDetails)
        {
            SubjDetails = subjDetails;
            Result = result;
            LocatDetails = locatDetails;
        }

        public override string ToString()
        {
            return "Result: " + result + "\nSubject details:\n" + subjDetails.ToString().Replace("\n", "\n    ") + "\nVenue details:\n" + locatDetails.ToString().Replace("\n", "\n    ");
        }
        public int Result { get => result; set => result = value; }
        public SubjectDetails SubjDetails { get => subjDetails; set => subjDetails = value; }
        public LocationDetails LocatDetails { get => locatDetails; set => locatDetails = value; }

        public static Exam[] getExams(int idApplicant)
        {
            String query =
            "SELECT " +
                "dbo.TestSchedule.Name AS 'ExamName', " +
                "dbo.Applicant_Test.Result, " +
                "dbo.TestSchedule.Date, " +
                "dbo.TestSchedule.StartTime, " +
                "dbo.TestSchedule.EndTime, " +
                "dbo.EducatInstitutionInfo.InstitutionName," +
                "dbo.EducatInstitutionInfo.InstitutionNumber, " +
                "dbo.City.CityName, " +
                "dbo.Region.RegionName, " +
                "dbo.TestInfo.RoomNumber, " +
                "dbo.TestInfo.Responsible " +
            "FROM " +
                "dbo.Applicant_Test " +
                "INNER JOIN dbo.ApplicantInfo " +
                "ON dbo.Applicant_Test.IDApplicant = dbo.ApplicantInfo.ID " +
                "INNER JOIN dbo.TestInfo " +
                "ON dbo.Applicant_Test.IDTestInfo = dbo.TestInfo.ID " +
                "INNER JOIN dbo.EducatInstitutionInfo " +
                "ON dbo.TestInfo.IDInstitution = dbo.EducatInstitutionInfo.ID " +
                "INNER JOIN dbo.TestSchedule " +
                "ON dbo.TestInfo.IDSubject = dbo.TestSchedule.ID " +
                "INNER JOIN dbo.City " +
                "ON dbo.EducatInstitutionInfo.IDCity = dbo.City.IDCity " +
                "INNER JOIN dbo.Region " +
                "ON dbo.City.IDRegion = dbo.Region.IDRegion " +
            "WHERE" +
                "(dbo.ApplicantInfo.ID = " + idApplicant + ");";

            var table = DBManager.extractTable(query);

            var exams = new Exam[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var currExam = new Exam();

                var currSubjectDetails = new SubjectDetails();
                currSubjectDetails.Date = table.Rows[i]["Date"].ToString();
                currSubjectDetails.Start = table.Rows[i]["StartTime"].ToString();
                currSubjectDetails.End = table.Rows[i]["EndTime"].ToString();
                currSubjectDetails.Subject = (ExamSubject)Array.IndexOf(SubjectDetails.stringSubjects, table.Rows[i]["ExamName"]);

                var currLocationDetails = new LocationDetails();
                currLocationDetails.Institution = new Institution((InstitutionType)Array.IndexOf(Institution.stringType, table.Rows[i]["InstitutionName"].ToString()), (int)table.Rows[i]["InstitutionNumber"], table.Rows[i]["CityName"].ToString(), table.Rows[i]["RegionName"].ToString());
                currLocationDetails.RoomNumber = (int)table.Rows[i]["RoomNumber"];

                String[] responsibleInfo = table.Rows[i]["Responsible"].ToString().Split(' ');
                currLocationDetails.Responsible = new FullName(responsibleInfo[0], responsibleInfo[1], "");

                exams[i] = new Exam(currSubjectDetails, (Int16)table.Rows[i]["Result"], currLocationDetails);
            }

            return exams;
        }

    }
}
