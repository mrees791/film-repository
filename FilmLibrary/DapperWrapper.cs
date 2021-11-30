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
        public IEnumerable<T> Query<T>(IDbConnection connection, string sql)
        {
            return connection.Query<T>(sql);
        }

        public IEnumerable<T> Query<T>(IDbConnection connection, string sql, object param)
        {
            return connection.Query<T>(sql, param);
        }
    }
}
