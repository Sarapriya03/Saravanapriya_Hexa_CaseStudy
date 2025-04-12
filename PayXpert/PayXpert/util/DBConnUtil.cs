using Microsoft.Data.SqlClient;

namespace PayXpert.util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
