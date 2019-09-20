using Microsoft.EntityFrameworkCore;
using PVueling.Domain.Entities;
using System.Reflection;


namespace PVueling.Infraestruct.RepositoryDB
{
    public class MyDbContextRateTransac : DbContext
    {
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
