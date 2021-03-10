using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace QR.Workflow.Infrastructure.Dapper
{
    public interface IConnectionFactory
    {
        IDbConnection GetOpenConnection();
		DbConnection GetConnection();

	}
    public class ConnectionFactory : IConnectionFactory
    {
		private string _connectionString = string.Empty;
		public ConnectionFactory(string connectionString)=>_connectionString=connectionString;
		public IDbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
			if (connection.State != ConnectionState.Open)
				connection.OpenAsync();
			return connection;
        }
		public DbConnection GetConnection()
		{
			var connection = new SqlConnection(_connectionString);
			return connection;
		}
	}
}
