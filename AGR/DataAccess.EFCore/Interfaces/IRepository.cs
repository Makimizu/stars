using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
//using static Dapper.SqlMapper;

namespace AGR.DataAccess.EFCore.Interfaces
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>() where T : class;
        T GetById<T>(int id) where T : class;
        List<T> QueryList<T>(string query, Dictionary<string, object> param = null, bool isStoredProcedure = false);
        void ExecuteQuery(string query, Dictionary<string, object> param = null, bool isStoredProcedure = false);
        IEnumerable<T> ExecuteProcedure<T>(string spName, object param);
        List<object> ExecuteMultipleProcedure(string spName, object param, params Func<Dapper.SqlMapper.GridReader, object>[] readerFuncts);
    }
}
