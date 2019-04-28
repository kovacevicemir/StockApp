using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace StockWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Projects\Assignment 3\StockApp\StockApp\StockApp.mdf';Integrated Security=True");


        SqlConnectionStringBuilder connStringBuilder;
        //void ConnectToDb()
        //{
        //    connStringBuilder = new SqlConnectionStringBuilder();
        //    connStringBuilder.DataSource = "(LocalDB)\\MSSQLLocalDB";
        //    connStringBuilder.InitialCatalog = "C:\\Projects\\Assignment 3\\StockApp\\StockApp\\StockApp.mdf";
        //    connStringBuilder.Encrypt = true;
        //    connStringBuilder.TrustServerCertificate = true;
        //    connStringBuilder.ConnectTimeout = 30;
        //    connStringBuilder.AsynchronousProcessing = true;
        //    connStringBuilder.MultipleActiveResultSets = true;
        //    connStringBuilder.IntegratedSecurity = true;

        //    conn = new SqlConnection(connStringBuilder.ToString());

        //    comm = conn.CreateCommand();

        //}
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Member GetMember(Member m)
        {
            Member member = new Member();
            SqlCommand comm = conn.CreateCommand();

            try
            {
                comm.CommandText = "select * from Users where username=@username and password=@password";
                comm.Parameters.AddWithValue("username", m.username);
                comm.Parameters.AddWithValue("password", m.password);
                comm.CommandType = CommandType.Text;

                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    member.Id = Convert.ToInt32(reader[0]);
                    member.username = reader[1].ToString();
                    member.firstname = reader[2].ToString();
                    member.lastname = reader[3].ToString();
                    member.email = reader[4].ToString();
                    member.password = reader[5].ToString();
                    member.lastSearchAll = reader[6].ToString();
                    member.lastSearchHistory = reader[7].ToString();
                    member.lastSearchLive = reader[8].ToString();
                }

                return member;
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }
        }

        public DataTable FetchProducts(string query)
        {

            try
            {
                conn.Open();

                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandTimeout = 0;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = query;
                cmd2.ExecuteNonQuery();

                DataTable dt1 = new DataTable("dt1");
                SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
                da1.Fill(dt1);
                return dt1;
            }
            catch (Exception ex)
            {
                CustomLog(ex.Message.ToString());
                CustomLog(ex.StackTrace.ToString());
                CustomLog("-------------------------------------------------------------------------------------------------------");
                DataTable dt10 = new DataTable("dt10");
                return dt10;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //[OperationBehavior]
        //public DataTable FetchProducts1(SqlCommand cmd5)
        //{
        //    DataTable dt2 = new DataTable("dt2");
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd5);
        //    da1.Fill(dt2);
        //    return dt2;
        //}


        public DataTable SearchData(Nsye Search)
        {
            try
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SearchData", conn))
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


                    DataTable dt2 = new DataTable("dt2");
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    da1.Fill(dt2);

                    return dt2;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int lastSearch(string lastSearch, int userId)
        {
            try
            {
                conn.Open();

                SqlCommand cmd3 = conn.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "UPDATE Users SET lastSearchAll = '" + lastSearch + "' , lastSearchHistory= '" + lastSearch + "' WHERE Id = " + userId;
                cmd3.ExecuteNonQuery();

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public DataTable selectHeaders(string tableName, string header)
        {
            try
            {
                conn.Open();

                //Select all from units table
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT DISTINCT " + header + " FROM " + tableName;
                cmd.ExecuteNonQuery();

                //create new datatable and fill with cmd
                DataTable dt = new DataTable("dt");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int updateCompare(string Compare, int userId)
        {
            try
            {
                conn.Open();

                SqlCommand cmd3 = conn.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "UPDATE Compare SET CompareTwo = '" + Compare + "' WHERE (UserId = '" + userId + "') AND (CompareTwo = 'None')";
                cmd3.ExecuteNonQuery();

                return 1;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int insertCompare(string Compare, int userId)
        {
            try
            {
                conn.Open();

                SqlCommand cmd3 = conn.CreateCommand();
                cmd3.CommandType = CommandType.Text;

                string x = "None";

                cmd3.CommandText = "INSERT INTO Compare VALUES ('" + userId + "', '" + Compare + "', '" + x + "')";
                cmd3.ExecuteNonQuery();

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public DataTable GetMemberByUsername(string username)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from Users where username='" + username + "'";
                cmd.ExecuteNonQuery();

                //Create data table with all usernames that are = username_input
                DataTable dt = new DataTable("dt");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int insertMember(string nickname, string fname, string lname, string email, string password, string allSearch, string historySearch, string liveSearch)
        {
            try
            {
                conn.Open();

                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into Users values('"+nickname+ "','" + fname + "','" + lname + "','" + email + "','" + password + "','" + allSearch + "','" + historySearch + "','" + liveSearch + "')";
                cmd1.ExecuteNonQuery();

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //Write to custom Log.txt file method
        public static void CustomLog(string outputMessage)
        {
            string sDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApplicationDir");
            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
                using (FileStream stream = File.Create(Path.Combine(sDirectory, "Log.txt")))
                {
                    stream.Close();
                }
            }

            using (StreamWriter writer = new StreamWriter(sDirectory + "/Log.txt", append: true))
            {
                writer.WriteLine(outputMessage);
                writer.Close();
            }
        }

    }
}
