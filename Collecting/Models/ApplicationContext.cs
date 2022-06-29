using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Collecting.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Collection> Collections { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}