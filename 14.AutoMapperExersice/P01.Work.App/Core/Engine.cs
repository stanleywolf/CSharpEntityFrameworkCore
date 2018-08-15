using System;
using System.Collections.Generic;
using System.Text;
using P01.Work.App.Contracts;
using P01.Work.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace P01.Work.App.Core
{
    public class Engine:IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            var initializeDb = this.serviceProvider.GetService<IDbInitializerService>();

            initializeDb.InitializeDatabase();

            var commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();

            
            while (true)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var result = commandInterpreter.Read(input);
                Console.WriteLine(result);
            }
        }
    }
}
