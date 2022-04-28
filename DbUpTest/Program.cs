using System;
using System.Reflection;
using DbUp;
using FirebirdSql.Data.FirebirdClient;

namespace DbUpTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (!AbrirConexaoPeloFirebird()) return;
            
            var upgradeEngine = DeployChanges.To
                .FirebirdDatabase(ConnectionString)
                .WithTransaction()
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .JournalToFirebirdTable("SchemaVersions")
                .LogToConsole()
                .Build();

            if (upgradeEngine.IsUpgradeRequired())
            {
                Console.WriteLine("Upgrade is Required.");
                var upgrader = upgradeEngine.PerformUpgrade();
                Console.WriteLine(upgrader.Successful 
                    ? "Upgrade was successful." 
                    : "Upgrade went wrong.");
                return;
            }
            
            Console.WriteLine("Upgrade is not Required.");
            Console.WriteLine("Exiting application.");
        }

        private static bool AbrirConexaoPeloFirebird()
        {
            try
            {
                Console.WriteLine($"Trying to connect to: {ConnectionString}");
                var connection = new FbConnection(ConnectionString);
                connection.Open();
                Console.WriteLine("Connection was successful in Firebird Database.");
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Connection went wrong in Firebird Database.");
                Console.WriteLine(exception.Message);
                Console.WriteLine("Exiting application.");
                return false;
            }
        }

        private static string ConnectionString =>
            "User=SYSDBA;Password=masterkey;Database=C:/Users/Lennon/Documents/Databases/DbUpTest.FDB;DataSource=127.0.0.1;Port=3050;Dialect=3;Pooling=true;";
    }
}
