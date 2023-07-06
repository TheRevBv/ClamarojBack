using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClamarojBack.Data;
public class UsersContext : IdentityDbContext<IdentityUser>
{
    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // It would be a good idea to move the connection string to user secrets        
        options.UseSqlServer("Server=EC0355;Database=Clamaroj;User Id=sa;Password=Uwtz501o;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}