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

        public FilmDatabase(string connectionString)
        {
            ConnectionString = connectionString;
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