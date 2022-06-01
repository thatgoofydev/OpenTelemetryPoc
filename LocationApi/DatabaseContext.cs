using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
#pragma warning disable CS8618

namespace LocationApi;

public class DatabaseContext : DbContext
{
    public DbSet<SimpleObject> Simples { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> db) : base(db)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SimpleObject>(builder =>
        {
            builder.HasKey(e => e.Id);
        });
    }
}

public class DatabaseContextDesignTimeContext : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        return new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer("Server=.;Database=otel_test;Trusted_Connection=True;").Options);
    }
}

public class SimpleObject
{
    public int Id { get; set; }
    public string Value { get; set; }
}