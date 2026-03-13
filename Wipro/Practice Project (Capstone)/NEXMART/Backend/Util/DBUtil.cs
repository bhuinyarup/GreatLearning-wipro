using Microsoft.Data.SqlClient;

namespace EcommerceApp.Util
{
    public static class DBPropertyUtil
    {
        public static string GetPropertyString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
    }

    public static class DBConnUtil
    {
        private static SqlConnection? _connection;

        public static SqlConnection GetConnection(string connectionString)
        {
            if (_connection == null || _connection.State == System.Data.ConnectionState.Closed)
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }
            return _connection;
        }
    }
}
