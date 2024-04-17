using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using web_app_demo.Entidades;

public class DbDemoOracleContext : DbContext, IUserOracleContext, IPaymentContext
{
    public DbDemoOracleContext(DbContextOptions<DbDemoOracleContext> options) : base(options)
    {
        
    }

    public override DatabaseFacade Database => base.Database;
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
}

public interface IUserOracleContext : IDisposable
{
    DatabaseFacade Database { get; }
    DbSet<User> Users { get; set; }
    int SaveChanges();

}

public interface IPaymentContext : IDisposable
{
    DatabaseFacade Database { get; }
    DbSet<Payment> Payments { get; set; }
    int SaveChanges();

}