using System.Data.SqlClient;
using Deer_Hub_Backend.AppConfig;

namespace Deer_Hub_Backend.DAL
{
    public static class DBHelper
    {
        private static readonly string connStr = ConfigurationManager.GetConnectionString("DEERDB");

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}
