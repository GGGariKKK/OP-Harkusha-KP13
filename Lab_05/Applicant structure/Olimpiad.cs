using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{
    public enum OlimpiadType
    {
        School,
        City, 
        Country,
        International
    }
    public class Olimpiad
    {
        public static String[] stringOlimpiadType = new string[] { "School", "City", "Country", "International" };
        private OlimpiadType type;
        private int prizePlace;

        public Olimpiad(OlimpiadType type, int prizePlace)
        {
            Type = type;
            PrizePlace = prizePlace;
        }
        public override string ToString()
        {
            return "    Olimpiad type: " + type + " olimpiad\nPrize place: " + prizePlace;
        }
        public OlimpiadType Type { get => type; set => type = value; }
        public int PrizePlace { get => prizePlace; set => prizePlace = value; }

        public static Olimpiad getOlimpiad(int idApplicant)
        {
            String query =
            "SELECT " +
                "dbo.Olimpiad_Level.Level_Name, " +
                "dbo.Applicant_Olimpiad.Prize_place " +
            "FROM " +
                "dbo.Applicant_Olimpiad " +
                "INNER JOIN dbo.ApplicantInfo " +
                "ON dbo.Applicant_Olimpiad.IDApplicant = dbo.ApplicantInfo.ID " +
                "INNER JOIN dbo.Olimpiad_Level " +
                "ON dbo.Applicant_Olimpiad.IDOlimpiad = dbo.Olimpiad_Level.ID " +
            "WHERE" +
                "(dbo.ApplicantInfo.ID = " + idApplicant + ");";

            var table = DBManager.extractTable(query);

            if (table.Rows.Count == 0)
                return null;

            return new Olimpiad((OlimpiadType)Array.IndexOf(Olimpiad.stringOlimpiadType, table.Rows[0]["Level_Name"]), (Int16)table.Rows[0]["Prize_place"]);
        }

    }
}
