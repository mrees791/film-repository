using Dapper;
using FilmLibrary.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary
{
    public class FilmDatabase
    {
        private readonly string ConnectionString;
        private readonly IDapperWrapper DapperWrapper;

        public FilmDatabase(string connectionString, IDapperWrapper dapperWrapper)
        {
            ConnectionString = connectionString;
            DapperWrapper = dapperWrapper;
        }

        public IEnumerable<Country> GetCountries()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<Country>("SELECT * FROM Country;");
            }
        }
    }
}