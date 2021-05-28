
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PeopleCsv.data
{
    public class CSVDbContext : DbContext
    {
        private string _connectionString;

        public CSVDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Person> People { get; set; }
    


    }
}