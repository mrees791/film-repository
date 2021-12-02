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
    /// <summary>
    /// Provides data access methods to the film database.
    /// </summary>
    public class FilmDatabase
    {
        private readonly string ConnectionString;
        private readonly IDapperWrapper DapperWrapper;

        public FilmDatabase(string connectionString, IDapperWrapper dapperWrapper)
        {
            ConnectionString = connectionString;
            DapperWrapper = dapperWrapper;
        }

        /// <summary>
        /// Returns a list of all countries.
        /// </summary>
        /// <returns>A list of all countries.</returns>
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryAsync<Country>(connection, "SELECT * FROM Country");
            }
        }

        /// <summary>
        /// Returns a country by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The country with the matching ID.</returns>
        public async Task<Country> GetCountryByIdAsync(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryFirstOrDefaultAsync<Country>(connection, "SELECT * FROM Country WHERE Id=@id", new { id });
            }
        }

        /// <summary>
        /// Returns a list of all films.
        /// </summary>
        /// <returns>A list of all films.</returns>
        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryAsync<Film>(connection, "SELECT * FROM Film");
            }
        }

        /// <summary>
        /// Returns a film record by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The film record with the matching name.</returns>
        public async Task<Film> GetFilmByNameAsync(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryFirstOrDefaultAsync<Film>(connection, "SELECT * FROM Film WHERE Name=@name", new { name });
            }
        }

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return await DapperWrapper.QueryAsync<User>(connection, "SELECT * FROM User");
            }
        }

        /// <summary>
        /// Returns a list of tuples matching each user with their country.
        /// </summary>
        /// <returns>A list of tuples with each user and their country.</returns>
        public async Task<IEnumerable<Tuple<User, Country>>> GetUserJoinOnCountryAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM [User] u INNER JOIN Country c ON u.CountryId = c.Id";
                return await DapperWrapper.QueryAsync<User, Country, Tuple<User, Country>>(connection, sql, (user, country) => new Tuple<User, Country>(user, country));
            }
        }

        /// <summary>
        /// Returns a list of tuples matching each user with their country and favorite film.
        /// If they do not have a favorite film, then the tuple's film will be null.
        /// </summary>
        /// <returns>A list of tuples matching each user with their country and favorite film.</returns>
        public async Task<IEnumerable<Tuple<User, Country, Film>>> GetUserCountryFilmJoinAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM [User] u INNER JOIN Country c ON u.CountryId = c.Id LEFT JOIN Film f ON u.FavoriteFilmId = f.Id";
                return await DapperWrapper.QueryAsync<User, Country, Film, Tuple<User, Country, Film>>(connection, sql, (user, country, film) => new Tuple<User, Country, Film>(user, country, film));
            }
        }
    }
}