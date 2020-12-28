using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatavSimulator.DatavObjects;

namespace DatavSimulator
{
    class DatavContext : DbContext
    {
        public DbSet<Datav> Datavs { get; set; }
        
        private readonly string _connectionString;
        public DatavContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
