using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04
{
    public enum ExamSubject
    {
        Mathematics,
        UkrainianLanguageAndLiterature,
        Spanish,
        German,
        French,
        English,
        Physics,
        HistoryOfUkraine,
        Biology,
        Geography,
        Chemistry
    }
    public class SubjectDetails
    {
        public static String[] stringSubjects = new string[] { "Mathematics", "Ukrainian language and literature", "Spanish", "German", "French", "English", "Physics", "History of Ukraine", "Biology", "Geography", "Chemistry" };

        private ExamSubject subject;
        private String date;
        private String start;
        private String end;

        public SubjectDetails()
        {

        }
        public SubjectDetails(ExamSubject subject, string date, string start, string end)
        {
            Subject = subject;
            Date = date;
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return "    Subject: " + stringSubjects[(int)subject] + "\nDate: " + date + "\nStart time: " + start + "\nEnd time: " + end;
        }
        public ExamSubject Subject { get => subject; set => subject = value; }
        public string Date { get => date; set => date = value.Trim(); }
        public string Start { get => start; set => start = value.Trim(); }
        public string End { get => end; set => end = value.Trim(); }
    }
}
