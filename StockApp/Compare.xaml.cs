using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

        DataTable tableTwo = new DataTable();
        DataTable tableOne = new DataTable();
        int isLabel1 = 0;
        int isLabel2 = 0;
        List<string> colNames1 = new List<string>();
        List<string> colNames2 = new List<string>();


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
            string searchQ1 = "SELECT TOP 1 * FROM Compare WHERE UserId = " + Global.thisUser.Id + " ORDER BY id DESC";
            DataTable dt1 = FetchProducts(searchQ1);

            //Get XML string from dbtable, prepare for deserialization
            string tableOneXML = "";
            foreach (DataRow row in dt1.Rows)
            {
                tableOneXML = row["CompareOne"].ToString();
            }

            //Deserialize XMLtable
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "data";
            xRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(DataTable), xRoot);
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
                tableTwoXML = row["CompareTwo"].ToString();
            }

            //If Second table does not exist in Compare.dbo STOP here
            if (tableTwoXML == "")
            {
                MessageBox.Show("Table Two is empty");
                return;
            }

            //Deserialize XMLtable
            XmlRootAttribute xRoot2 = new XmlRootAttribute();
            xRoot2.ElementName = "data";
            xRoot2.IsNullable = true;

            XmlSerializer serializer2 = new XmlSerializer(typeof(DataTable), xRoot2);
            using (TextReader reader = new StringReader(tableTwoXML))
            {
                tableTwo = (DataTable)serializer2.Deserialize(reader);
            }

            //Feed Table two
            dataTable2.ItemsSource = tableTwo.DefaultView;
            dataTable2.CanUserAddRows = false;

            Global.CompareTwoTable = true;

            //COMBO BOXES
            //VisualiseBy ComboBox One -> fetch from table one
            colNames1.Clear();
            visualiseInputBy1.Items.Clear();

            foreach (DataColumn column in tableOne.Columns)
            {
                colNames1.Add(column.ColumnName.ToString());
            }

            foreach (string item in colNames1)
            {
                visualiseInputBy1.Items.Add(item);
            }

            visualiseInputBy1.Items.Remove("date");
            visualiseInputBy1.Items.Remove("stock_symbol");
            visualiseInputBy1.Items.Remove("exchange");

            //VisualiseBy ComboBox One -> fetch from table one
            colNames2.Clear();
            visualiseInputBy2.Items.Clear();

            foreach (DataColumn column in tableTwo.Columns)
            {
                colNames2.Add(column.ColumnName.ToString());
            }

            foreach (string item in colNames2)
            {
                visualiseInputBy2.Items.Add(item);
            }

            visualiseInputBy2.Items.Remove("date");
            visualiseInputBy2.Items.Remove("stock_symbol");
            visualiseInputBy2.Items.Remove("exchange");

            //Display numbers of results for table one
            noResults1.Text = "Number of results: " + tableOne.Rows.Count.ToString();
            noResults2.Text = "Number of results: " + tableTwo.Rows.Count.ToString();

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

        private void Chart1btn_Click(object sender, RoutedEventArgs e)
        {
            ////////////
            //TABLE ONE
            ////////////

            //Check if Visualise by: is selected
            if (visualiseInputBy1.SelectedItem == null)
            {
                MessageBox.Show("Please select Visualise By: ");
                return;
            }

            string chartDateOne = "";
            string chartDateTwo = "";

            //List of chart values
            List<double> numberList1 = new List<double>();

            //Get visualise by: combobox input
            string visualiseByInput1 = visualiseInputBy1.SelectedValue.ToString();

            //Add values to list ( DataChart will be drawed from this list)

            if (tableOne.Rows.Count > 0)
            {
                var t1 = 0;
                foreach (DataRow row in tableOne.Rows)
                {
                    double item = double.Parse(row[visualiseByInput1].ToString(), CultureInfo.InvariantCulture);
                    numberList1.Add(item);
                    t1++;

                    //draw first 1000 table => datachart
                    if (t1 >= 1000) break;
                }
                //Get First Date and Last date from search... (current datatable)
                DataRow lastRowDT1 = tableOne.Rows[tableOne.Rows.Count - 1];
                DataRow firstRowDT1 = tableOne.Rows[0];

                chartDateOne = firstRowDT1["date"].ToString();
                chartDateTwo = lastRowDT1["date"].ToString();
            }
            else
            {
                MessageBox.Show("Table one that you selected to compare is empty!");
            }

            //Clear labels if exist (Refresh axisX)
            if(isLabel1 == 1)
            {
                chart1.AxisX.RemoveAt(1);
            }

            chart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = numberList1.AsChartValues(),
                    PointGeometrySize = 2
                }
            };

            //Add first and last date from search query to chart
            chart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Separator = new LiveCharts.Wpf.Separator { Step = 1 },
                Title = "Timeline",
                MinValue = 0,
                Labels = new[]
                {
                    chartDateOne,
                    chartDateTwo
                }
            });

            //1 means that AxiX labels has been added, and they will be removed on next execution
            isLabel1 = 1;

        }

        private void Chart2btn_Click(object sender, RoutedEventArgs e)
        {
            ////////////
            //TABLE TWO
            ////////////

            //Check if Visualise by: is selected
            if (visualiseInputBy2.SelectedItem == null)
            {
                MessageBox.Show("Please select Visualise By: ");
                return;
            }

            string chartDateOne = "";
            string chartDateTwo = "";

            //List of chart values
            List<double> numberList2 = new List<double>();

            //Get visualise by: combobox input
            string visualiseByInput2 = visualiseInputBy2.SelectedValue.ToString();

            //Add values to list ( DataChart will be drawed from this list)

            if (tableOne.Rows.Count > 0)
            {
                var t2 = 0;
                foreach (DataRow row in tableOne.Rows)
                {
                    double item = double.Parse(row[visualiseByInput2].ToString(), CultureInfo.InvariantCulture);
                    numberList2.Add(item);
                    t2++;

                    //draw first 1000 table => datachart
                    if (t2 >= 1000) break;
                }
                //Get First Date and Last date from search... (current datatable)
                DataRow lastRowDT1 = tableOne.Rows[tableOne.Rows.Count - 1];
                DataRow firstRowDT1 = tableOne.Rows[0];

                chartDateOne = firstRowDT1["date"].ToString();
                chartDateTwo = lastRowDT1["date"].ToString();
            }
            else
            {
                MessageBox.Show("Table Two that you selected to compare is empty!");
            }

            //Clear labels if exist (Refresh axisX)
            if (isLabel2 == 1)
            {
                chart2.AxisX.RemoveAt(1);
            }

            chart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = numberList2.AsChartValues(),
                    PointGeometrySize = 2
                }
            };

            //Add first and last date from search query to chart
            chart2.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Separator = new LiveCharts.Wpf.Separator { Step = 1 },
                Title = "Timeline",
                MinValue = 0,
                Labels = new[]
                {
                    chartDateOne,
                    chartDateTwo
                }
            });

            //1 means that AxiX labels has been added, and they will be removed on next execution
            isLabel1 = 2;
        }
    }
}
