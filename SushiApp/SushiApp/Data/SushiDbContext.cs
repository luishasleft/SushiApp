using Microsoft.EntityFrameworkCore;
using SushiApp.Models.Entities;

namespace SushiApp.Data;

public class SushiDbContext: DbContext
{
    public SushiDbContext(DbContextOptions<SushiDbContext> options) : base(options) { }
    
    public DbSet<UserAccount> UserAccounts { get; set; }
    
}