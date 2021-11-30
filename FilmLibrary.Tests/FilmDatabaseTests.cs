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
        [Fact]
        public async Task GetCountriesAsync_ReturnsCountriesFromQueryUsingExpectedSQLQueryAndConnectionString()
        {
            // Arrange
            var mockDapper = new Mock<IDapperWrapper>();
            var expectedConnectionString = @"Server=SERVERNAME;Database=TESTDB;Integrated Security=true;";
            var expectedQuery = "SELECT * FROM Country";
            var filmDb = new FilmDatabase(expectedConnectionString, mockDapper.Object);
            var expectedCountries = new List<Country>()
            {
                new Country() { Name = "United States" }
            };

            mockDapper
                .Setup(t => t.QueryAsync<Country>(It.Is<IDbConnection>(db => db.ConnectionString == expectedConnectionString), expectedQuery))
                .ReturnsAsync(expectedCountries);

            // Act
            IEnumerable<Country> countries = await filmDb.GetCountriesAsync();

            // Assert
            Assert.Same(expectedCountries, countries);
        }
    }
}