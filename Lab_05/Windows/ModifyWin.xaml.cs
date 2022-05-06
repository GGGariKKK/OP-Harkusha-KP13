
using Lab_04.Applicant_structure;
using Lab_04.DataWindowPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Lab_04
{
    public delegate void callAction(String selected); //delegate for picker window
    public delegate void changeInstitution(DataRowView row); // for changing institutions
    public delegate void changeStreet(DataRowView row); // for changing applicant street

    public partial class ModifyWin : Window
    {
        private Applicant applicant;
        private List<Exam> exams = new List<Exam>();

        public ModifyWin(Applicant applicant)
        {
            InitializeComponent();
            this.applicant = applicant;
            fillFields();
        }

        public void fillFields()
        {
            fillGeneralInfo();
            fillGraduationInfo();

            String query = "SELECT Description FROM dbo.Privilege";
            AppPrivilege.Items.Add("None");
            initializeComboBox(query, AppPrivilege);

            query = "SELECT Level_Name FROM dbo.Olimpiad_Level";
            AppOlimpiadType.Items.Add("None");
            initializeComboBox(query, AppOlimpiadType);

            fillPrivilegeInfo();
            fillOlimpiadInfo();

            query = "SELECT " +
                "dbo.TestSchedule.Name " +
                "FROM dbo.Applicant_Test " +
                "INNER JOIN dbo.ApplicantInfo " +
                "ON dbo.Applicant_Test.IDApplicant = dbo.ApplicantInfo.ID " +
                "INNER JOIN dbo.TestInfo " +
                "ON dbo.Applicant_Test.IDTestInfo = dbo.TestInfo.ID " +
                "INNER JOIN dbo.TestSchedule " +
                "ON dbo.TestInfo.IDSubject = dbo.TestSchedule.ID " +
                "WHERE " +
                "dbo.ApplicantInfo.ID = " + applicant.ID + ";";

            initializeComboBox(query, examSubject);
            if (examSubject.Items.Count < 5)
                examSubject.Items.Add("Add exam");

            exams = applicant.Exams.ToList();

        }


        #region GenInfo tab
        public void fillGeneralInfo()
        {
            AppID.Content += applicant.ID.ToString();
            AppName.Text = applicant.FullName.Name;
            AppSurname.Text = applicant.FullName.Surname;
            AppSecName.Text = applicant.FullName.SecName;
            AppBirthdate.SelectedDate = applicant.BirthDate;
            AppPhoneNumber.Text = applicant.PhoneNumber;
            AppRegion.Text = applicant.Address.Region;
            AppCity.Text = applicant.Address.City;
            AppStreet.Text = applicant.Address.Street;
            AppBuildingNumber.Text = applicant.Address.BuildingNum.ToString();
        }
        private void changeHomeAddressButt_Click(object sender, RoutedEventArgs e)
        {
            changeStreet change = changeHomeAddress;
            new DataWin(new StreetInfo(change), false).Show();
        }
        private void changeHomeAddress(DataRowView row)
        {
            AppRegion.Text = row[0].ToString();
            AppCity.Text = row[1].ToString();
            AppStreet.Text = row[2].ToString();
        }

        #endregion

        #region Graduation info tab
        public void fillGraduationInfo()
        {
            gradInstType.Text = applicant.GradInfo.institution.Type.ToString();
            gradInstNumber.Text = applicant.GradInfo.institution.Number.ToString();
            gradInstCity.Text = applicant.GradInfo.institution.City;
            gradInstRegion.Text = applicant.GradInfo.institution.Region;
            gradYear.Text = applicant.GradInfo.gradYear.ToString();
            gradAverageGrade.Text = applicant.GradInfo.avgGrade.ToString();
        }
        private void changeGraduationInstitution_Click(object sender, RoutedEventArgs e)
        {
            changeInstitution change = changeGraduationInstitution;
            new DataWin(new InstitutionInfo(change), false).Show();
        }
        public void changeGraduationInstitution(DataRowView row)
        {
            gradInstType.Text = row["Institution"].ToString();
            gradInstNumber.Text = row["Number"].ToString();
            gradInstCity.Text = row["City"].ToString();
            gradInstRegion.Text = row["Region"].ToString();
        }

        #endregion

        #region Exams tab
        public void fillExams(String exam)
        {
            Exam ex = null;
            foreach (Exam curr in exams)
                if (SubjectDetails.stringSubjects[(int)curr.SubjDetails.Subject] == exam)
                {
                    ex = curr;
                    break;
                }
            if (ex == null)
                return;

            examResult.Text = ex.Result.ToString();
            examDate.Text = ex.SubjDetails.Date.ToString().Split(' ')[0];
            examStart.Text = ex.SubjDetails.Start;
            examEnd.Text = ex.SubjDetails.End;
            examInstType.Text = ex.LocatDetails.Institution.Type.ToString();
            examInstNumber.Text = ex.LocatDetails.Institution.Number.ToString();
            examInstCity.Text = ex.LocatDetails.Institution.City;
            examInstRegion.Text = ex.LocatDetails.Institution.Region;
            examRoomNumber.Text = ex.LocatDetails.RoomNumber.ToString();
            examResponsible.Text = ex.LocatDetails.Responsible.ToString();
        }
        public void fillAddedExam(String exam)
        {
            examResult.Text = "";
            examRoomNumber.Text = "";
            examResponsible.Text = "";
            examInstType.Text = "";
            examInstNumber.Text = "";
            examInstCity.Text = "";
            examInstRegion.Text = "";

            String query =
                "SELECT " +
                "Date, StartTime, EndTime " +
                "FROM " +
                "dbo.TestSchedule " +
                "WHERE" +
                "(Name = '" + exam + "')";

            var table = DBManager.extractTable(query);

            examDate.Text = table.Rows[0]["Date"].ToString().Split(' ')[0];
            examStart.Text = table.Rows[0]["StartTime"].ToString();
            examEnd.Text = table.Rows[0]["EndTime"].ToString();
        }
        private void changeExamInstitution_Click(object sender, RoutedEventArgs e)
        {
            changeInstitution change = changeExamInstitution;
            new DataWin(new ExamLocation(change, examSubject.SelectedItem.ToString()), false).Show();
        }
        public void changeExamInstitution(DataRowView row)
        {
            examInstType.Text = row["Institution"].ToString();
            examInstNumber.Text = row["Number"].ToString();
            examInstCity.Text = row["City"].ToString();
            examInstRegion.Text = row["Region"].ToString();
            examRoomNumber.Text = row["RoomNumber"].ToString();
            examResponsible.Text = row["Responsible"].ToString();
        }

        String previousSelection;
        private void examSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null)
            {
                examResult.Text = "";
                examDate.Text = "";
                examStart.Text = "";
                examEnd.Text = "";
                examInstType.Text = "";
                examInstNumber.Text = "";
                examInstCity.Text = "";
                examInstRegion.Text = "";
                examRoomNumber.Text = "";
                examResponsible.Text = "";
                previousSelection = null;
                changeExamLocation.IsEnabled = false;
                return;
            }
            else if (examSubject.SelectedItem.ToString() == previousSelection)
                return;
            else if (!validateExamFields() && previousSelection != null)
            {
                MessageBox.Show("Current exam page is not fully filled or contains errors");
                examSubject.SelectedItem = previousSelection;
                return;
            }

            if (previousSelection != null && previousSelection != "Add exam" && !removingSubject)
                saveExam(previousSelection);

            if ((sender as ComboBox).SelectedItem.ToString().Equals("Add exam"))
            {
                var tempCombo = new ComboBox();
                foreach (String s in SubjectDetails.stringSubjects)
                    if (!(sender as ComboBox).Items.Contains(s))
                        tempCombo.Items.Add(s);
                Picker.Show(tempCombo, addExam);
            }
            else
            {
                fillExams((sender as ComboBox).SelectedItem.ToString());
                changeExamLocation.IsEnabled = true;
            }

            previousSelection = (sender as ComboBox).SelectedItem.ToString();
        }
        private void saveExam(String exam)
        {
            //Gathering exam info:
            var currExam = new Exam();

            currExam.Result = int.Parse(examResult.Text);

            currExam.SubjDetails = new SubjectDetails
                (
                (ExamSubject)Array.IndexOf(SubjectDetails.stringSubjects, exam),
                examDate.Text,
                examStart.Text,
                examEnd.Text
                );

            currExam.LocatDetails = new LocationDetails
                (
                new Institution((InstitutionType)Array.IndexOf(Institution.stringType, examInstType.Text), int.Parse(examInstNumber.Text), examInstCity.Text, examInstRegion.Text),
                int.Parse(examRoomNumber.Text),
                FullName.getFullName(examResponsible.Text)
                );

            //adding to list or modifying it:
            int index = -1;
            for (int i = 0; i < exams.Count; i++)
            {
                if (SubjectDetails.stringSubjects[(int)exams[i].SubjDetails.Subject] == exam)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                exams.Add(currExam);
            else
                exams[index] = currExam;
        }

        private bool removingSubject = false;
        private void examRemove_Click(object sender, RoutedEventArgs e)
        {
            if (examSubject.SelectedItem == null || exams.Count == 1)
                return;
            for (int i = 0; i < exams.Count; i++)
            {
                if (SubjectDetails.stringSubjects[(int)exams[i].SubjDetails.Subject] == examSubject.SelectedItem.ToString())
                {
                    removingSubject = true;

                    exams.RemoveAt(i);
                    examSubject.Items.Remove(examSubject.SelectedItem.ToString());
                    previousSelection = null;

                    removingSubject = false;

                    if (exams.Count < 5 && !examSubject.Items.Contains("Add exam"))
                        examSubject.Items.Add("Add exam");

                    break;
                }
            }

        }
        public void addExam(String selected)
        {
            if (selected == null)
            {
                examSubject.SelectedItem = null;
                return;
            }
            else if (examSubject.Items.Count == 5)
            {
                examSubject.Items.Remove("Add exam");
                examSubject.Items.Add(selected);
            }
            else
                examSubject.Items.Insert(examSubject.Items.Count - 1, selected);

            examSubject.SelectedItem = null;

            examSubject.SelectedItem = selected;

            fillAddedExam(selected);
        }

        #endregion

        #region Privilege and Olimpiad
        public void fillOlimpiadInfo()
        {
            if (applicant.Olimpiad == null)
            {
                AppOlimpiadType.SelectedItem = "None";
                return;
            }
            AppOlimpiadType.SelectedItem = applicant.Olimpiad.Type.ToString();
            AppOlimpiadPlace.Text = applicant.Olimpiad.PrizePlace.ToString();
            AppOlimpiadPlace.IsEnabled = true;
        }
        private void AppOlimpiadType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem.ToString() == "None")
            {
                AppOlimpiadPlace.IsEnabled = false;
                AppOlimpiadPlace.Text = "";
            }
            else
                AppOlimpiadPlace.IsEnabled = true;
        }
        public void fillPrivilegeInfo()
        {
            if (applicant.Privilege == null)
            {
                AppPrivilege.SelectedItem = "None";
                return;
            }
            AppPrivilege.SelectedItem = applicant.Privilege.Description.ToString();
        }


        #endregion


        #region Gathering data
        private void SaveApplicant(object sender, RoutedEventArgs e)
        {
            if (examSubject.SelectedItem != null && validateExamFields())
                saveExam(examSubject.SelectedItem.ToString());
            if (!validateData())
            {
                MessageBox.Show("Filled form contains mistakes. Correct them and try again");
                return;
            }

            var applicant = gatherData();

            try
            {
                DBManager.modifyApplicant(applicant);
            }catch (Exception)
            {
                MessageBox.Show("Something went wrong. Operation unseccessful");
                DBManager.removeApplicant(applicant.ID);
            }
        }
        private Applicant gatherData()
        {
            var ID = applicant.ID;

            var name = new FullName
                (
                AppName.Text,
                AppSurname.Text,
                AppSecName.Text
                );

            var birthdate = AppBirthdate.SelectedDate.GetValueOrDefault(DateTime.Today);

            var phoneNumber = AppPhoneNumber.Text;

            var address = new Address
                (
                AppRegion.Text,
                AppCity.Text,
                AppStreet.Text,
                int.Parse(AppBuildingNumber.Text)
                );

            var gradInfo = new GraduationInfo
                (
                new Institution
                    (
                        (InstitutionType)Array.IndexOf(Institution.stringType, gradInstType.Text),                    
                        int.Parse(gradInstNumber.Text),
                        gradInstCity.Text,
                        gradInstRegion.Text
                    ),
                int.Parse(gradYear.Text),
                double.Parse(gradAverageGrade.Text)
                );

            var exams = this.exams.ToArray();

            var olimpiad =
                AppOlimpiadType.SelectedItem.ToString() == "None"
                ? null
                : new Olimpiad
                    (
                        (OlimpiadType)Array.IndexOf(Olimpiad.stringOlimpiadType, AppOlimpiadType.Text),
                        int.Parse(AppOlimpiadPlace.Text)
                    );

            var privilege = AppPrivilege.SelectedItem.ToString() == "None"
                ? null
                : new Privilege(AppPrivilege.SelectedItem.ToString());

            Applicant newApplicant = new Applicant(ID, name, birthdate, olimpiad, privilege, gradInfo, exams, phoneNumber, address);

            return newApplicant;
        }
        
        #region Validating data
        private Boolean validateData()
        {
            return validateGenInfo() && validateGradInfo() && validateExam() && validateOlimpiad();
        }

        #region GenInfo tab
        public bool validateGenInfo()
        {
            bool[] validFields =
            {
                AppName.Text.Length > 0,
                AppSurname.Text.Length > 0,
                AppBirthdate.SelectedDate != null && AppBirthdate.SelectedDate < DateTime.Today,
                Regex.Match(AppPhoneNumber.Text.ToString(), @"\+\d{12}$").Success,
                int.TryParse(AppBuildingNumber.Text, out _) && int.Parse(AppBuildingNumber.Text) > 0,
                AppRegion.Text.Length > 0
            };
            return validFields.All(x => x);
        }
        #endregion

        #region Graduation info tab
        public bool validateGradInfo()
        {
            bool[] validFields =
            {
                int.TryParse(gradYear.Text, out _) && int.Parse(gradYear.Text) >= 2005 && int.Parse(gradYear.Text) <= DateTime.Today.Year,
                double.TryParse(gradAverageGrade.Text, out _) && double.Parse(gradAverageGrade.Text) > 0 && double.Parse(gradAverageGrade.Text) <= 12,
                int.Parse(gradInstNumber.Text) > 0
            };
            return validFields.All(x => x);
        }
        #endregion

        #region Exams tab
        
        public bool validateExamFields()
        {
            bool[] validFields =
            {
                int.TryParse(examResult.Text, out _) && Helper.inRange(int.Parse(examResult.Text), 100, 200),
                int.TryParse(examRoomNumber.Text, out _) && int.Parse(examRoomNumber.Text) > 0,
                Helper.inRange(examResponsible.Text.ToString().Split(' ').Length, 2, 3),
                examInstType.Text.ToString().Length > 0
            };

            return validFields.All(x => x);
        }
        public bool validateExam()
        {
            return (validateExamFields() || examSubject.SelectedItem == null) && Helper.inRange(exams.Count, 3, 5);
        }

        #endregion

        #region Privilege and Olimpiad
        public bool validateOlimpiad()
        {
            bool[] validFields =
            {
                int.TryParse(AppOlimpiadPlace.Text, out _) && Helper.inRange(int.Parse(AppOlimpiadPlace.Text), 1, 3) || AppOlimpiadType.SelectedItem.ToString() == "None"
            };
            return validFields.All(x => x);
        }
        #endregion

        #endregion

        #endregion


        #region AdditionalMethods

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new DataWin(new ApplicantInfo()).Show();
        }
        public void initializeComboBox(String query, ComboBox combo)
        {
            var table = DBManager.extractTable(query);
            foreach (DataRow row in table.Rows)
                combo.Items.Add(row[0]);
        }
        private void DeleteApplicant_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                DBManager.removeApplicant(applicant.ID);
                Close();
            }
        }
        #endregion


    }
}
