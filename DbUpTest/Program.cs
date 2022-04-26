using System;
using DbUp;

namespace DbUpTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ConnectionString);
            
        }

        private static string ConnectionString =>
            "User=SYSDBA;Password=masterkey;Database=C:\\Users\\Lennon\\Documents\\Databases\\DbUpTest.FDB;DataSource=127.0.0.1;Port=3050;Dialect=3;Pooling=true;";
    }
}