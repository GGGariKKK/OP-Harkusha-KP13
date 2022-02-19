using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prac_01
{
    class Calculation
    {

        public static List<Double> calculate_math_expectation(double[,] intervals)
        {
            List<Double> math_expectation = new List<double>();
            for (int record = 0; record < intervals.GetLength(0); record++)
            //Розділяємо матрицю результатів на окремі рядки
            {
                for (int i = 0; i < intervals.GetLength(1); i++)
                {
                    double M_i = 0;
                    for (int inters = 0; inters < intervals.GetLength(1); inters++)//Розраховуємо M_i(проходимо по рядку, кожен раз не включаючи один з елементів)
                    {
                        if (inters != i)
                        {
                            M_i += intervals[record, inters];//Додаємо всі інтервали, окрім одного
                        }
                    }
                    M_i = M_i / (intervals.GetLength(1) - 1);//Ділимо на їх кількість(n - 1)
                    math_expectation.Add(M_i);
                }
            }
            return math_expectation;
        }
        public static List<Double> calculate_dispersion(double[,] intervals, List<Double> m_e)
        {
            List<Double> dispersion_squared = new List<Double>();
            int counter = 0;

            for (int record = 0; record < intervals.GetLength(0); record++)
            //Розділяємо матрицю результатів на окремі рядки
            {
                for (int i = 0; i < intervals.GetLength(1); i++)
                {

                    double numerator = 0;//Чисельник дисперсії

                    for (int k = 0; k < intervals.GetLength(1); k++)
                    {//Розраховуємо дисперсію(її чисельник)
                        if (k != i)
                        {
                            numerator += Math.Pow(intervals[record, k] - m_e[counter], 2);
                        }
                    }


                    double S_squared_i = numerator / (intervals.GetLength(1) - 2);//Дисперсія(в знаменнику н-штрих - 1 = звичайне н - 2)
                    dispersion_squared.Add(S_squared_i);

                    //double student_coeff = Math.Abs((intervals[record, i] - m_e[counter]) / Math.Sqrt(S_squared_i));
                    counter++;
                }
            }
            return dispersion_squared;
        }
        //private static List<Double> calculate_math_expectation(double[] intervals)
        //{
        //    List<Double> math_expectation = new List<double>();
        //    for (int record = 0; record < intervals.GetLength(0); record++)
        //    //Розділяємо матрицю результатів на окремі рядки
        //    {
        //        for (int i = 0; i < intervals.GetLength(1); i++)
        //        {
        //            double M_i = 0;
        //            for (int inters = 0; inters < intervals.GetLength(1); inters++)//Розраховуємо M_i(проходимо по рядку, кожен раз не включаючи один з елементів)
        //            {
        //                if (inters != i)
        //                {
        //                    M_i += intervals[record, inters];//Додаємо всі інтервали, окрім одного
        //                }
        //            }
        //            M_i = M_i / (intervals.GetLength(1) - 1);//Ділимо на їх кількість(n - 1)
        //            math_expectation.Add(M_i);
        //        }
        //    }
        //    return math_expectation;
        //}
        //private static List<Double> calculate_dispersion(double[] intervals, List<Double> m_e)
        //{
        //    List<Double> dispersion_squared = new List<Double>();
        //    int counter = 0;

        //    for (int record = 0; record < intervals.GetLength(0); record++)
        //    //Розділяємо матрицю результатів на окремі рядки
        //    {
        //        for (int i = 0; i < intervals.GetLength(1); i++)
        //        {

        //            double numerator = 0;//Чисельник дисперсії

        //            for (int k = 0; k < intervals.GetLength(1); k++)
        //            {//Розраховуємо дисперсію(її чисельник)
        //                if (k != i)
        //                {
        //                    numerator += Math.Pow(intervals[record, k] - m_e[counter], 2);
        //                }
        //            }


        //            double S_squared_i = numerator / (intervals.GetLength(1) - 2);//Дисперсія(в знаменнику н-штрих - 1 = звичайне н - 2)
        //            dispersion_squared.Add(S_squared_i);

        //            //double student_coeff = Math.Abs((intervals[record, i] - m_e[counter]) / Math.Sqrt(S_squared_i));
        //            counter++;
        //        }
        //    }
        //    return dispersion_squared;
        //}

        public static List<Double> check_student_coeff(double[,] interval_matrix)
        {//Розраховуємо коефіцієнт Стьюдента матриці інтервалів, яка задана як параметр в методі

            List<Double> math_expectation = calculate_math_expectation(interval_matrix);
            List<Double> dispersion_squared = calculate_dispersion(interval_matrix, math_expectation);
            List<Double> stud_coeffs = new List<double>();
            int counter = 0;
            for (int record = 0; record < interval_matrix.GetLength(0); record++)
                for (int i = 0; i < interval_matrix.GetLength(1); i++)
                {
                    if (interval_matrix[record, i] == 0) continue;
                    double student_coeff = Math.Abs((interval_matrix[record, i] - math_expectation[counter]) / Math.Sqrt(dispersion_squared[counter]));
                    counter++;
                    stud_coeffs.Add(student_coeff);
                    //MessageBox.Show("record: " + record + "(" + interval_matrix[record, i] + ")\ncolumn: " + i + "\nstudent = " + student_coeff);
                }
            return stud_coeffs;
        }
        public static double get_student_table(int freedom_degree, double alpha)
        {
            String all_data = File.ReadAllText("Student.txt");
            String first_row = all_data.Split("\n")[0];
            String[] alpha_info = first_row.Split("\t");
            int i = 0;
            for (; i < alpha_info.Length; i++)
            {
                if (alpha_info[i].Contains(alpha.ToString()))
                    break;
            }
            //String print = all_data.Split("\n")[freedom_degree].Split("\t")[i];
            //MessageBox.Show(print);
            return Double.Parse(all_data.Split("\n")[freedom_degree].Split("\t")[i]);
        }
        public static double get_fisher_coeff(int freedom_degree)
        {
            String all_data = File.ReadAllText("fisher.txt");
            return Double.Parse(all_data.Split("\t")[freedom_degree - 1]);

            //1 2	3	4	5	6	7	8	9	10
        }
        //public static List<double> calculate_full_dispersion(double[,] intervals)
        //{
        //    List<double> dispersions = new List<double>();
        //    for (int row = 0; row < intervals.GetLength(0); row++)
        //    {
        //        String to_print = "";
        //        double m_e = 0; //мат очікування, загальне для ряду інтервалів
        //        for (int i = 0; i < intervals.GetLength(1); i++)
        //        {
        //            m_e += intervals[row, i];
        //            to_print += intervals[row, i] + " ";
        //        }
        //        m_e /= intervals.GetLength(1);
        //        double dispersion = 0;
        //        for (int i = 0; i < intervals.GetLength(1); i++)
        //        {
        //            dispersion += Math.Pow(intervals[row, i] - m_e, 2);
        //        }
        //        dispersion = dispersion / (intervals.GetLength(1) - 1);
        //        dispersions.Add(dispersion);
        //    }
        //    return dispersions;
        //}
        public static (List<double>, List<double>) calculate_full_dispersion(double[,] intervals)
        {
            List<double> dispersions = new List<double>();
            List<double> m_es = new List<double>(); 
            for (int row = 0; row < intervals.GetLength(0); row++)
            {
                String to_print = "";
                double m_e = 0; //мат очікування, загальне для ряду інтервалів
                for (int i = 0; i < intervals.GetLength(1); i++)
                {
                    m_e += intervals[row, i];
                    to_print += intervals[row, i] + " ";
                }
                m_e /= intervals.GetLength(1);
                double dispersion = 0;
                for (int i = 0; i < intervals.GetLength(1); i++)
                {
                    dispersion += Math.Pow(intervals[row, i] - m_e, 2);
                }
                dispersion = dispersion / (intervals.GetLength(1) - 1);
                dispersions.Add(dispersion);
                m_es.Add(m_e);
            }
            return (dispersions, m_es);
        }

    }
}
