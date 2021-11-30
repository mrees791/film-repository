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

        public async Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object param)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }
}
