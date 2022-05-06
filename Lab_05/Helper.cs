using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{
    class Helper
    {
        public static bool inRange(int numberToCheck, int bottom, int top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }
        public static DataTable cropDate(DataTable Table)//Crop time part from date("6/6/22 12:00:00") = ("6/6/22")
        {
            DataTable dtCloned = Table.Clone();
            dtCloned.Columns[2].DataType = typeof(string);
            foreach (DataRow row in Table.Rows)
            {
                DataRow temp = dtCloned.NewRow();
                temp[0] = row[0];
                temp[1] = row[1];
                temp[2] = row[2].ToString().Split(' ')[0];
                temp[3] = row[3];
                temp[4] = row[4];
                dtCloned.Rows.Add(temp);
            }
            return Table;
        }
    }
}
