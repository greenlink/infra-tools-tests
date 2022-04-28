using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMigrationTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        
        private static IServiceProvider CreateServices() =>
            new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddFirebird()
                    // Set the connection string
                    .WithGlobalConnectionString(ConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);

        private static string ConnectionString =>
            "User=SYSDBA;Password=masterkey;Database=C:/Users/Lennon/Documents/Databases/FluentMigrationTest.FDB;DataSource=127.0.0.1;Port=3050;Dialect=3;Pooling=true;";
        
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
            
            migrationRunner.ListMigrations();

            Console.WriteLine(migrationRunner.HasMigrationsToApplyUp()
                ? "Update for the database available."
                : "There are no updates for the database.");
            // Execute the migrations
            //migrationRunner.MigrateUp();
        }
    }
}
