using FilmLibrary;
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
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FilmDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            FilmDatabase filmDb = new FilmDatabase(connectionString);
            var countries = filmDb.GetCountries();
        }
    }
}