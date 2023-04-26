using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=192.168.20.33,1433; Initial Catalog=TestDB; TrustServerCertificate=True; User ID=sa; Password=Passw0rd;");
        }
    }
}
