using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using StockApp.Models;
using StockApp.ServiceReference1;
using Nsye = StockApp.ServiceReference1.Nsye;
using System.Runtime.Serialization;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for HistoryNyse.xaml
    /// </summary>
    public partial class HistoryNyse : Window
    {
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");
        string lastSearchHistory = "";

        Service1Client service = new Service1Client();

        //Create dataTabes here, so they can be used in SAVE button handler
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

        //Used for clearing chart labels, prevent doubling up, if its 1 -> clear, 0 -> add new
        //Using this because AxisX.Clear(); is not working properly...
        int isLabel = 0;

        //DataTable column Names (used for visualise by: combobox)
        List<string> colNames = new List<string>();

        //Search values
        Nsye Search = new Nsye();

        public HistoryNyse()
        {
            InitializeComponent();

            this.Title = Global.AppName + " - History";

            //check for connection state
            //if (con.State == System.Data.ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();

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
            string q = "SELECT TOP 1000 * FROM nyse_history ORDER BY CONVERT(datetime, date, 103) ASC";
            //dt1 = FetchProducts(q);
            
            dt1 = service.FetchProducts(q);

            //Insert data into datagrid_products
            dataGrid_nyse.ItemsSource = dt1.DefaultView;
            dataGrid_nyse.CanUserAddRows = false;

            //Fill combo box exchange
            string exchange = "exchange";
            fill_combo_box(exchangeInput, exchange);

            //Fill combo box symbol
            string symbol = "stock_symbol";
            fill_combo_box(symbolInput, symbol);

            //Add Items to colName if dt1 (last search exists)
            if(dt1.Rows.Count > 0)
            {
                colNames.Clear();
                visualiseInputBy.Items.Clear();

                foreach (DataColumn column in dt1.Columns)
                {
                    colNames.Add(column.ColumnName.ToString());
                }

                foreach (string item in colNames)
                {
                    visualiseInputBy.Items.Add(item);
                }

                visualiseInputBy.Items.Remove("date");
                visualiseInputBy.Items.Remove("stock_symbol");
                visualiseInputBy.Items.Remove("exchange");

                noResults.Text = "Number of results: " + dt1.Rows.Count.ToString();
            }
        }

        //FETCH All FROM DB / PUT IT IN DataTable x
        //used only for testing, before stored procedures were implemented
        //public DataTable FetchProducts(string query)
        //{
        //    SqlCommand cmd2 = con.CreateCommand();
        //    cmd2.CommandType = CommandType.Text;
        //    cmd2.CommandText = query;
        //    cmd2.ExecuteNonQuery();

        //    DataTable dt1 = new DataTable();
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
        //    da1.Fill(dt1);
        //    return dt1;
        //}

        //Stored procedure fetch method
        public DataTable FetchProducts1(SqlCommand cmd5)
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd5);
            da1.Fill(dt2);
            return dt2;
        }

        //SEARCH HANDLER
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //Store search inputs into Search (Nsye model)
            Search = new ServiceReference1.Nsye();

            //GET ALL INPUTS, AND SET DEFAULT VALUES IF THERE IS NO INPUT

            //Exchange
            Search.exchange = exchangeInput.SelectedValue.ToString();

            //Symbol
            Search.stock_symbol = symbolInput.SelectedValue.ToString();

            //Open
            if(openFromInput.Text != "")
            {
                Search.stock_price_open_from = float.Parse(openFromInput.Text);
            }
            else
            {
                Search.stock_price_open_from = 1;
            }

            if (openToInput.Text != "")
            {
                Search.stock_price_open_to = float.Parse(openToInput.Text);
            }
            else
            {
                Search.stock_price_open_to = 20;
            }

            //Close
            if (closeInputFrom.Text != "")
            {
                Search.stock_price_close_from = float.Parse(closeInputFrom.Text);
            }
            else
            {
                Search.stock_price_close_from = 1;
            }

            if (closeInputTo.Text != "")
            {
                Search.stock_price_close_to = float.Parse(closeInputTo.Text);
            }
            else
            {
                Search.stock_price_close_to = 20;
            }

            //High
            if (highInputFrom.Text != "")
            {
                Search.stock_price_high_from = float.Parse(highInputFrom.Text);
            }
            else
            {
                Search.stock_price_high_from = 1;
            }

            if (highInputTo.Text != "")
            {
                Search.stock_price_high_to = float.Parse(highInputTo.Text);
            }
            else
            {
                Search.stock_price_high_to = 20;
            }

            //Low
            if (lowInputFrom.Text != "")
            {
                Search.stock_price_low_from = float.Parse(lowInputFrom.Text);
            }
            else
            {
                Search.stock_price_low_from = 1;
            }

            if (lowInputTo.Text != "")
            {
                Search.stock_price_low_to = float.Parse(lowInputTo.Text);
            }
            else
            {
                Search.stock_price_low_to = 20;
            }

            //Volume
            if (volumeInputFrom.Text != "")
            {
                Search.stock_volume_from = int.Parse(volumeInputFrom.Text);
            }
            else
            {
                Search.stock_volume_from = 1;
            }

            if (volumeInputTo.Text != "")
            {
                Search.stock_volume_to = int.Parse(volumeInputTo.Text);
            }
            else
            {
                Search.stock_volume_to = 9999999;
            }

            //Adj
            if (adjInputFrom.Text != "")
            {
                Search.stock_price_adj_close_from = float.Parse(adjInputFrom.Text);
            }
            else
            {
                Search.stock_price_adj_close_from = 1;
            }

            if (adjInputTo.Text != "")
            {
                Search.stock_price_adj_close_to = float.Parse(adjInputTo.Text);
            }
            else
            {
                Search.stock_price_adj_close_to = 20;
            }

            //Date
            if(dateFrom.SelectedDate != null)
            {
                Search.date_from = dateFrom.SelectedDate.Value.ToString("d/MM/yyyy");
            }
            else
            {
                Search.date_from = "10/10/1999";
            }

            if (dateTo.SelectedDate != null)
            {
                Search.date_to = dateTo.SelectedDate.Value.ToString("d/MM/yyyy");
            }
            else
            {
                Search.date_to = "10/10/2020";
            }



            //Return Search
            //string date_query = "SELECT * from nyse_history where convert(datetime, date, 103) between CONVERT(datetime, '"+Search.date_from+"', 103) and CONVERT(datetime,'"+Search.date_to+"')";
            //string open_query = "SELECT * FROM nyse_history WHERE CONVERT(float, stock_price_open)" +" BETWEEN "+Search.stock_price_open_from+ " AND " +Search.stock_price_open_to;
            //string mix = "SELECT * from nyse_history where (convert(datetime, date, 103) between CONVERT(datetime, '" + Search.date_from + "', 103) and CONVERT(datetime,'" + Search.date_to + "')) " +
            //    "AND (CONVERT(float, stock_price_open) BETWEEN " + Search.stock_price_open_from + " AND " + Search.stock_price_open_to+
            //    ") AND (CONVERT(float, stock_price_high) BETWEEN " + Search.stock_price_high_from + " AND " + Search.stock_price_high_to +")";



            //CALL STORED PROCEDURE WHICH RETURN SEARCH FROM DB
            //using (SqlCommand cmd = new SqlCommand("SearchData", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.Add("@exchange", SqlDbType.VarChar).Value = Search.exchange;
            //    cmd.Parameters.Add("@symbol", SqlDbType.VarChar).Value = Search.stock_symbol;
            //    cmd.Parameters.Add("@dateFrom", SqlDbType.VarChar).Value = Search.date_from;
            //    cmd.Parameters.Add("@dateTo", SqlDbType.VarChar).Value = Search.date_to;
            //    cmd.Parameters.Add("@priceOpenFrom", SqlDbType.VarChar).Value = Search.stock_price_open_from;
            //    cmd.Parameters.Add("@priceOpenTo", SqlDbType.VarChar).Value = Search.stock_price_open_to;
            //    cmd.Parameters.Add("@priceHighFrom", SqlDbType.VarChar).Value = Search.stock_price_high_from;
            //    cmd.Parameters.Add("@priceHighTo", SqlDbType.VarChar).Value = Search.stock_price_high_to;
            //    cmd.Parameters.Add("@priceCloseFrom", SqlDbType.VarChar).Value = Search.stock_price_close_from;
            //    cmd.Parameters.Add("@priceCloseTo", SqlDbType.VarChar).Value = Search.stock_price_close_to;
            //    cmd.Parameters.Add("@priceLowFrom", SqlDbType.VarChar).Value = Search.stock_price_low_from;
            //    cmd.Parameters.Add("@priceLowTo", SqlDbType.VarChar).Value = Search.stock_price_low_to;
            //    cmd.Parameters.Add("@adjFrom", SqlDbType.VarChar).Value = Search.stock_price_adj_close_from;
            //    cmd.Parameters.Add("@adjTo", SqlDbType.VarChar).Value = Search.stock_price_adj_close_to;
            //    cmd.Parameters.Add("@volumeFrom", SqlDbType.VarChar).Value = Search.stock_volume_from;
            //    cmd.Parameters.Add("@volumeTo", SqlDbType.VarChar).Value = Search.stock_volume_to;


            //    cmd.ExecuteNonQuery();

                //Call WCF -> to db stored procedure for search history data
                dt2 = service.SearchData(Search);

                //Insert data into datagrid_products
                dataGrid_nyse.ItemsSource = dt2.DefaultView;
                dataGrid_nyse.CanUserAddRows = false;

                //Serialise Search and store in database
                lastSearchHistory = Global.SerializeToXml(Search);

            //SqlCommand cmd3 = con.CreateCommand();
            //cmd3.CommandType = CommandType.Text;
            //cmd3.CommandText = "UPDATE Users SET lastSearchAll = '" +lastSearchHistory+ "' , lastSearchAll= '"+ lastSearchHistory + "' WHERE Id = " + Global.thisUser.Id;
            //cmd3.ExecuteNonQuery();

                int SearchToDb = service.lastSearch(lastSearchHistory, Global.thisUser.Id);
                if(SearchToDb != 1)
                {
                    MessageBox.Show("Last Search is not saved to history search, something went wrong!");
                }

                //update number of results
                noResults.Text = "Number of results: " + dt2.Rows.Count.ToString();

                //Fill Visualise by: combo box:
                foreach (DataColumn column in dt2.Columns)
                {
                    colNames.Add(column.ColumnName.ToString());
                }

                //add items from colList
                colNames.Clear();
                visualiseInputBy.Items.Clear();

                foreach (DataColumn column in dt2.Columns)
                {
                    colNames.Add(column.ColumnName.ToString());
                }

                foreach (string item in colNames)
                {
                    visualiseInputBy.Items.Add(item);
                }

                visualiseInputBy.Items.Remove("date");
                visualiseInputBy.Items.Remove("stock_symbol");
                visualiseInputBy.Items.Remove("exchange");

            //}
        }

        //COMBOBOX
        public void fill_combo_box(ComboBox comboBox, string header)
        {
            comboBox.Items.Clear();

            //Select all from units table
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT DISTINCT " +header+ " FROM nyse_history";
            //cmd.ExecuteNonQuery();

            ////create new datatable and fill with cmd
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);

            DataTable dt = new DataTable();
            dt = service.selectHeaders("nyse_history", header);

            //iterate dt and add each row ["unit"] to combo box item
            foreach (DataRow row in dt.Rows)
            {
                comboBox.Items.Add(row[header].ToString());
            }
        }

        //SAVE BUTTON HANDLER
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Save user search from last log in
            if(dt2.Rows.Count == 0)
            {
                Global.SaveDataTableToXml(dt1);
            }
            else
            {
                //Else Save user search current
                Global.SaveDataTableToXml(dt2);
            }
        }

        private void Chart1btn_Click(object sender, RoutedEventArgs e)
        {
            //Check if Visualise by: is selected
            if(visualiseInputBy.SelectedItem == null)
            {
                MessageBox.Show("Please select Visualise By: ");
                return;
            }

            string chartDateOne = "";
            string chartDateTwo = "";

            //List of chart values
            List<double> numberList = new List<double>();

            //Get visualise by: combobox input
            string visualiseByInput = visualiseInputBy.SelectedValue.ToString();

            //Add values to list ( DataChart will be drawed from this list)
            if(dt2.Rows.Count < 1)
            {
                if(dt1.Rows.Count > 0)
                {
                    var i = 0;
                    foreach (DataRow row in dt1.Rows)
                    {
                        double item = double.Parse(row[visualiseByInput].ToString(), CultureInfo.InvariantCulture);
                        numberList.Add(item);
                        i++;

                        //draw first 1000 table => datachart
                        if (i >= 1000) break;
                    }
                    //Get First Date and Last date from search... (current datatable)
                    DataRow lastRowDT1 = dt1.Rows[dt1.Rows.Count - 1];
                    DataRow firstRowDT1 = dt1.Rows[0];

                    chartDateOne = firstRowDT1["date"].ToString();
                    chartDateTwo = lastRowDT1["date"].ToString();
                }
                else
                {
                    MessageBox.Show("There is no data to be visualised, please make new search.");
                }
            }
            else //If last search (dt2 exists)
            {
                var i = 0;
                foreach (DataRow row in dt2.Rows)
                {
                    double item = double.Parse(row[visualiseByInput].ToString(), CultureInfo.InvariantCulture);
                    numberList.Add(item);
                    i++;

                    //draw first 1000 table => datachart
                    if (i >= 1000) break;
                }
                //Get First Date and Last date from search... (current datatable)
                DataRow lastRowDT2 = dt2.Rows[dt2.Rows.Count - 1];
                DataRow firstRowDT2 = dt2.Rows[0];

                chartDateOne = firstRowDT2["date"].ToString();
                chartDateTwo = lastRowDT2["date"].ToString();
            }
            
            //Clear labels if exist (Refresh axisX)
            if(isLabel == 1)
            {
                chart1.AxisX.RemoveAt(1);
            }

            chart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = numberList.AsChartValues(),
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
            isLabel = 1;

           
        }

        private void CompareBtn_Click(object sender, RoutedEventArgs e)
        {
            string Compare = "";
            
            //Compare last search since user loged in
            if (dt2.Rows.Count > 0)
            {
                //Serialise search and save to DB
                Compare = Global.SerializeTableToString(dt2);

                MessageBox.Show("Dt2 rows > 0, Search to XML...");
            }
            else //Compare last search when user last loged in
            {
                Compare = Global.SerializeTableToString(dt1);

                MessageBox.Show("dt1 Serialized successfully");
            }

            //Check -> save string Compare to CompareOne or CompareTwo in db...
            if (Global.compare1)
            {
                Global.compare2 = true;
                Global.compare1 = false;

                int updateCompare = service.updateCompare(Compare, Global.thisUser.Id);
                if(updateCompare != 1)
                {
                    MessageBox.Show("Compare 2, not saved something went wrong!");
                }

                MessageBox.Show("Updated!");
            }
            else
            {
                Global.compare1 = true;
                Global.compare2 = false;

                int insertCompare = service.insertCompare(Compare, Global.thisUser.Id);
                if(insertCompare !=1)
                {
                    MessageBox.Show("Compare table one is not saved, something went wrong!");
                }

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
