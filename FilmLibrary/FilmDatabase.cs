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

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryAsync<Country>(connection, "SELECT * FROM Country");
            }
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryFirstOrDefaultAsync<Country>(connection, "SELECT * FROM Country WHERE Id=@id", new { id });
            }
        }
    }
}