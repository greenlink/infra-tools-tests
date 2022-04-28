using System;
using System.Reflection;
using FluentMigrator.Exceptions;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace FluentMigrationTest
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            using (var scope = CreateServices().CreateScope()) 
                UpdateDatabase(scope.ServiceProvider);
        }
        
        private static IServiceProvider CreateServices() =>
            new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddFirebird()
                    .WithGlobalConnectionString(ConnectionString)
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

        private static string ConnectionString =>
            "User=SYSDBA;Password=masterkey;Database=C:/Users/Lennon/Documents/Databases/FluentMigrationTest.FDB;DataSource=127.0.0.1;Port=3050;Dialect=3;Pooling=true;";
        
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
            migrationRunner.ListMigrations();
            
            if (migrationRunner.HasMigrationsToApplyUp())
            {
                Console.WriteLine("Update available.");
                Console.WriteLine(UpdateDatabase(migrationRunner) 
                    ? "Update was successful." 
                    : "Update went wrong.");
                return;
            }
            
            Console.WriteLine("There are no updates.");
        }

        private static bool UpdateDatabase(IMigrationRunner migrationRunner)
        {
            try
            {
                migrationRunner.MigrateUp();
                return true;
            }
            catch(DuplicateMigrationException duplicateMigrationException)
            {
                Console.WriteLine(duplicateMigrationException.Message);
                return false;
            }
            catch(DatabaseOperationNotSupportedException databaseOperationNotSupportedException)
            {
                Console.WriteLine(databaseOperationNotSupportedException.Message);
                return false;
            }
            catch(FluentMigratorException fluentMigratorException)
            {
                Console.WriteLine(fluentMigratorException.Message);
                return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
