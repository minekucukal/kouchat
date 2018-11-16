using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LuisBot.Dialogs
{
    class ChatbotDataBase
    {
        private string DataSource = "free-sql-app-dbserver.database.windows.net";
        private string UserID = "minekucukal";
        private string Password = "Mayn@282801";
        private string InitialCatalog = "free-sql-db";

        /**
         * Get Today Meal List 
         **/
        public string getTodayMailList()
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            String dy = datevalue.Day.ToString();
            return GetTheMealListOfGivenDay(Int32.Parse(dy));
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public string GetPublicIPAddress()
        {
            return new WebClient().DownloadString("http://icanhazip.com");
        }

        /**
         * Creata Sql Connection String Builder and return it
         * */
        public SqlConnectionStringBuilder InitializeSqlConnectionStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = this.DataSource;
            builder.UserID = this.UserID;
            builder.Password = this.Password;
            builder.InitialCatalog = this.InitialCatalog;
            return builder;
        }

        /**
         * Get the meal list of given day
         **/
        public string GetTheMealListOfGivenDay(int day)
        {
            try
            {
                //Console.WriteLine("GetLocalIPAddress: " + GetLocalIPAddress());
                //Console.WriteLine("GetPublicIPAddress: " + GetPublicIPAddress());

                SqlConnectionStringBuilder builder = InitializeSqlConnectionStringBuilder();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT* FROM dbo.may_monthly_meal_list ");
                    sb.Append("WHERE dayOfMonth = ");
                    sb.Append(day);
                    sb.Append(" ");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                                String stringValue = reader.GetString(1) + "\n" + reader.GetString(2) + "\n" + reader.GetString(3) + "\n" + reader.GetString(4);
                                return stringValue;
                            }
                        }
                    }
                }
                return "Bulunamadı";
            }
            catch (SqlException e)
            {
                return e.ToString();
            }
        }
        public string GetTheExams(int numb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.examdates ");
            sb.Append("WHERE numberid = ");
            sb.Append(numb);
            sb.Append(" ");
            String exams = GetTheStringFromDatabase(sb, 1) + ":  " + GetTheStringFromDatabase(sb, 2) + "\nSınavlarında boş şanslar! :)";

            return exams;
        }

        public string GetTheCafes(int numb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.cafeteria_informations ");
            sb.Append("WHERE numberOfCafe = ");
            sb.Append(numb);
            sb.Append(" ");
            String cafes = GetTheStringFromDatabase(sb, 1) + "\n" + GetTheStringFromDatabase(sb, 2) + "\n" + GetTheStringFromDatabase(sb, 3);
            return cafes;
        }

        public string GetQuestions(int numb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT* FROM dbo.questions ");
            sb.Append("WHERE numberOfQuestion = ");
            sb.Append(numb);
            sb.Append(" ");
            return GetTheStringFromDatabase(sb, 2);
        }
        public string GetTheLocationTitle(int numb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT* FROM dbo.location ");
            sb.Append("WHERE numberOfLocation = ");
            sb.Append(numb);
            sb.Append(" ");
            return GetTheStringFromDatabase(sb, 1);
        }

        public string GetTheLocationMapLink(int numb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT* FROM dbo.location ");
            sb.Append("WHERE numberOfLocation = ");
            sb.Append(numb);
            sb.Append(" ");
            return GetTheStringFromDatabase(sb, 2);
        }

        public string GetTheLocationImageLink(int numb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT* FROM dbo.location ");
            sb.Append("WHERE numberOfLocation = ");
            sb.Append(numb);
            sb.Append(" ");
            return GetTheStringFromDatabase(sb, 3);
        }




        public string GetTheCafeInfo(int numb , int column)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT* FROM dbo.cafeteria_informations ");
            sb.Append("WHERE numberOfCafe = ");
            sb.Append(numb);
            sb.Append(" ");
            return GetTheStringFromDatabase(sb, column);
        }


        /**
        * Get The Location Google Map Link
        **/
        public string GetTheStringFromDatabase(StringBuilder sqlCommand, int columnNumb)
        {
            try
            {
                SqlConnectionStringBuilder builder = InitializeSqlConnectionStringBuilder();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlCommand.ToString(), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                                return reader.GetString(columnNumb);
                            }
                        }
                    }
                }
                return "Bulunamadı";
            }
            catch (SqlException e)
            {
                return e.ToString();
            }
        }

    }
}