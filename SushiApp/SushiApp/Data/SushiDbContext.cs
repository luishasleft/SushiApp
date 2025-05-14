
using CinemaManager.Models.Entities;
using SushiApp.Models.Entities;

namespace SushiApp;

using Microsoft.EntityFrameworkCore;

public class SushiDbContext : DbContext
{
    public SushiDbContext(DbContextOptions<SushiDbContext> options) : base(options) { }
    
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Piatto> Piatti { get; set; }
 
}