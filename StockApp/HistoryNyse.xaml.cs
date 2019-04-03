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
using StockApp.Models;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for HistoryNyse.xaml
    /// </summary>
    public partial class HistoryNyse : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");
        string lastSearchHistory = "";

        //Create dataTabes here, so they can be used in SAVE button handler
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

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
            dt1 = FetchProducts(q);

            //Insert data into datagrid_products
            dataGrid_nyse.ItemsSource = dt1.DefaultView;
            dataGrid_nyse.CanUserAddRows = false;

            //Fill combo box exchange
            string exchange = "exchange";
            fill_combo_box(exchangeInput, exchange);

            //Fill combo box symbol
            string symbol = "stock_symbol";
            fill_combo_box(symbolInput, symbol);
        }

        //FETCH All FROM DB / PUT IT IN DataTable x
        //used only for testing, before stored procedures were implemented
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
            Nsye Search = new Nsye();

            //Exchange
            Search.exchange = exchangeInput.SelectedValue.ToString();

            //Symbol
            Search.stock_symbol = symbolInput.SelectedValue.ToString();

            //Open
            if(openFromInput.Text != "")
            {
                Search.stock_price_open_from = float.Parse(openFromInput.Text);
            }

            if (openToInput.Text != "")
            {
                Search.stock_price_open_to = float.Parse(openToInput.Text);
            }

            //Close
            if (closeInputFrom.Text != "")
            {
                Search.stock_price_close_from = float.Parse(closeInputFrom.Text);
            }

            if (closeInputTo.Text != "")
            {
                Search.stock_price_close_to = float.Parse(closeInputTo.Text);
            }

            //High
            if (highInputFrom.Text != "")
            {
                Search.stock_price_high_from = float.Parse(highInputFrom.Text);
            }

            if (highInputTo.Text != "")
            {
                Search.stock_price_high_to = float.Parse(highInputTo.Text);
            }

            //Low
            if (lowInputFrom.Text != "")
            {
                Search.stock_price_low_from = float.Parse(lowInputFrom.Text);
            }

            if (lowInputTo.Text != "")
            {
                Search.stock_price_low_to = float.Parse(lowInputTo.Text);
            }

            //Volume
            if (volumeInputFrom.Text != "")
            {
                Search.stock_volume_from = int.Parse(volumeInputFrom.Text);
            }

            if (volumeInputTo.Text != "")
            {
                Search.stock_volume_to = int.Parse(volumeInputTo.Text);
            }

            //Adj
            if (adjInputFrom.Text != "")
            {
                Search.stock_price_adj_close_from = float.Parse(adjInputFrom.Text);
            }

            if (adjInputTo.Text != "")
            {
                Search.stock_price_adj_close_to = float.Parse(adjInputTo.Text);
            }

            //Date
            if(dateFrom.SelectedDate != null)
            {
                Search.date_from = dateFrom.SelectedDate.Value.ToString("d/MM/yyyy");
            }

            if (dateTo.SelectedDate != null)
            {
                Search.date_to = dateTo.SelectedDate.Value.ToString("d/MM/yyyy");
            }



            //Return Search
            //string date_query = "SELECT * from nyse_history where convert(datetime, date, 103) between CONVERT(datetime, '"+Search.date_from+"', 103) and CONVERT(datetime,'"+Search.date_to+"')";
            //string open_query = "SELECT * FROM nyse_history WHERE CONVERT(float, stock_price_open)" +" BETWEEN "+Search.stock_price_open_from+ " AND " +Search.stock_price_open_to;
            //string mix = "SELECT * from nyse_history where (convert(datetime, date, 103) between CONVERT(datetime, '" + Search.date_from + "', 103) and CONVERT(datetime,'" + Search.date_to + "')) " +
            //    "AND (CONVERT(float, stock_price_open) BETWEEN " + Search.stock_price_open_from + " AND " + Search.stock_price_open_to+
            //    ") AND (CONVERT(float, stock_price_high) BETWEEN " + Search.stock_price_high_from + " AND " + Search.stock_price_high_to +")";



            //CALL STORED PROCEDURE WHICH RETURN SEARCH FROM DB
            using (SqlCommand cmd = new SqlCommand("SearchData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@exchange", SqlDbType.VarChar).Value = Search.exchange;
                cmd.Parameters.Add("@symbol", SqlDbType.VarChar).Value = Search.stock_symbol;
                cmd.Parameters.Add("@dateFrom", SqlDbType.VarChar).Value = Search.date_from;
                cmd.Parameters.Add("@dateTo", SqlDbType.VarChar).Value = Search.date_to;
                cmd.Parameters.Add("@priceOpenFrom", SqlDbType.VarChar).Value = Search.stock_price_open_from;
                cmd.Parameters.Add("@priceOpenTo", SqlDbType.VarChar).Value = Search.stock_price_open_to;
                cmd.Parameters.Add("@priceHighFrom", SqlDbType.VarChar).Value = Search.stock_price_high_from;
                cmd.Parameters.Add("@priceHighTo", SqlDbType.VarChar).Value = Search.stock_price_high_to;
                cmd.Parameters.Add("@priceCloseFrom", SqlDbType.VarChar).Value = Search.stock_price_close_from;
                cmd.Parameters.Add("@priceCloseTo", SqlDbType.VarChar).Value = Search.stock_price_close_to;
                cmd.Parameters.Add("@priceLowFrom", SqlDbType.VarChar).Value = Search.stock_price_low_from;
                cmd.Parameters.Add("@priceLowTo", SqlDbType.VarChar).Value = Search.stock_price_low_to;
                cmd.Parameters.Add("@adjFrom", SqlDbType.VarChar).Value = Search.stock_price_adj_close_from;
                cmd.Parameters.Add("@adjTo", SqlDbType.VarChar).Value = Search.stock_price_adj_close_to;
                cmd.Parameters.Add("@volumeFrom", SqlDbType.VarChar).Value = Search.stock_volume_from;
                cmd.Parameters.Add("@volumeTo", SqlDbType.VarChar).Value = Search.stock_volume_to;


                cmd.ExecuteNonQuery();

                dt2 = FetchProducts1(cmd);

                //Insert data into datagrid_products
                dataGrid_nyse.ItemsSource = dt2.DefaultView;
                dataGrid_nyse.CanUserAddRows = false;

                //Serialise Search and store in database
                lastSearchHistory = Global.SerializeToXml(Search);

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "UPDATE Users SET lastSearchAll = '" +lastSearchHistory+ "' , lastSearchHistory = '"+ lastSearchHistory + "' WHERE Id = " + Global.thisUser.Id;
                cmd3.ExecuteNonQuery();
            }
        }

        //COMBOBOX
        public void fill_combo_box(ComboBox comboBox, string header)
        {
            comboBox.Items.Clear();

            //Select all from units table
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT " +header+ " FROM nyse_history";
            cmd.ExecuteNonQuery();

            //create new datatable and fill with cmd
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

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
    }
}
