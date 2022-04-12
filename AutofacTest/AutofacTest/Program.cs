using System;
using Autofac;

namespace AutofacTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TesteModel>();
            containerBuilder.RegisterType<TesteService>();
            containerBuilder.RegisterType<TestePresenter>();
            var container = containerBuilder.Build();

            var presenter = container.Resolve<TestePresenter>(new NamedParameter("NomeAtual", "Uhuu foi!" ));

            Console.WriteLine(presenter.RetornarNomeAtual());
        }
    }
}