using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deer_Hub_Backend.DAL
{
    public static class DBHelper
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["DEERDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}
