using System.Data.SqlClient;

namespace Mangananggal.Connections
{
    internal class Connection
    {
        private static readonly string connectionString = "Server = localhost\\SQLExpress;Database=mangananggalDB;Trusted_Connection=True;";

        public static SqlConnection conn()
        {
            return new SqlConnection(connectionString);
        }
        public static bool TestConnection()
        {
            using (SqlConnection connection = conn())
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }

}