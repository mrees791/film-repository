using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FilmLibrary
{
    public interface IDapperWrapper
    {
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql);
        Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection connection, string sql, object param);
    }
}