using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.Common;

namespace WholesaleEntities
{
    public class DataBaseConnection
    {
        private DbConnection _connection;
        private static DataBaseConnection _instance;

        public static DataBaseConnection Instance
        {
            get
            {
                return _instance;
            }
        }
        static DataBaseConnection()
        {
            _instance = new DataBaseConnection();
        }

        private DataBaseConnection()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Wholesale;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _connection = new SqlConnection(connectionString);
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }
    }
}
