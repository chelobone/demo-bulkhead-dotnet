using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using web_app_demo.Entidades;

public class DbDemoContext : DbContext, IUserContext
{
    public DbDemoContext(DbContextOptions<DbDemoContext> options) : base(options)
    {
        
    }

    public override DatabaseFacade Database => base.Database;
    public DbSet<User> Test { get; set; }
}

public interface IUserContext : IDisposable
{
    DatabaseFacade Database { get; }
    DbSet<User> Test { get; set; }
    int SaveChanges();

}