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

            IEnumerable<Tuple<User, Country, Film>> join = await filmDb.GetUserCountryFilmJoinAsync();

            foreach (Tuple<User, Country, Film> joinItem in join)
            {
                Console.Write($"{joinItem.Item1.FirstName} {joinItem.Item1.LastName} {joinItem.Item2.Name} ");
                if (joinItem.Item3 != null)
                {
                    Console.WriteLine($"{joinItem.Item3.Name}");
                }
                else
                {
                    Console.WriteLine("(None)");
                }
            }
        }
    }
}