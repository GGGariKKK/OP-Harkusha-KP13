using Lab_04.Applicant_structure;
using System;

namespace Lab_04
{
    public class Applicant
    {
        private int id;
        private FullName fullName;
        private DateTime birthDate;
        private Olimpiad olimpiad; //may be null
        private Privilege privilege; //may be null
        private GraduationInfo gradInfo;
        private Exam[] exams;
        private String phoneNumber;
        private Address address;

        public Applicant(int ID, FullName name, DateTime birthDate, GraduationInfo gradInfo, Exam[] exams, String phoneNumber, Address address)
        {
            this.id = ID;
            this.fullName = name;
            this.birthDate = birthDate;
            this.gradInfo = gradInfo;
            this.exams = exams;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }
        public Applicant(int ID, FullName name, DateTime birthDate, Olimpiad olimpiad, GraduationInfo gradInfo, Exam[] exams, String phoneNumber, Address address) : this(ID, name, birthDate, gradInfo, exams, phoneNumber, address)
        {
            this.olimpiad = olimpiad;
        }
        public Applicant(int ID, FullName name, DateTime birthDate, Privilege privilege, GraduationInfo gradInfo, Exam[] exams, String phoneNumber, Address address) : this(ID, name, birthDate, gradInfo, exams, phoneNumber, address)
        {
            this.privilege = privilege;
        }
        public Applicant(int ID, FullName name, DateTime birthDate, Olimpiad olimpiad, Privilege privilege, GraduationInfo gradInfo, Exam[] exams, String phoneNumber, Address address) : this(ID, name, birthDate, gradInfo, exams, phoneNumber, address)
        {
            this.olimpiad = olimpiad;
            this.privilege = privilege;
        }

        public override string ToString()
        {
            String toReturn = "ID: " + id + "\nFull name: " + fullName + "\nBirthday: " + birthDate + "\nPhone number: " + phoneNumber + "\nGraduation info:\n" + gradInfo.ToString().Replace("\n", Environment.NewLine + "    ");

            toReturn += "\nExams:\n";
            foreach (Exam ex in Exams)
                toReturn += ex.ToString().Replace("\n", "\n    ") + "\n";

            if (olimpiad != null)
                toReturn += "\nOlimpiad:\n" + olimpiad.ToString().Replace("\n", "\n    ");

            if (privilege != null)
                toReturn += "\nPrivilege:\n" + privilege.ToString().Replace("\n", "\n    ");

            toReturn += "\nHome address:\n" + address.ToString().Replace("\n", "\n    ");

            return toReturn;
        }
        public int ID { get => id; set => id = value; }
        public FullName FullName { get => fullName; set => fullName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public GraduationInfo GradInfo { get => gradInfo; set => gradInfo = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value.Trim(); }
        internal Olimpiad Olimpiad { get => olimpiad; set => olimpiad = value; }
        internal Privilege Privilege { get => privilege; set => privilege = value; }
        internal Exam[] Exams { get => exams; set => exams = value; }
        public Address Address { get => address; set => address = value; }

        public static Applicant getApplicant(int id)
        {
            String query =
            "SELECT " +
                "dbo.ApplicantInfo.ID, " +
                "dbo.ApplicantInfo.Name, " +
                "dbo.ApplicantInfo.Surname, " +
                "dbo.ApplicantInfo.SecondName, " +
                "dbo.ApplicantInfo.[Date of birth], " +
                "dbo.EducatInstitutionInfo.InstitutionName, " +
                "dbo.EducatInstitutionInfo.InstitutionNumber, " +
                "dbo.City.CityName, dbo.Region.RegionName, " +
                "dbo.Applicant_Institution.GraduateYear, " +
                "dbo.Applicant_Institution.AvgGrade, " +
                "dbo.ContactInfo.PhoneNumber " +
            "FROM " +
                "dbo.ApplicantInfo " +
                "INNER JOIN dbo.Applicant_Institution " +
                "ON dbo.ApplicantInfo.ID = dbo.Applicant_Institution.ApplicantID " +
                "INNER JOIN dbo.EducatInstitutionInfo " +
                "ON dbo.Applicant_Institution.InstitutionID = dbo.EducatInstitutionInfo.ID " +
                "INNER JOIN dbo.City " +
                "ON dbo.EducatInstitutionInfo.IDCity = dbo.City.IDCity " +
                "INNER JOIN dbo.Region " +
                "ON dbo.City.IDRegion = dbo.Region.IDRegion " +
                "INNER JOIN dbo.ContactInfo " +
                "ON dbo.ApplicantInfo.ID = dbo.ContactInfo.IDApplicant " +
            "WHERE" +
                "(dbo.ApplicantInfo.ID = " + id + ");";

            var table = DBManager.extractTable(query);

            var ID = (int)table.Rows[0]["ID"];

            var name = new FullName
                (
                table.Rows[0]["Name"].ToString(),
                table.Rows[0]["Surname"].ToString(),
                table.Rows[0]["SecondName"].ToString()
                );

            var birthdate = Convert.ToDateTime(table.Rows[0]["Date of birth"].ToString());

            var gradInfo = new GraduationInfo
                (
                new Institution
                    (
                        (InstitutionType)Array.IndexOf(Institution.stringType,
                        table.Rows[0]["InstitutionName"].ToString()
                    ),
                (int)table.Rows[0]["InstitutionNumber"],
                table.Rows[0]["CityName"].ToString(),
                table.Rows[0]["RegionName"].ToString()),
                (int)table.Rows[0]["GraduateYear"],
                (double)table.Rows[0]["AvgGrade"]
                );

            var phoneNumber = table.Rows[0]["PhoneNumber"].ToString();

            var olimpiad = Olimpiad.getOlimpiad(id);

            var privilege = Privilege.getPrivilege(id);

            var exams = Exam.getExams(id);

            var address = Address.getAddress(id);

            Applicant applicant = new Applicant(ID, name, birthdate, olimpiad, privilege, gradInfo, exams, phoneNumber, address);

            return applicant;
        }
        public static Applicant getEmptyApplicant()
        {
            var ID = DBManager.extractCell<int>("SELECT Max(ID) FROM ApplicantInfo ") + 1;

            var name = new FullName
                (
                "",
                ""
                );

            var birthdate = Convert.ToDateTime(DateTime.MinValue);
            
            var gradInfo = new GraduationInfo
                (
                new Institution
                    (
                        (InstitutionType)1,
                        0,
                        "",
                        ""
                    ),
                0,
                0
                );

            var phoneNumber = "";

            //var olimpiad = Olimpiad.getOlimpiad(id);

            //var privilege = Privilege.getPrivilege(id);

            Exam[] exams = new Exam[0];// Exam.getExams(id);

            var address = new Address("", "", "", 0);

            Applicant applicant = new Applicant(ID, name, birthdate, gradInfo, exams, phoneNumber, address);

            return applicant;
        }

    }
}
