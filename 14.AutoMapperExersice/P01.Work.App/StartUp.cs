using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using P01.Work.App.Contracts;
using P01.Work.App.Controllers;
using P01.Work.App.Core;
using P01.Work.App.Core.Dtos;
using P01.Work.Data;
using P01.Work.Services;
using P01.Work.Services.Contracts;

namespace P01.Work.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var service = ConfigureService();

            IEngine engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<WorkDbContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<WorkDbProfile>());

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();
            serviceCollection.AddTransient<IManagerController, ManagerController>();

            serviceCollection.AddTransient<IEmployeeConrtoller, EmployeeController>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
