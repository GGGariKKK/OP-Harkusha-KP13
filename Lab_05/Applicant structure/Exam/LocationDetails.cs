using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{
    public class LocationDetails
    {
        private Institution institution;
        private int roomNumber;
        private FullName responsible;
        
        public LocationDetails()
        {

        }
        public LocationDetails(Institution location, int roomNumber, FullName responsible)
        {
            Institution = location;
            RoomNumber = roomNumber;
            Responsible = responsible;
        }

        public override string ToString()
        {
            return "    Institution:\n" + institution.ToString(true).Replace("\n", "\n    ") + "\nRoom number: " + roomNumber + "\nResponsible: " + responsible;
        }
        public Institution Institution { get => institution; set => institution = value; }
        public int RoomNumber { get => roomNumber; set => roomNumber = value; }
        public FullName Responsible { get => responsible; set => responsible = value; }
    }
}
