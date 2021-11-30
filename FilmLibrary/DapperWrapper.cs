using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FilmLibrary
{
    public class DapperWrapper : IDapperWrapper
    {
        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql)
        {
            return await connection.QueryAsync<T>(sql);
        }

        /*public IEnumerable<T> Query<T>(IDbConnection connection, string sql, object param)
        {
            return connection.Query<T>(sql, param);
        }*/
    }
}
