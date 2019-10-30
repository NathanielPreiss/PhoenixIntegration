using Microsoft.EntityFrameworkCore;

namespace Leviathan
{
    public class LeviathanSyncDbContext : DbContext
    {
        private readonly string _dbName;

        public LeviathanSyncDbContext()
        {
            _dbName = "LeviathanSync";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_dbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeMapConfiguration());
            modelBuilder.ApplyConfiguration(new LeviathanEmployeeConfiguration());
        }

        public DbSet<EmployeeMap> EmployeeMaps { get; set; }
        public DbSet<LeviathanEmployee> LeviathanEmployees { get; set; }
    }
}
