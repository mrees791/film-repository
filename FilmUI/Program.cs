using FilmLibrary;
using FilmLibrary.Tables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new Program().StartAsync();
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Film Repository App");

            // Testing connection string
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FilmDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DapperWrapper dapperWrapper = new DapperWrapper();
            FilmDatabase filmDb = new FilmDatabase(connectionString, dapperWrapper);
            var countries = await filmDb.GetCountriesAsync();
            foreach (Country country in countries)
            {
                Console.WriteLine($"{country.Id} - {country.Name}");
            }
            IEnumerable<Tuple<User, Country>> joins = await filmDb.GetUserJoinOnCountry();

            foreach (Tuple<User, Country> join in joins)
            {
                Console.WriteLine($"{join.Item1.FirstName} {join.Item1.LastName} {join.Item2.Name}");
            }
        }
    }
}