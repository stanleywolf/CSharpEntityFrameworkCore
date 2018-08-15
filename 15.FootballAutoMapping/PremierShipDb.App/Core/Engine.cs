using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PremierShipDb.App.Contracts;
using PremierShipDb.Services;
using PremierShipDb.Services.Contracts;

namespace PremierShipDb.App.Core
{
    public class Engine:IEngine
    {
        private readonly IServiceProvider provider;
        public Engine(IServiceProvider provider)
        {
            this.provider = provider;
        }
        public void Run()
        {
            var initializeDb = this.provider.GetService<IDbInitializerServices>();

            initializeDb.InitializeDatabase();

            var commandInterpreter = provider.GetService<ICommandInterpreter>();

            while (true)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var result = commandInterpreter.Read(input);

                Console.WriteLine(result);
            }
        }
    }
}
