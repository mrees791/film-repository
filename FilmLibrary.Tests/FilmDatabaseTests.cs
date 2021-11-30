﻿using System;
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
                It.Is<object>(c => (int)c.GetType().GetProperty("id").GetValue(c) == 1)))
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
            var expectedFilms = new List<Film>()
            {
                new Film() { Id = 1, Name = "The Evil Dead", ReleaseDate = new DateTime(1981, 10, 15) },
                new Film() { Id = 2, Name = "Evil Dead 2: Dead by Dawn", ReleaseDate = new DateTime(1987, 3, 13) },
                new Film() { Id = 3, Name = "A Nightmare on Elm Street 3: Dream Warriors", ReleaseDate = new DateTime(1987, 2, 27) }
            };

            mockDapper.Setup(t => t.QueryAsync<Film>(It.Is<IDbConnection>(db => db.ConnectionString == ExpectedConnectionString), expectedQuery))
                .ReturnsAsync(expectedFilms);

            // Act
            IEnumerable<Film> films = await filmDb.GetFilmsAsync();

            // Assert
            Assert.Same(expectedFilms, films);
        }
    }
}