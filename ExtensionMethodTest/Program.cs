using System;

namespace ExtensionMethodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var peopleJames = new People
                { Name = "James", Email = "james@email.com", Phone = "17 36212775", Cellphone = "17 996239878" };
            var companyJames = new Employee
            {
                Company = "SharkTank", Name = "James", Email = "james@email.com", Phone = "17 36212775",
                Cellphone = "17 996239878"
            };
            
            Console.WriteLine($"{peopleJames.GetContactInformation()}");
            Console.WriteLine($"{companyJames.ContactInformation}");
        }
    }
}