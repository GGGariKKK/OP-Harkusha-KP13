using System;

namespace Lab_04
{
    public enum InstitutionType
    {
        ZOSh,
        Lyceum,
        Gymnasium
    }
    public class Institution
    {
        public static String[] stringType = new string[] { "ZOSh", "Lyceum", "Gymnasium" };
        private InstitutionType type;
        private int number;
        private String city;
        private String region;

        public Institution(InstitutionType type, int number, String city, String region)
        {
            this.Type = type;
            this.Number = number;
            this.City = city;
            this.Region = region;
        }

        public InstitutionType Type { get => type; set => type = value; }
        public int Number { get => number; set => number = value; }
        public string City { get => city; set => city = value.Trim(); }
        public string Region { get => region; set => region = value.Trim(); }

        public override string ToString()
        {
            return "    Institution type: " + Type + "\nInsitution number: " + Number + "\nLocation city: " + City + "\nLocation region: " + Region;
        }
        public string ToString(Boolean withoutCity)
        {
            return "    Institution type: " + Type + "\nInsitution number: " + Number;
        }

    }
}
