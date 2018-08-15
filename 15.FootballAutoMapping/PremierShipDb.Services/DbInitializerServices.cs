using System;
using Microsoft.EntityFrameworkCore;
using PremierShipDb.Data;
using PremierShipDb.Services.Contracts;

namespace PremierShipDb.Services
{
    public class DbInitializerServices: IDbInitializerServices
    {
        private readonly PremierShipDbContext context;

        public DbInitializerServices(PremierShipDbContext context)
        {
            this.context = context;
        }
        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
