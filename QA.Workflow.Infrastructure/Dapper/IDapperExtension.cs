using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.Dapper
{
    public interface IDapperExtension
    {
        Task<List<T>> ExecuteReaderAsync<T>(string sqlQuery, DynamicParameters parameterValues);
        List<T> ExecuteReader<T>(string sqlQuery, DynamicParameters parameterValues);
        Task<List<T>> ExecuteNonQueryReaderAsync<T>(string storedProcedureName, DynamicParameters parameterValues);
        List<T> ExecuteNonQueryReader<T>(string storedProcedureName, DynamicParameters parameterValues);
        Task<List<T>> ExecuteNonQueryAsync<T>(string storedProcedureName, DynamicParameters parameterValues);
        List<T> ExecuteNonQuery<T>(string storedProcedureName, DynamicParameters parameterValues);
		Task<T> ExecuteNonQueryReaderSingleRowAsync<T>(string storedProcedureName, DynamicParameters parameterValues, DbConnection connection = null, IDbTransaction dbTransaction=null);
		T ExecuteNonQueryReaderSingleRow<T>(string storedProcedureName, DynamicParameters parameterValues);
		DbConnection Connection();
	}
}
