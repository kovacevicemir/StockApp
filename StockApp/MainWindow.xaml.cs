﻿using StockApp.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");
        bool isWindowOpen = false;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = Global.AppName + " - Home";
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

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var history = new HistoryNyse();
            history.Show();
            this.Close();
        }

        public void Display()
        {
            string xmlObject = "";
            Nsye lastSearch = new Nsye();

            //Get current user Id
            int Id = Global.thisUser.Id;
            string q = "select lastSearchAll from Users where Id = " + Id;

            //get user data
            DataTable dt1 = FetchProducts(q);

            foreach (DataRow row in dt1.Rows)
            {
                xmlObject = row["lastSearchAll"].ToString();
            }

            //Deserialize lastSearchAll (from current user) to Nyse object (lastSearch)
            XmlSerializer serializer = new XmlSerializer(typeof(Nsye));
            using (TextReader reader = new StringReader(xmlObject))
            {
                lastSearch = (Nsye)serializer.Deserialize(reader);
            }

            //CALL STORED PROCEDURE WHICH RETURN LAST SEARCH FROM CURRENT USER
            using (SqlCommand cmd = new SqlCommand("SearchData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@exchange", SqlDbType.VarChar).Value = lastSearch.exchange;
                cmd.Parameters.Add("@symbol", SqlDbType.VarChar).Value = lastSearch.stock_symbol;
                cmd.Parameters.Add("@dateFrom", SqlDbType.VarChar).Value = lastSearch.date_from;
                cmd.Parameters.Add("@dateTo", SqlDbType.VarChar).Value = lastSearch.date_to;
                cmd.Parameters.Add("@priceOpenFrom", SqlDbType.VarChar).Value = lastSearch.stock_price_open_from;
                cmd.Parameters.Add("@priceOpenTo", SqlDbType.VarChar).Value = lastSearch.stock_price_open_to;
                cmd.Parameters.Add("@priceHighFrom", SqlDbType.VarChar).Value = lastSearch.stock_price_high_from;
                cmd.Parameters.Add("@priceHighTo", SqlDbType.VarChar).Value = lastSearch.stock_price_high_to;
                cmd.Parameters.Add("@priceCloseFrom", SqlDbType.VarChar).Value = lastSearch.stock_price_close_from;
                cmd.Parameters.Add("@priceCloseTo", SqlDbType.VarChar).Value = lastSearch.stock_price_close_to;
                cmd.Parameters.Add("@priceLowFrom", SqlDbType.VarChar).Value = lastSearch.stock_price_low_from;
                cmd.Parameters.Add("@priceLowTo", SqlDbType.VarChar).Value = lastSearch.stock_price_low_to;
                cmd.Parameters.Add("@adjFrom", SqlDbType.VarChar).Value = lastSearch.stock_price_adj_close_from;
                cmd.Parameters.Add("@adjTo", SqlDbType.VarChar).Value = lastSearch.stock_price_adj_close_to;
                cmd.Parameters.Add("@volumeFrom", SqlDbType.VarChar).Value = lastSearch.stock_volume_from;
                cmd.Parameters.Add("@volumeTo", SqlDbType.VarChar).Value = lastSearch.stock_volume_to;


                cmd.ExecuteNonQuery();

                //Get last search data
                DataTable dt2 = FetchProducts1(cmd);

                //Insert data into datagrid_products
                dataTable.ItemsSource = dt2.DefaultView;
                dataTable.CanUserAddRows = false;
            }
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

        public DataTable FetchProducts1(SqlCommand cmd5)
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd5);
            da1.Fill(dt2);
            return dt2;
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Prevent multiple import windows to be opened
            foreach (Window w in Application.Current.Windows)
            {
                if (w is Import)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            //If import window is not open
            if (!isWindowOpen)
            {
                Import newwindow = new Import();
                newwindow.Show();
            }
        }
    }
}


