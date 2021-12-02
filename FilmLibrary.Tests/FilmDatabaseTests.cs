using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLibrary.Tables;
using Moq;
using Xunit;

namespace FilmLibrary.Tests
{
    public class FilmDatabaseTests
    {
        public readonly string ExpectedConnectionString = @"Server=SERVERNAME;Database=TESTDB;Integrated Security=true;";

        [Fact]
        public async Task GetCountriesAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM Country";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var expectedCountries = new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Mexico" }
            };

            mockDapper
                .Setup(t => t.QueryAsync<Country>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery, null, null, null, null))
                .ReturnsAsync(expectedCountries);

            // Act
            IEnumerable<Country> countries = await filmDb.GetCountriesAsync();

            // Assert
            Assert.Same(expectedCountries, countries);
        }

        [Fact]
        public async Task GetCountryByIdAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM Country WHERE Id=@id";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var expectedCountry = new Country { Id = 1, Name = "United States" };

            mockDapper.Setup(t => t.QueryFirstOrDefaultAsync<Country>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString),
                expectedQuery,
                It.Is<object>(c => (int)c.GetType().GetProperty("id").GetValue(c) == 1), null, null, null))
                .ReturnsAsync(expectedCountry);

            // Act
            Country country = await filmDb.GetCountryByIdAsync(1);

            // Assert
            Assert.Same(expectedCountry, country);
        }

        [Fact]
        public async Task GetFilmsAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM Film";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var expectedFilms = new List<Film>
            {
                new Film { Id = 1, Name = "Sunset Avenue", ReleaseDate = new DateTime(1999, 1, 5) },
                new Film { Id = 2, Name = "A Halloween Story", ReleaseDate = new DateTime(1980, 10, 25) },
                new Film { Id = 3, Name = "Luca City", ReleaseDate = new DateTime(1997, 9, 12) }
            };

            mockDapper.Setup(t => t.QueryAsync<Film>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery, null, null, null, null))
                .ReturnsAsync(expectedFilms);

            // Act
            IEnumerable<Film> films = await filmDb.GetFilmsAsync();

            // Assert
            Assert.Same(expectedFilms, films);
        }

        [Fact]
        public async Task GetFilmByNameAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM Film WHERE Name=@name";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var expectedFilm = new Film { Id = 1, Name = "Sunset Avenue", ReleaseDate = new DateTime(1999, 5, 20) };

            mockDapper.Setup(t => t.QueryFirstOrDefaultAsync<Film>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString),
                expectedQuery,
                It.Is<object>(c => (string)c.GetType().GetProperty("name").GetValue(c) == "Sunset Avenue"), null, null, null))
                .ReturnsAsync(expectedFilm);

            // Act
            Film film = await filmDb.GetFilmByNameAsync("Sunset Avenue");

            // Assert
            Assert.Same(expectedFilm, film);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM User";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var expectedUsers = new List<User>
            {
                new User { Id = 1, CountryId = 2, FirstName = "Kevin", LastName = "Smith", FavoriteFilmId = 1 },
                new User { Id = 2, CountryId = 1, FirstName = "Sarah", LastName = "Mitchell", FavoriteFilmId = 2 },
                new User { Id = 3, CountryId = 2, FirstName = "Jacob", LastName = "Butler", FavoriteFilmId = 0 },
            };

            mockDapper.Setup(t => t.QueryAsync<User>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery, null, null, null, null))
                .ReturnsAsync(expectedUsers);

            // Act
            IEnumerable<User> users = await filmDb.GetUsersAsync();

            // Assert
            Assert.Same(expectedUsers, users);
        }

        [Fact]
        public async Task GetUserJoinOnCountryAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM [User] u INNER JOIN Country c ON u.CountryId = c.Id";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Mexico" }
            };
            var users = new List<User>
            {
                new User { Id = 1, CountryId = 1, FirstName = "Kevin", LastName = "Smith" },
                new User { Id = 2, CountryId = 2, FirstName = "Sarah", LastName = "Mitchell" },
                new User { Id = 3, CountryId = 3, FirstName = "Jacob", LastName = "Butler" },
            };
            var expectedJoin = new List<Tuple<User, Country>>
            {
                new Tuple<User, Country>(users[0], countries[0]),
                new Tuple<User, Country>(users[1], countries[1]),
                new Tuple<User, Country>(users[2], countries[2])
            };

            mockDapper.Setup(t => t.QueryAsync(
                It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery,
                It.IsAny<Func<User, Country, Tuple<User, Country>>>(), null, null, true, "Id", null, null))
                .ReturnsAsync(expectedJoin);

            // Act
            IEnumerable<Tuple<User, Country>> join = await filmDb.GetUserJoinOnCountryAsync();

            // Assert
            Assert.Same(expectedJoin, join);
        }

        [Fact]
        public async Task GetUserCountryFilmJoinAsync_ShouldWork()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedQuery = "SELECT * FROM [User] u INNER JOIN Country c ON u.CountryId = c.Id LEFT JOIN Film f ON u.FavoriteFilmId = f.Id";
            var filmDb = new FilmDatabase(ExpectedConnectionString, mockDapper.Object);
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Mexico" }
            };
            var users = new List<User>
            {
                new User { Id = 1, CountryId = 1, FirstName = "Kevin", LastName = "Smith" },
                new User { Id = 2, CountryId = 2, FirstName = "Sarah", LastName = "Mitchell", FavoriteFilmId = 1 },
                new User { Id = 3, CountryId = 3, FirstName = "Jacob", LastName = "Butler", FavoriteFilmId = 2 },
            };
            var films = new List<Film>
            {
                new Film { Id = 1, Name = "Sunset Avenue", ReleaseDate = new DateTime(1999, 1, 5) },
                new Film { Id = 2, Name = "A Halloween Story", ReleaseDate = new DateTime(1980, 10, 25) },
                new Film { Id = 3, Name = "Luca City", ReleaseDate = new DateTime(1997, 9, 12) }
            };
            var expectedJoin = new List<Tuple<User, Country, Film>>
            {
                new Tuple<User, Country, Film>(users[0], countries[0], null),
                new Tuple<User, Country, Film>(users[1], countries[1], films[0]),
                new Tuple<User, Country, Film>(users[2], countries[2], films[1])
            };

            mockDapper.Setup(t => t.QueryAsync(
                It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery,
                It.IsAny<Func<User, Country, Film, Tuple<User, Country, Film>>>(), null, null, true, "Id", null, null))
                .ReturnsAsync(expectedJoin);

            // Act
            IEnumerable<Tuple<User, Country, Film>> join = await filmDb.GetUserCountryFilmJoinAsync();

            // Assert
            Assert.Same(expectedJoin, join);
        }
    }
}