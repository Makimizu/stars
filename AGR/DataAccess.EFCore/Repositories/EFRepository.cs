using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using AGR.DataAccess.EFCore.Interfaces;
using Dapper;
//using static Dapper.SqlMapper;

namespace AGR.DataAccess.EFCore.Repositories
{
    public partial class EFRepository : IRepository
    {
        protected readonly IDbConnection context;
        public EFRepository(string connectionString)
        {
            context = new SqlConnection(connectionString);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public List<T> QueryList<T>(string query, Dictionary<string, object> param = null, bool isStoredProcedure = false)
        {
            if (isStoredProcedure)
            {
                return (context.Query<T>(query, param, commandTimeout: 120, commandType: CommandType.StoredProcedure)).ToList();
            }
            return (context.Query<T>(query, param, commandTimeout: 120)).ToList();
        }

        public void ExecuteQuery(string query, Dictionary<string, object> param = null, bool isStoredProcedure = false)
        {
            if (isStoredProcedure)
            {
                context.Execute(query, param, commandTimeout: 120, commandType: CommandType.StoredProcedure);
            }
            context.Execute(query, param, commandTimeout: 120);
        }

        public IEnumerable<T> ExecuteProcedure<T>(string spName, object param)
        {
            return context.Query<T>(spName, param, commandTimeout: 120, commandType: CommandType.StoredProcedure);
        }
        public List<object> ExecuteMultipleProcedure(string spName, object param, params Func<Dapper.SqlMapper.GridReader, object>[] readerFuncts)
        {
            var result = new List<object>();

            var gridReader = context.QueryMultiple(spName, param, commandTimeout: 120, commandType: CommandType.StoredProcedure);

            foreach (var reader in readerFuncts)
            {
                var obj = reader(gridReader);
                result.Add(obj);
            }

            return result;
        }
    }
}