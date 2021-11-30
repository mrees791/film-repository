using FilmLibrary;
using FilmLibrary.Tables;
using System;

namespace FilmUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }

        public void Start()
        {
            Console.WriteLine("Film Repository App");

            // Testing connection string
            /*string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FilmDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DapperWrapper dapperWrapper = new DapperWrapper();
            FilmDatabase filmDb = new FilmDatabase(connectionString, dapperWrapper);
            var countries = filmDb.GetCountries();
            foreach (Country country in countries)
            {
                Console.WriteLine($"{country.Id} - {country.Name}");
            }*/
        }
    }
}