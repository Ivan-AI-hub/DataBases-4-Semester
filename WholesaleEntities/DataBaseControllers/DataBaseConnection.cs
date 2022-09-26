using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace WholesaleEntities.DataBaseControllers
{
    public class DataBaseConnection : IDisposable
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
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
