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
using StockApp.Models;

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

            //Fill combo box exchange
            string exchange = "exchange";
            fill_combo_box(exchangeInput, exchange);

            //Fill combo box symbol
            string symbol = "stock_symbol";
            fill_combo_box(symbolInput, symbol);
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

        //SEARCH HANDLER
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //Store search inputs into Search (Nsye model)
            Nsye Search = new Nsye();

            //Search.exchange = exchangeInput.SelectedValue.ToString();
            //Search.stock_symbol = symbolInput.SelectedValue.ToString();

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
            Search.date_from = dateFrom.SelectedDate.Value.ToString("d/MM/yyyy");
            MessageBox.Show(Search.date_from);
            Search.date_to = dateTo.SelectedDate.Value.ToString("d/MM/yyyy");
            MessageBox.Show(Search.date_to);

            //Return Search
            string q = "select * from nyse_history WHERE date BETWEEN " +"'"+Search.date_from+"'" +" AND " + "'"+Search.date_to+"'";
            string q1 = "select * from nyse_history where date = '7/03/2005'";
            //SELECT* FROM nyse_history WHERE date BETWEEN '3/03/2005' and '3/03/2006';
            DataTable dt2 = FetchProducts(q);

            //Insert data into datagrid_products
            dataGrid_nyse.ItemsSource = dt2.DefaultView;
            dataGrid_nyse.CanUserAddRows = false;

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
    }
}
