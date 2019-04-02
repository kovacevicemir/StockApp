using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StockApp.Models;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");

        public Login()
        {
            InitializeComponent();

            //check for connection state
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private async void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            //Animation gif.
            loader.Visibility = Visibility.Visible;
            await Task.Run(() => Thread.Sleep(2000));
            loader.Visibility = Visibility.Collapsed;

            //get login details
            Member member_login = new Member();
            member_login.username = username_input.Text;
            member_login.password = password_input.Password.ToString();

            //remove white spaces
            Regex.Replace(member_login.username, @"\s+", "");
            Regex.Replace(member_login.password, @"\s+", "");

            //Create Query command that select user_login_inputs from database 'cmd'
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Users where username='" + member_login.username + "' and password= '" + member_login.password + "'";
            cmd.ExecuteNonQuery();

            //Create data table and insert cmd to it
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            //check how many rows in dt (check if login is correct)
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                errorArea.Visibility = Visibility.Visible;
                errorMsg.Text = "Invalid username or password !";
            }
            else
            {
                //Save user id in Global storage (session like)
                foreach(DataRow row in dt.Rows)
                {

                    Global.thisUser.Id = Convert.ToInt32(row["Id"]);
                    //Instead of using global, we will serialise search objects and store them in db..
                    //Global.thisUser.lastSearchAll = row["lastSearchAll"].ToString();
                    //Global.thisUser.lastSearchHistory = row["lastSearchHistory"].ToString();
                    //Global.thisUser.lastSearchLive = row["lastSearchLive"].ToString();
                }

                await Task.Run(() => Thread.Sleep(1000));
                var homepage = new MainWindow();
                homepage.Show();
                this.Close();
            }
            //
        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
