using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{
    public class GraduationInfo
    {
        public Institution institution;
        public int gradYear;
        public double avgGrade;
        public GraduationInfo(Institution institution, int gradYear, double avgGrade)
        {
            this.institution = institution;
            this.gradYear = gradYear;
            this.avgGrade = avgGrade;
        }
        public override string ToString()
        {
            return "    Institution:\n" + institution.ToString().Replace("\n", "\n    ") + "\nGraduation year: " + gradYear + "\nAverage grade: " + avgGrade;
        }
    }

}
