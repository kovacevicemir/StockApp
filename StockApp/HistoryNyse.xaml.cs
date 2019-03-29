using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for HistoryNyse.xaml
    /// </summary>
    public partial class HistoryNyse : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");

        public HistoryNyse()
        {
            InitializeComponent();

            //check for connection state
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            Display();
        }

        private void HomepageBtn_Click(object sender, RoutedEventArgs e)
        {
            var homepage = new MainWindow();
            homepage.Show();
            this.Close();
        }

        //DATAGRID
        public void Display()
        {
            //Select all from nyse_history table
            //Create new datatable and flll it with cmd2
            string q = "select * from nyse_history";
            DataTable dt1 = FetchProducts(q);

            //Insert data into datagrid_products
            dataGrid_nyse.ItemsSource = dt1.DefaultView;
            dataGrid_nyse.CanUserAddRows = false;
        }

        //FETCH All FROM DB / PUT IT IN DataTable x
        public DataTable FetchProducts(string query)
        {
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = query;
            cmd2.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            da1.Fill(dt1);
            return dt1;
        }
    }
}
