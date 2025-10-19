using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGeneratorBlazorHybrid.Data
{
    public class AppDbContextFactory
    {
        private readonly string _connectionString;
        public bool IsDatabaseAvailable { get; set; }

        public AppDbContextFactory()
        {
            //_connectionString = $"Data Source={Preferences.Get("DatabaseFilePath", "")}";
            //_connectionString = "Data Source=c:\\temp\\app.db";
        }


        public AppDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            string connectionString = $"Data Source={Preferences.Get("DatabaseFilePath", "")}";
            optionsBuilder.UseSqlite(connectionString);

            var context = new AppDbContext(optionsBuilder.Options);

            // Ensure the database and schema exist
            context.Database.EnsureCreated();

            return context;
        }
    }
}
