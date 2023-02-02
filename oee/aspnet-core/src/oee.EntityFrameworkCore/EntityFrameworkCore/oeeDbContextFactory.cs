using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace oee.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class oeeDbContextFactory : IDesignTimeDbContextFactory<oeeDbContext>
{
    public oeeDbContext CreateDbContext(string[] args)
    {
        oeeEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<oeeDbContext>()
        //.UseNpgsql(configuration.GetConnectionString("Default"));
        .UseSqlServer(configuration.GetConnectionString("Default"));


        return new oeeDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../oee.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
