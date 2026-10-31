using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Warehouse.Database;

public class WarehouseDbContextFactory
    : IDesignTimeDbContextFactory<WarehouseDbContext>
{
    public WarehouseDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<WarehouseDbContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("WarehouseDb"));

        return new WarehouseDbContext(optionsBuilder.Options);
    }
}