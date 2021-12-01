using FilmLibrary;
using FilmLibrary.Tables;
using System;
using System.Configuration;
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
            string connectionString = ConfigurationManager.ConnectionStrings["FilmDBConnectionString"].ConnectionString;
            DapperWrapper dapperWrapper = new DapperWrapper();
            FilmDatabase filmDb = new FilmDatabase(connectionString, dapperWrapper);
            IEnumerable<Tuple<User, Country, Film>> join = await filmDb.GetUserCountryFilmJoinAsync();

            Console.WriteLine("Film Repository App");
            PrintUserFilmList(join);
        }

        private void PrintUserFilmList(IEnumerable<Tuple<User, Country, Film>> userFilmList)
        {
            foreach (Tuple<User, Country, Film> item in userFilmList)
            {
                Console.Write($"{item.Item1.FirstName} {item.Item1.LastName} {item.Item2.Name} ");
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