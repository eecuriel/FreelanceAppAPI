using FreelanceAppAPI.Entities;
using FreelanceAppAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreelanceAppAPI.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }

        public DbSet<Customer> Customes {get; set;}
        public DbSet<DocumentHeader> DocumentHeaders {get; set;}
        public DbSet<DocumentDetail> DocumentDetails {get; set;}         
    }
}

