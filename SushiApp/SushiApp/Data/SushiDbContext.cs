
using CinemaManager.Models.Entities;

namespace SushiApp;

using Microsoft.EntityFrameworkCore;

public class SushiDbContext : DbContext
{
    public SushiDbContext(DbContextOptions<SushiDbContext> options) : base(options) { }
    
    public DbSet<UserAccount> UserAccounts { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    // }
}