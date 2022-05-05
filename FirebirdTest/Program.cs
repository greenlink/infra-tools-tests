using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace FirebirdTest
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            ListPeople();
            ShowPoolList();
        }
        
        private static void ListPeople()
        {
            var peoples = new List<People>();
            using (var fbConnection = new FbConnection(ConnectionString))
            {
                fbConnection.Open();
                using (var fbCommand = new FbCommand("Select * From People", fbConnection))
                {
                    using (var fbDataReader =  fbCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        while (fbDataReader.Read())
                        {
                            var values = new object[fbDataReader.FieldCount];
                            fbDataReader.GetValues(values);
                            Console.WriteLine(string.Join(" | ", values));
                            // peoples.Add(new People
                            // {
                            //     IdPeople = f,
                            //     Name = ,
                            //     Created = 
                            // });
                        }
                    }
                }
            }

            foreach (var people in peoples)
                Console.WriteLine($"Id: {people.IdPeople} Name: {people.Name} Created: {people.Created:dd/MM/yyyy hh:mm:ss}");
        }

        private static string ConnectionString =>
            $"User=SYSDBA;Password=masterkey;Database={Application.StartupPath}/CleanDatabase/FirebirdTest.FDB;DataSource=127.0.0.1;Port=3050;Dialect=3;Pooling=true;";
        
        private static void ShowPoolList()
        {
            
        }
    }

    internal class People
    {
        public int IdPeople { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
