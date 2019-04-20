using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");

        public Login()
        {
            InitializeComponent();

            //Change window title and icon
            this.Title = Global.AppName + " - Login";
            Uri iconUri = new Uri("pack://application:,,,/dollar.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            //check for connection state
            //if (con.State == System.Data.ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();

        }

        private async void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            //Animation gif.
            loader.Visibility = Visibility.Visible;
            await Task.Run(() => Thread.Sleep(2000));
            loader.Visibility = Visibility.Collapsed;

            //get login details
            Member member_login = new ServiceReference1.Member();
            member_login.username = username_input.Text;
            member_login.password = password_input.Password.ToString();
            member_login.email = "temp";
            member_login.firstname = "temp";
            member_login.lastname = "temp";
            member_login.Id = 1;
            member_login.lastSearchAll = "temp";
            member_login.lastSearchLive = "temp";
            member_login.lastSearchHistory = "temp";

            //remove white spaces
            Regex.Replace(member_login.username, @"\s+", "");
            Regex.Replace(member_login.password, @"\s+", "");

            List<Member> MemberL = new List<Member>();
            Service1Client service = new Service1Client();
            MemberL.Add(service.GetMember(member_login));

            if(MemberL[0].username == member_login.username && MemberL[0].password == member_login.password)
            {
                Global.thisUser.Id = MemberL[0].Id;

                await Task.Run(() => Thread.Sleep(1000));
                var homepage = new MainWindow();
                homepage.Show();
                this.Close();
            }
            else
            {
                errorArea.Visibility = Visibility.Visible;
                errorMsg.Text = "Invalid username or password !";
            }

        }

        //private async void Login_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    //Animation gif.
        //    loader.Visibility = Visibility.Visible;
        //    await Task.Run(() => Thread.Sleep(2000));
        //    loader.Visibility = Visibility.Collapsed;

        //    //get login details
        //    Member member_login = new Member();
        //    member_login.username = username_input.Text;
        //    member_login.password = password_input.Password.ToString();

        //    //remove white spaces
        //    Regex.Replace(member_login.username, @"\s+", "");
        //    Regex.Replace(member_login.password, @"\s+", "");

        //    //Create Query command that select user_login_inputs from database 'cmd'
        //    int i = 0;
        //    SqlCommand cmd = con.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandText = "select * from Users where username='" + member_login.username + "' and password= '" + member_login.password + "'";
        //    cmd.ExecuteNonQuery();

        //    //Create data table and insert cmd to it
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dt);

        //    //check how many rows in dt (check if login is correct)
        //    i = Convert.ToInt32(dt.Rows.Count.ToString());

        //    if (i == 0)
        //    {
        //        errorArea.Visibility = Visibility.Visible;
        //        errorMsg.Text = "Invalid username or password !";
        //    }
        //    else
        //    {
        //        //Save user id in Global storage (session like)
        //        foreach(DataRow row in dt.Rows)
        //        {

        //            Global.thisUser.Id = Convert.ToInt32(row["Id"]);
        //            //Instead of using global, we will serialise search objects and store them in db..
        //            //Global.thisUser.lastSearchAll = row["lastSearchAll"].ToString();
        //            //Global.thisUser.lastSearchHistory = row["lastSearchHistory"].ToString();
        //            //Global.thisUser.lastSearchLive = row["lastSearchLive"].ToString();
        //        }

        //        await Task.Run(() => Thread.Sleep(1000));
        //        var homepage = new MainWindow();
        //        homepage.Show();
        //        this.Close();
        //    }
        //    //
        //}

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration();
            registration.Show();
            this.Close();
        }

        
    }
}
