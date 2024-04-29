using Labb3API.Models;
using Microsoft.EntityFrameworkCore;
namespace Labb3API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<HumanInterest> HumanInterests { get; set; }

    }

}

