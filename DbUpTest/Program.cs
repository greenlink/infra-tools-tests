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
            Console.WriteLine(ConnectionString);
            
            var upgradeEngine = DeployChanges.To
                .FirebirdDatabase(ConnectionString)
                .WithTransaction()
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();
            
            if(!upgradeEngine.TryConnect(out var errorMessage))
            {
                Console.WriteLine(errorMessage);
                AbrirConexaoPeloFirebird();
                Console.WriteLine("Exiting application.");
                return;
            }

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

        private static void AbrirConexaoPeloFirebird()
        {
            try
            {
                var connection = new FbConnection(ConnectionString);
                connection.Open();
                Console.WriteLine("Connected by Firebird.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string ConnectionString =>
            "User=SYSDBA;Password=masterkey;Database=C:/Users/Lennon/Documents/Databases/DbUpTest.FDB;DataSource=127.0.0.1;Port=3050;Dialect=3;Pooling=true;";
    }
}
