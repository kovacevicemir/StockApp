using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Microsoft.Win32;
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
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");
        List<string> colNames1 = new List<string>();
        DataTable dataTable = new DataTable();
        int isLabel1 = 0;
        public Import()
        {
            InitializeComponent();

            //Change window title and icon
            this.Title = Global.AppName + " - Import";
            Uri iconUri = new Uri("pack://application:,,,/dollar.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            //check for connection state
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void Import_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.

                string xmlfile = File.ReadAllText(files[0]);

                //Deserialize XMLtable
                XmlRootAttribute xRoot2 = new XmlRootAttribute();
                xRoot2.ElementName = "data";
                xRoot2.IsNullable = true;

                XmlSerializer serializer2 = new XmlSerializer(typeof(DataTable), xRoot2);
                using (TextReader reader = new StringReader(xmlfile))
                {
                    dataTable = (DataTable)serializer2.Deserialize(reader);
                }

                //Feed Table two
                tableOne.ItemsSource = dataTable.DefaultView;
                tableOne.CanUserAddRows = false;

                //Toggle between import / drag and drop and visual, data
                ImportDrop.Visibility = Visibility.Collapsed;
                ImportBtn.Visibility = Visibility.Collapsed;
                tableOne.Visibility = Visibility.Visible;
                compareBtn.Visibility = Visibility.Visible;
                noResults1.Visibility = Visibility.Visible;
                chart1.Visibility = Visibility.Visible;
                VisBy.Visibility = Visibility.Visible;
                dataChartLabel.Visibility = Visibility.Visible;

            }
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create new dialog
            var sfd = new OpenFileDialog
            {
                Filter = "XML-File | *.xml",
            };

            if (sfd.ShowDialog() == true)
            {
                //filename is path+filename
                string filename = sfd.FileName;
                string xmlfile = File.ReadAllText(filename);

                //Deserialize XMLtable
                XmlRootAttribute xRoot2 = new XmlRootAttribute();
                xRoot2.ElementName = "data";
                xRoot2.IsNullable = true;

                XmlSerializer serializer2 = new XmlSerializer(typeof(DataTable), xRoot2);
                using (TextReader reader = new StringReader(xmlfile))
                {
                    dataTable = (DataTable)serializer2.Deserialize(reader);
                }

                //Feed Table two
                tableOne.ItemsSource = dataTable.DefaultView;
                tableOne.CanUserAddRows = false;

                FetchTableHeaders();

                //Toggle between import / drag and drop and visual, data
                ImportDrop.Visibility = Visibility.Collapsed;
                ImportBtn.Visibility = Visibility.Collapsed;
                tableOne.Visibility = Visibility.Visible;
                compareBtn.Visibility = Visibility.Visible;
                noResults1.Visibility = Visibility.Visible;
                chart1.Visibility = Visibility.Visible;
                VisBy.Visibility = Visibility.Visible;
                dataChartLabel.Visibility = Visibility.Visible;

            }
        }

        public void FetchTableHeaders()
        {
            //VisualiseBy ComboBox One -> fetch from table one
            colNames1.Clear();
            visualiseInputBy1.Items.Clear();

            foreach (DataColumn column in dataTable.Columns)
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

            if (dataTable.Rows.Count > 0)
            {
                var t1 = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    double item = double.Parse(row[visualiseByInput1].ToString(), CultureInfo.InvariantCulture);
                    numberList1.Add(item);
                    t1++;

                    //draw first 1000 table => datachart
                    if (t1 >= 1000) break;
                }
                //Get First Date and Last date from search... (current datatable)
                DataRow lastRowDT1 = dataTable.Rows[dataTable.Rows.Count - 1];
                DataRow firstRowDT1 = dataTable.Rows[0];

                chartDateOne = firstRowDT1["date"].ToString();
                chartDateTwo = lastRowDT1["date"].ToString();
            }
            else
            {
                MessageBox.Show("Table one that you selected to compare is empty!");
            }

            //Clear labels if exist (Refresh axisX)
            if (isLabel1 == 1)
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

        private void CompareBtn_Click(object sender, RoutedEventArgs e)
        {
            string Compare = "";
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;

            //Compare last search since user loged in
            if (dataTable.Rows.Count > 0)
            {
                //Serialise search and save to DB
                Compare = Global.SerializeTableToString(dataTable);

                MessageBox.Show("Dt2 rows > 0, Search to XML...");
            }
            else //Compare last search when user last loged in
            {
                Compare = Global.SerializeTableToString(dataTable);

                MessageBox.Show("dt1 Serialized successfully");
            }

            //Check -> save string Compare to CompareOne or CompareTwo in db...
            if (Global.compare1)
            {
                Global.compare2 = true;
                Global.compare1 = false;
                cmd3.CommandText = "UPDATE Compare SET CompareTwo = '" + Compare + "' WHERE (UserId = '" + Global.thisUser.Id + "') AND (CompareTwo = 'None')";
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Updated!");
            }
            else
            {
                Global.compare1 = true;
                Global.compare2 = false;

                string x = "None";

                cmd3.CommandText = "INSERT INTO Compare VALUES ('" + Global.thisUser.Id + "', '" + Compare + "', '" + x + "')";
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Inserted!");

            }

            if (Global.compare1)
            {
                MessageBox.Show("Compare 1 is true");
            }

            if (Global.compare2)
            {
                MessageBox.Show("Compare 2 is true");
                //Open Compare Window
                var CompareWin = new Compare();
                CompareWin.Show();
            }

        }
    }
}
