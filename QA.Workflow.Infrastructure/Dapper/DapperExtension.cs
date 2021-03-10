using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.Dapper
{
    public class DapperExtension : IDapperExtension
    {
        protected readonly IConnectionFactory connectionFactory;
        public DapperExtension(string connectionString)=>this.connectionFactory = new ConnectionFactory(connectionString);
        public async Task<List<T>> ExecuteReaderAsync<T>(string sqlQuery, DynamicParameters parameterValues)
        {
            List<T> result = null;
			using (var connection = connectionFactory.GetConnection())
			{
				await connection.OpenAsync();
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        var data = await connection.QueryAsync<T>(sqlQuery, parameterValues, tran);
                        result = data.ToList<T>();
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
						connection.Close();
						throw;
                    }
                }
				connection.Close();
			}
            return result;
        }
        public List<T> ExecuteReader<T>(string sqlQuery, DynamicParameters parameterValues)
        {
            List<T> result = null;
			using (var connection = connectionFactory.GetConnection())
			{
				connection.Open();
				using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        result = connection.Query<T>(sqlQuery, parameterValues, tran).ToList<T>();
						connection.Close();
						tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
						connection.Close();
						throw;
                    }
                }
            }
            return result;
        }
        public async Task<List<T>> ExecuteNonQueryReaderAsync<T>(string storedProcedureName, DynamicParameters parameterValues)
        {
            List<T> result = null;
            using (var connection = connectionFactory.GetConnection())
            {
				await connection.OpenAsync();
				try
                {
                    var data = await connection.QueryAsync<T>(storedProcedureName, parameterValues, null, 60, commandType: CommandType.StoredProcedure);
                    result = data.ToList<T>();
                }
                catch
                {
					connection.Close();
					throw;
                }
				connection.Close();
			}
            return result;
        }
		public T ExecuteNonQueryReaderSingleRow<T>(string storedProcedureName, DynamicParameters parameterValues)
		{
			T result;
			using (var connection = connectionFactory.GetConnection())
			{
				connection.Open();
				try
				{
					var data = connection.Query<T>(storedProcedureName, parameterValues, null,true, 60, commandType: CommandType.StoredProcedure);
					result = data.FirstOrDefault<T>();
				}
				catch(System.Exception exception)
				{
					connection.Close();
					throw;
				}
				connection.Close();
			}
			return result;
		}
		public async Task<T> ExecuteNonQueryReaderSingleRowAsync<T>(string storedProcedureName, DynamicParameters parameterValues,DbConnection connection=null, IDbTransaction dbTransaction = null)
		{
			T result;
            if (connection == null)
            {
                using (connection = connectionFactory.GetConnection())
                {
                    await connection.OpenAsync();
                    try
                    {
                        var data = await connection.QueryAsync<T>(storedProcedureName, parameterValues, null, 60, commandType: CommandType.StoredProcedure);
                        result = data.FirstOrDefault<T>();
                    }
                    catch
                    {
                        connection.Close();
                        throw;
                    }
                    connection.Close();
                }
            }
            else {
                var data = await connection.QueryAsync<T>(storedProcedureName, parameterValues, dbTransaction, 60, commandType: CommandType.StoredProcedure);
                result = data.FirstOrDefault<T>();
            }
			return result;
		}
		public List<T> ExecuteNonQueryReader<T>(string storedProcedureName, DynamicParameters parameterValues)
        {
            List<T> result = null;
			using (var connection = connectionFactory.GetConnection())
			{
				connection.Open();
				try
                {
                    result = connection.Query<T>(storedProcedureName, parameterValues, null, true, 60, commandType: CommandType.StoredProcedure).ToList<T>();
                }
                catch
                {
					connection.Close();
					throw;
                }
				connection.Close();
            }
            return result;
        }
        public async Task<List<T>> ExecuteNonQueryAsync<T>(string storedProcedureName, DynamicParameters parameterValues)
        {
            List<T> result = null;
			using (var connection = connectionFactory.GetConnection())
			{
				await connection.OpenAsync();
				using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        var data = await connection.QueryAsync<T>(storedProcedureName, parameterValues, tran, 60, commandType: CommandType.StoredProcedure);
                        result = data.ToList<T>();
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
						connection.Close();
						throw;
                    }
                }
				connection.Close();
			}
            return result;
        }
        public List<T> ExecuteNonQuery<T>(string storedProcedureName, DynamicParameters parameterValues)
        {
            List<T> result = null;
			using (var connection = connectionFactory.GetConnection())
			{
				connection.Open();
				using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        result = connection.Query<T>(storedProcedureName, parameterValues, tran, true, 60, commandType: CommandType.StoredProcedure).ToList<T>();
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
						connection.Close();
						throw;
                    }
                }
				connection.Close();
			}
            return result;
        }

		public DbConnection Connection()
		{
			return this.connectionFactory.GetConnection();
		}
	}
}
