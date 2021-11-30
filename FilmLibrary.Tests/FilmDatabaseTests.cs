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
        public void GetCountries_ReturnsCountriesFromQueryUsingExpectedSQLQueryAndConnectionString()
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
                .Setup(t => t.Query<Country>(It.Is<IDbConnection>(db => db.ConnectionString == expectedConnectionString), expectedQuery))
                .Returns(expectedCountries);

            // Act
            var countries = filmDb.GetCountries();

            // Assert
            Assert.Same(expectedCountries, countries);
        }
    }
}