using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{

    public class Privilege
    {
        private String description;

        public Privilege(String description)
        {
            Description = description;
        }
        public override string ToString()
        {
            return "Category: " + description;
        }
        public string Description { get => description; set => description = value.Trim(); }

        public static Privilege getPrivilege(int idApplicant)
        {
            String query =
            "SELECT " +
                "dbo.Privilege.ID AS PrivID, " +
                "dbo.Privilege.Description " +
            "FROM dbo.Applicant_Privilege " +
                "INNER JOIN dbo.ApplicantInfo " +
                "ON dbo.Applicant_Privilege.IDApplicant = dbo.ApplicantInfo.ID " +
                "INNER JOIN dbo.Privilege " +
                "ON dbo.Applicant_Privilege.IDPrivelege = dbo.Privilege.ID " +
            "WHERE(dbo.ApplicantInfo.ID = " + idApplicant + ");";

            var table = DBManager.extractTable(query);

            if (table.Rows.Count == 0)
                return null;

            return new Privilege(table.Rows[0]["Description"].ToString());
        }

    }
}
