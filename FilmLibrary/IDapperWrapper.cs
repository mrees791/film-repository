using System.Collections.Generic;
using System.Data;

namespace FilmLibrary
{
    public interface IDapperWrapper
    {
        IEnumerable<T> Query<T>(IDbConnection connection, string sql);
    }
}