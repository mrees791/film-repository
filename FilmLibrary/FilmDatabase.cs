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

        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryAsync<Film>(connection, "SELECT * FROM Film");
            }
        }

        public async Task<Film> GetFilmByNameAsync(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryFirstOrDefaultAsync<Film>(connection, "SELECT * FROM Film WHERE Name=@name", new { name });
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryAsync<User>(connection, "SELECT * FROM User");
            }
        }

        public async Task<IEnumerable<Tuple<User, Country>>> GetUserJoinOnCountryAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM [User] u INNER JOIN Country c ON u.CountryId = c.Id";
                return await DapperWrapper.QueryAsync<User, Country, Tuple<User, Country>>(connection, sql, (user, country) => new Tuple<User, Country>(user, country));
            }
        }
    }
}