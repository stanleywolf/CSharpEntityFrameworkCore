using System;
using Microsoft.EntityFrameworkCore;
using P01.Work.Data;
using P01.Work.Services.Contracts;

namespace P01.Work.Services
{
    public class DbInitializerService: IDbInitializerService
    {

        private readonly WorkDbContext context;

        public DbInitializerService(WorkDbContext context)
        {
            this.context = context;
        }
        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
