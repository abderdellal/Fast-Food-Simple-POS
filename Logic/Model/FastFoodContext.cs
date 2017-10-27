using System.Data.Entity;
using Logic.Migrations;

namespace Logic.Model
{
    public class FastFoodContext : DbContext
    {
        public FastFoodContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FastFoodContext, Configuration>());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<FastFoodContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<Item> Items { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }
    }
}