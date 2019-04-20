using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using StockApp.ServiceReference1;
using Member = StockApp.ServiceReference1.Member;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Service1Client service = new Service1Client();
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");
        Member new_member = new Member();
        public Registration()
        {
            InitializeComponent();

            this.Title = Global.AppName + " - Registration";
            Uri iconUri = new Uri("pack://application:,,,/dollar.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);


            //check for connection state
            //if (con.State == System.Data.ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
        }

        //SUBMIT REGISTRATION BUTTON HANDLER
        private void SubmitRegistration_Click(object sender, RoutedEventArgs e)
        {
            //Get all inputs
            new_member.username = username_input.Text;
            new_member.firstname= firstname_input.Text;
            new_member.lastname = lastname_input.Text;
            new_member.email = email_input.Text;
            new_member.password = password_input.Text;

            //VERIFY ALL INPUTS:
            //Username verification
            var username_verification = Verification(new_member.username);
            if(username_verification == false) { return; }

            //Firstname verification
            var firstname_verification = Verification(new_member.firstname);
            if (firstname_verification == false) { return; }

            //Lastname verification
            var lastname_verification = Verification(new_member.lastname);
            if (lastname_verification == false) { return; }

            //Password verification
            var password_verification = Verification(new_member.password);
            if (password_verification == false) { return; }

            //Email
            if(new_member.email == "")
            {
                new_member.email = "none";
            }

            //SearchHistory for new user
            new_member.lastSearchAll = "none";
            new_member.lastSearchHistory = "none";
            new_member.lastSearchLive = "none";



            int i = 0;

            ////Create Query command that select username where username = username_input
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "select * from Users where username='" + new_member.username + "'";
            //cmd.ExecuteNonQuery();

            ////Create data table with all usernames that are = username_input
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);

            DataTable dt = service.GetMemberByUsername(new_member.username);


            //check how many rows in dt (check if there is existing username already in db)
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0) //If non-existing username
            {
                //SqlCommand cmd1 = con.CreateCommand();
                //cmd1.CommandType = CommandType.Text;
                //cmd1.CommandText = "insert into Users values('nickname','emir','kovacevic','emir@nesto','sifra')";
                //cmd1.ExecuteNonQuery();

                int insertMember = service.insertMember(new_member.username, new_member.firstname, new_member.lastname, new_member.email, new_member.password, new_member.lastSearchAll, new_member.lastSearchHistory, new_member.lastSearchLive);
                if(insertMember != 1)
                {
                    MessageBox.Show("Something went wrong! Please restart application and try again");
                }

                //clear error msg
                errorArea.Visibility = Visibility.Collapsed;

                errorMsg.Text = "";

                //show loading gif.
                Loader();

            }
            else
            {
                errorArea.Visibility = Visibility.Visible;
                errorMsg.Text = "Username already exists";
                username_input.Focus();
            }
            
        }

        //BACK TO LOGIN BUTTON HANDLER
        private void BackToLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var loginpg = new Login();
            loginpg.Show();
            this.Close();
        }

        //GIF LOADER AND SUCCESSFULL MESSAGE
        public async void Loader()
        {
            //Show loading gif
            loader.Visibility = Visibility.Visible;
            await Task.Run(() => Thread.Sleep(3000));
            loader.Visibility = Visibility.Collapsed;
            MessageBox.Show("Thank you for creating account with us !!!");
            MessageBox.Show("Your login details are:\n Username : " + new_member.username + "\n password : " + new_member.password);

            //redirect to login page
            var loginpg = new Login();
            loginpg.Show();
            this.Close();
        }

        //VERIFICATION METHOD - 0-9 A-Z
        public bool Verification(string verify)
        {
            if (verify != "")
            {
                bool verification = Regex.IsMatch(verify, @"^[a-zA-Z0-9]+$");
                if (verification == false)
                {
                    errorArea.Visibility = Visibility.Visible;
                    errorMsg.Text = "Please use only A-z and 0-9 for username. No spaces allowed !";
                    return false;
                }
            }
            else
            {
                errorArea.Visibility = Visibility.Visible;
                errorMsg.Text = "Username cannot be empty!";
                return false;
            }
            return true;
        }
    }
}
