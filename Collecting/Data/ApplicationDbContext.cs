using Collecting.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Collecting.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Collection> Collections { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}