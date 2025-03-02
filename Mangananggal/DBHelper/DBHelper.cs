using Mangananggal.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangananggal.DBHelper
{
    internal class DBHelper: Connection
    {
        public static int ExecuteScalarQuery(string query)
        {
            using (var connection = conn())
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        public static SqlDataReader ExecuteReaderQuery(string query)
        {
            var connection = conn();
            var command = new SqlCommand(query, connection);
            connection.Open();
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        public static int ExecuteNonQuery(string query)
        {
            using (var connection = conn())
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
