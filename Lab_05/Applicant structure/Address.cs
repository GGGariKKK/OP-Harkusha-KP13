using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04.Applicant_structure
{
    public class Address
    {
        private String region;
        private String city;       
        private String street;
        private int buildingNum;


        public Address(String region, String city, String street, int buildingNum)
        {
            City = city;
            Street = street;
            BuildingNum = buildingNum;
            Region = region;
        }

        public override string ToString()
        {
            return "   Region: " + region + "\nCity: " + city + "\nStreet: " + street + "\nBuilding number: " + buildingNum;
        }
        public string City { get => city; set => city = value.Trim(); }
        public string Street { get => street; set => street = value.Trim(); }
        public int BuildingNum { get => buildingNum; set => buildingNum = value; }
        public string Region { get => region; set => region = value.Trim(); }

        public static Address getAddress(int idApplicant)
        {
            String query =
            "SELECT " +
                "dbo.Region.RegionName, " +
                "dbo.City.CityName, " +
                "dbo.Street.Name AS StreetName, " +
                "dbo.ContactInfo.BuildingNumber " +
            "FROM dbo.Region " +
                "INNER JOIN dbo.City " +
                "ON dbo.Region.IDRegion = dbo.City.IDRegion " +
                "INNER JOIN dbo.Street " +
                "ON dbo.City.IDCity = dbo.Street.IDCity " +
                "INNER JOIN dbo.ApplicantInfo " +
                "INNER JOIN dbo.ContactInfo " +
                "ON dbo.ApplicantInfo.ID = dbo.ContactInfo.IDApplicant " +
                "ON dbo.Street.ID = dbo.ContactInfo.IDStreet " +
            "WHERE " +
                "dbo.ApplicantInfo.ID = '" + idApplicant + "';";

            var table = DBManager.extractTable(query);

            var region = table.Rows[0]["RegionName"].ToString();
            var city = table.Rows[0]["CityName"].ToString();
            var street = table.Rows[0]["StreetName"].ToString();
            var buildingNum = int.Parse(table.Rows[0]["BuildingNumber"].ToString());

            return new Address(region, city, street, buildingNum);
        }

    }
}
