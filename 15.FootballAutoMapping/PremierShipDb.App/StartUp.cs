using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PremierShipDb.App.Contracts;
using PremierShipDb.App.Controllers;
using PremierShipDb.App.Core;
using PremierShipDb.App.Core.Dtos;
using PremierShipDb.Data;
using PremierShipDb.Services;
using PremierShipDb.Services.Contracts;

namespace PremierShipDb.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var service = ConfigureServices();

            var engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<PremierShipDbContext>(
                opt => opt.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<TeamProfile>());

            serviceCollection.AddTransient<IDbInitializerServices, DbInitializerServices>();
            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddTransient<ITeamController, TeamController>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
