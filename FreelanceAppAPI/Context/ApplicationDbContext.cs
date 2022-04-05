using FreelanceAppAPI.Entities;
using FreelanceAppAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreelanceAppAPI.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IConfiguration Configuration { get;  }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionSQLServer"));
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnectionSQlite"));
        }

        public DbSet<Customer> Customes {get; set;}
        public DbSet<DocumentHeader> DocumentHeaders {get; set;}
        public DbSet<DocumentDetail> DocumentDetails {get; set;}         
    }
}

