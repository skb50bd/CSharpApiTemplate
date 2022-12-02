using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Template.Data;

// ReSharper disable once UnusedType.Global
// Used By EF Core CLI Tools
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private const string ConnectionString = "Data Source=/Users/shakibharis/Template.db";
    
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite(ConnectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}