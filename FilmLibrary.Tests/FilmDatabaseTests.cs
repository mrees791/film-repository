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
            var expectedCountries = new List<Country>()
            {
                new Country() { Id = 1, Name = "United States" },
                new Country() { Id = 2, Name = "Canada" },
                new Country() { Id = 3, Name = "Mexico" }
            };

            mockDapper
                .Setup(t => t.QueryAsync<Country>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery))
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
            var expectedCountry = new Country() { Id = 1, Name = "United States" };

            mockDapper.Setup(t => t.QueryFirstOrDefaultAsync<Country>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString),
                expectedQuery,
                It.IsAny<object>()))
                .ReturnsAsync(expectedCountry);

            // Act
            var country = await filmDb.GetCountryByIdAsync(2);

            // Assert
            Assert.Same(expectedCountry, country);
        }
    }
}