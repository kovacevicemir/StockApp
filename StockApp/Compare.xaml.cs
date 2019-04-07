using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Xml.Serialization;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for Compare.xaml
    /// </summary>
    public partial class Compare : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");
        public Compare()
        {
            InitializeComponent();

            //Change window title and icon
            this.Title = Global.AppName + " - Login";
            Uri iconUri = new Uri("pack://application:,,,/dollar.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            //check for connection state
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            
            Display();

        }
        public void Display()
        {
            //GET TABLE 1
            string searchQ1 = "SELECT TOP 1 * FROM Compare WHERE UserId = " +Global.thisUser.Id+ " ORDER BY id DESC";
            DataTable dt1 = FetchProducts(searchQ1);

            //Get XML string from dbtable, prepare for deserialization
            string tableOneXML = "";
            foreach(DataRow row in dt1.Rows)
            {
                tableOneXML = row["CompareOne"].ToString();
            }

            //Deserialize XMLtable
            DataTable tableOne = new DataTable();

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "data";
            // xRoot.Namespace = "http://www.cpandl.com";
            xRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(DataTable),xRoot);
            using (TextReader reader = new StringReader(tableOneXML))
            {
                tableOne = (DataTable)serializer.Deserialize(reader);
            }

            //Feed Table one
            dataTable1.ItemsSource = tableOne.DefaultView;
            dataTable1.CanUserAddRows = false;

            //GET TABLE 2
            string searchQ2 = "SELECT TOP 1 * FROM Compare WHERE UserId = " + Global.thisUser.Id + " ORDER BY id DESC";
            DataTable dt2 = FetchProducts(searchQ2);

            //Get XML string from dbtable, prepare for deserialization
            string tableTwoXML = "";
            foreach (DataRow row in dt2.Rows)
            {
                tableOneXML = row["CompareTwo"].ToString();
            }

            //If Second table does not exist in Compare.dbo STOP here
            if(tableTwoXML == "")
            {
                MessageBox.Show("Table one is ready ! \n Please select Table Two...");
                return;
            }

            //Deserialize XMLtable
            DataTable tableTwo = new DataTable();

            XmlRootAttribute xRoot2 = new XmlRootAttribute();
            xRoot.ElementName = "data";
            // xRoot.Namespace = "http://www.cpandl.com";
            xRoot.IsNullable = true;

            XmlSerializer serializer2 = new XmlSerializer(typeof(DataTable), xRoot2);
            using (TextReader reader = new StringReader(tableTwoXML))
            {
                tableTwo = (DataTable)serializer2.Deserialize(reader);
            }

            //Feed Table one
            dataTable2.ItemsSource = tableTwo.DefaultView;
            dataTable2.CanUserAddRows = false;

            Global.CompareTwoTable = true;

        }

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
