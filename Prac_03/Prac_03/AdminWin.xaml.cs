using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Prac_03
{

    public partial class AdminWin : Window
    {
        public AdminWin()
        {
            InitializeComponent();
            passTextBoxes = new TextBox[]{ add_pass, modify_pass};
            otherTextBoxes = new TextBox[] { addFullName, addLogin, modifyFullName, modifyLogin };
            hideShowbutts = new Button[] { addUser_password, modUser_password};
            actionButts = new Button[] { addUser, modifyUser, logout, modifyRemove };

            initializeUsersComboBox();

        }
        static TextBox[] passTextBoxes; // without login textbox
        static TextBox[] otherTextBoxes; //without login textbox
        static Button[] hideShowbutts;
        static Button[] actionButts; // without Login and exit button
        DataTable table; // table with users info from db

        string connectionString = "Data Source=DESKTOP-E96R6SM;Initial Catalog=UsersDB;Integrated Security=True";
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        private void retrieveData()//fill DataTable with data
        {
            String SQLQuery = "SELECT * FROM dbo.UsersInfo;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            table.PrimaryKey = new DataColumn[] { table.Columns["Login"] };
            table.Rows[table.Rows.IndexOf(table.Rows.Find("ADMIN"))]["Password"] = "***";
            mainTable.ItemsSource = table.DefaultView;
            connection.Close();
        }

        #region ChangeVisibilityOfPasswords
        private void showPass(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            switch (clicked.Name.ToString())
            {
                case "addUser_password":
                    changeVisibility(add_pass);
                    break;
                case "modUser_password":
                    changeVisibility(modify_pass);
                    break;
                case "enterPass":
                    changeVisibility(login);
                    break;
            }
        }
        private void changeVisibility(TextBox toChange)
        {
            switch (toChange.FontFamily.ToString())
            {
                case "Malgun Gothic":
                    toChange.FontFamily = new FontFamily("Password");
                    break;
                case "Password":
                case "/Prac_03;component/#Password":
                    toChange.FontFamily = new FontFamily("Malgun Gothic");
                    break;
            }
        }
        #endregion
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }      
        private void disableEnable(bool isEnabled)//Disable or enable all objects when logged out or in
        {
            foreach (TextBox tb in passTextBoxes)
                tb.IsEnabled = isEnabled;
            foreach (TextBox tb in otherTextBoxes)
                tb.IsEnabled = isEnabled;
            foreach (Button b in hideShowbutts)
                b.IsEnabled = isEnabled;
            foreach (Button b in actionButts)
                b.IsEnabled = isEnabled;
            userCombo.IsEnabled = isEnabled;
            mainTable.IsEnabled = isEnabled;
            activeUser.IsEnabled = isEnabled;
        }
        private void clearTextBoxes(TextBox[] toClear)
        {
            foreach(TextBox tb in toClear)
                tb.Text = "";
        }
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            enterLogin.IsEnabled = true;
            mainTable.ItemsSource = null;
            login.IsEnabled = true;
            enterPass.IsEnabled = true;
            login.IsEnabled = true;
            clearTextBoxes(passTextBoxes);
            clearTextBoxes(otherTextBoxes);
            login.Text = "";
            activeUser.IsChecked = false;
            userCombo.SelectedItem = null;
            disableEnable(false);
        }
        public bool checkLoginAvailability(String login)
        {
            String SQLQuery = "SELECT COUNT(Login) AS Count FROM dbo.UsersInfo WHERE Login = '" + login + "';";
            bool loginUsed = false;
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand(SQLQuery, connection);
                    DataTable temp = new DataTable();
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(temp);
                    loginUsed = temp.Rows[0]["Count"].ToString() == "0" ? true : false;
                }
                catch (Exception m)
                {
                    MessageBox.Show(m.Message);
                }
            }
            return loginUsed;
        }

        public const int acceptedAttempts = 3;
        public const int minPasswordLength = 5;
        public static bool passwordValidity(String password)
        {
            String forbiddenSymbs = "&\"^%$#@!*()-+=`~./?|\\}{[]';:";
            foreach (char s in forbiddenSymbs.ToCharArray())
                if (password.Contains(s.ToString()))
                    return false;
            return true;
        }


        private int enterLoginWrongPassCount = 0;
        private void enterLogin_Click(object sender, RoutedEventArgs e)
        {
            String SQLQuery = "SELECT Password FROM dbo.UsersInfo WHERE Login = 'ADMIN'";
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            String password = table.Rows[0]["Password"].ToString();
            if (login.Text.ToString() == password)
            {
                disableEnable(true);
                login.IsEnabled = false;
                enterPass.IsEnabled = false;
                if(login.FontFamily.ToString() == "Malgun Gothic")
                    changeVisibility(login);
                enterLogin.IsEnabled = false;
                try
                {
                    retrieveData();
                }
                catch (Exception m)
                {
                    MessageBox.Show(m.Message);
                }
                enterLoginWrongPassCount = 0;
            }
            else
            {
                enterLoginWrongPassCount++;
                MessageBox.Show("Wrong current password.\nYou have " + (acceptedAttempts - enterLoginWrongPassCount) + " attempts left");
                if (enterLoginWrongPassCount >= acceptedAttempts)
                {
                    login.Text = "";
                    new MainWindow().Show();
                    this.Close();
                }
                    
            }

        }
        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            if (addLogin.Text.ToString() == "")//Login can not be empty string
                return;

            //check name format:
            if(addFullName.Text.ToString().Split(' ').Length != 2)
            {
                MessageBox.Show("Wrong full name format!\nMust be: [Surname+WHITESPACE+Name]");
                addFullName.Clear();
                add_pass.Clear();
                return;
            }

            String newUserLogin = addLogin.Text.ToString(); //New login

            //check if such login exists in DB:
            if (!checkLoginAvailability(newUserLogin))
            {
                MessageBox.Show("Login is already used. Try different one");
                addLogin.Clear();
                add_pass.Clear();
                return;
            }

            //Check if password corresponds to the requirements:
            if ( (add_pass.Text.ToString().Length < minPasswordLength || !passwordValidity(add_pass.Text.ToString())) && add_pass.Text.ToString() != "")
            {
                MessageBox.Show("Password is less than required amount of symbols or contains forbidden symbols. Try again");
                addFullName.Clear();
                addLogin.Clear();
                add_pass.Clear();
                return;
            }

            //And finally adding user to the DB
            String[] fullName = addFullName.Text.ToString().Split(' ');
            String SQLQuery = "INSERT INTO dbo.UsersInfo VALUES" +
                "(" +
                "'" + fullName[0] + "', '" + fullName[1] + "', '" + newUserLogin + "', '" + add_pass.Text.ToString() + "', 'Yes');";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand(SQLQuery, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception m)
                {
                    MessageBox.Show(m.Message);
                }
            }          
            addFullName.Clear();
            addLogin.Clear();
            add_pass.Clear();
            userCombo.Items.Add(newUserLogin);
            retrieveData();
            MessageBox.Show("Succesful operation!");
        }

        #region ModifyUserGrid
        private void initializeUsersComboBox()//The combobox to choose user to modify his or her data
        {
            userCombo.Items.Clear();
            DataSet ds = new DataSet();
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT Login FROM dbo.UsersInfo;";
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    var Sdr = command.ExecuteReader();
                    while (Sdr.Read())
                    {
                        for (int i = 0; i < Sdr.FieldCount; i++)
                        {
                            userCombo.Items.Add(Sdr.GetString(i));
                        }
                    }
                    Sdr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }      
        private void userCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userCombo.SelectedItem == null)
                return;
            modifyLogin.IsEnabled = true;
            String chosenLogin = userCombo.SelectedItem.ToString();
            DataTable currUsInfo = new DataTable();
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM dbo.UsersInfo WHERE Login = '" + chosenLogin + "';";
                    connection.Open();
                    command = new SqlCommand(query, connection);               
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(currUsInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            var rowInfo = currUsInfo.Rows[0];
            modifyFullName.Text = rowInfo["Surname"] + " " + rowInfo["Name"];
            modifyLogin.Text = rowInfo["Login"].ToString();
            if (modifyLogin.Text.ToString() == "ADMIN")
                modifyLogin.IsEnabled = false;
            if(modifyLogin.Text.ToString() != "ADMIN")
                modify_pass.Text = rowInfo["Password"].ToString();
            adminPass = rowInfo["Password"].ToString();
            activeUser.IsChecked = rowInfo["Active"].ToString() == "Yes";
            oldLogin = rowInfo["Login"].ToString();
        }
        String oldLogin;
        String adminPass;
        int enterAttempts = 0;
        private void modifyUser_Click(object sender, RoutedEventArgs e)
        {
            if (userCombo.SelectedItem == null)
                return;
            
            //getting modified data:
            String surname = modifyFullName.Text.ToString().Split(' ')[0];
            String name = modifyFullName.Text.ToString().Split(' ')[1];
            String newLogin = modifyLogin.Text.ToString();           
            String active = activeUser.IsChecked == true ? "Yes" : "No";

            if (newLogin == "ADMIN")
            {//Admin can never be not active
                active = "Yes"; //ADMIN always active

                //To make any changes in admin account you need to confirm password
                string inputRead = new InputBox("Confirm current admin password", "Password Confirmation", "Arial", 17).ShowDialog();
                if (inputRead != adminPass) // Check if confirmation of the current password is succesful
                {
                    enterAttempts++;
                    MessageBox.Show("Wrong entered current password!\nYou have " + (acceptedAttempts - enterAttempts) + " more attempts");
                    if (enterAttempts == acceptedAttempts)
                        logout_Click(null, null);
                    return;
                }
            }

            if (oldLogin != newLogin)//Check if new entered login is already used in db
                if (!checkLoginAvailability(newLogin))
                {
                    MessageBox.Show("Entered login is already in use. Try another one");
                    modifyLogin.Text = "";
                    return;
                }
            //using (connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        string query = "SELECT COUNT(Login) AS Count FROM dbo.UsersInfo WHERE Login = '" + newLogin + "';";
            //        DataTable loginCount = new DataTable();
            //        connection.Open();
            //        command = new SqlCommand(query, connection);                  
            //        new SqlDataAdapter(command).Fill(loginCount);

            //        if (loginCount.Rows[0]["Count"].ToString() != "0")
            //        {
            //            MessageBox.Show("Such login already in use. Try another one");
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        return;
            //    }
            //}

            String password = modify_pass.Text.ToString();
            if (password.Length < minPasswordLength || !passwordValidity(password)) //Check password validity
            {
                MessageBox.Show("Password is less than required amount of symbols or contains forbidden symbols. Try again");
                modify_pass.Clear();
                return;
            }

            adminPass = password;
            if (newLogin == "ADMIN")//If we are modifying admin fields, then update password in 'login' field
                login.Text = password;

            using (connection = new SqlConnection(connectionString))//Updating user data
            {
                try
                {
                    string query =
                        "UPDATE dbo.UsersInfo " +
                        "SET Surname = '" + surname + "', " +
                        "Name = '" + name + "', " +
                        "Login = '" + newLogin + "', " +
                        "Password = '" + password + "', " +
                        "Active = '" + active + "' " +
                        "WHERE Login = '" + oldLogin + "';";
                    connection.Open();
                    command = new SqlCommand(query, connection);                  
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            modifyFullName.Clear();
            modifyLogin.Clear();
            modify_pass.Clear();
            activeUser.IsChecked = false;
            oldLogin = newLogin;
            retrieveData();
            initializeUsersComboBox();
            MessageBox.Show("Operation succesful!");
        }
        private void modifyRemove_Click(object sender, RoutedEventArgs e)
        {
            if (userCombo.SelectedItem == null)
                return;
            else if(modifyLogin.Text.ToString() == "ADMIN")
            {
                MessageBox.Show("Cannot delete admin account!");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
                return;
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query =
                        "DELETE FROM dbo.UsersInfo " +
                        "WHERE Login = '" + modifyLogin.Text.ToString() + "';";
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            userCombo.Items.Remove(modifyLogin.Text.ToString());
            modifyFullName.Clear();
            modifyLogin.Clear();
            modify_pass.Clear();
            activeUser.IsChecked = false;
            retrieveData();
        }
        #endregion


    }
}
