using System.Windows;
using System.Windows.Controls;

namespace Lab_04
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DBManager.removeApplicant(99);
            genInfo.Click += (x, y) => {new DataWin(new ApplicantInfo()).Show(); Close();};
            testInfo.Click += (x, y) => {new DataWin(new ExamInfo()).Show(); Close();};
            olimpPrivInfo.Click += (x, y) => {new DataWin(new OlimpiadAndPrivelegeInfo()).Show(); Close(); };
            educInfo.Click += (x, y) => {new DataWin(new InstitutionInfo()).Show(); Close(); };
            streetInfo.Click += (x, y) => {new DataWin(new StreetInfo()).Show(); Close(); };
        }

    }
}
