using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LuisBot.Dialogs
{
    class ChatbotDataBase
    {
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

        /**
         * Get the meal list of given day
         **/
        public string GetTheMealListOfGivenDay(int day)
        {
            try
            {
                //Console.WriteLine("GetLocalIPAddress: " + GetLocalIPAddress());
                //Console.WriteLine("GetPublicIPAddress: " + GetPublicIPAddress());

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "free-sql-app-dbserver.database.windows.net";
                builder.UserID = "minekucukal";
                builder.Password = "Mayn@282801";
                builder.InitialCatalog = "free-sql-db";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

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
                                Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                            }
                        }
                    }
                }
                return "GG";
            }
            catch (SqlException e)
            {
                return e.ToString();
            }
        }

        public static string GetLocalIPAddress()
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

        public static string GetPublicIPAddress()
        {
            return new WebClient().DownloadString("http://icanhazip.com");
        }
    }
}