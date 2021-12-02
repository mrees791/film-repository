using FilmLibrary;
using FilmLibrary.Tables;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmUI
{
    /// <summary>
    /// A console application which displays a list of all users and their favorite films.
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new Program().StartAsync();
        }

        public async Task StartAsync()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FilmDBConnectionString"].ConnectionString;
            DapperWrapper dapperWrapper = new DapperWrapper();
            FilmDatabase filmDb = new FilmDatabase(connectionString, dapperWrapper);
            IEnumerable<Tuple<User, Country, Film>> join = await filmDb.GetUserCountryFilmJoinAsync();

            Console.WriteLine("Film Repository App");
            PrintUserFilmList(join);
        }

        /// <summary>
        /// Prints each user's name, country of origin, and favorite film if they have one.
        /// </summary>
        /// <param name="userFilmList"></param>
        private void PrintUserFilmList(IEnumerable<Tuple<User, Country, Film>> userFilmList)
        {
            foreach (Tuple<User, Country, Film> item in userFilmList)
            {
                Console.Write($"Name: {item.Item1.FirstName} {item.Item1.LastName} ".PadRight(22));
                Console.Write($"| Country: {item.Item2.Name} ".PadRight(18));
                Console.Write($"| Favorite Film: ");
                if (item.Item3 != null)
                {
                    Console.WriteLine($"{item.Item3.Name}");
                }
                else
                {
                    Console.WriteLine("(None)");
                }
            }
        }
    }
}