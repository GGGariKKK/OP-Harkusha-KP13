using System;

namespace Lab_04
{
    public class FullName
    {
        private String name;
        private String surname;
        private String secName;

        public FullName(String name, String surname)
        {
            Name = name;
            Surname = surname;
        }
        public FullName(String name, String surname, String secName) : this(name, surname)
        {           
            SecName = secName;
        }

        public string Name { get => name; set => name = value.Trim(); }
        public string Surname { get => surname; set => surname = value.Trim(); }
        public string SecName { get => secName; set => secName = value.Trim(); }

        public override string ToString()
        {
            return (Name + " " + Surname + " " + SecName).Trim();
        }
        public static FullName getFullName(String name)
        {
            String[] nameParts = name.Split(' ');
            if (nameParts.Length < 2 || nameParts.Length > 3)
                throw new Exception("Illegal name format: " + name);

            var full = new FullName(nameParts[0], nameParts[1]);
            if (nameParts.Length == 3)
                full.SecName = nameParts[2];
            return full;
        }
    }

}
