using Microsoft.EntityFrameworkCore;
using SpaceMetaApi.Entities;
using System.Data;
using System.Data.SqlClient;

namespace SpaceMetaApi.Data;

public class SpaceMetaDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public SpaceMetaDbContext(IConfiguration configuration)
    {
        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString(isDevelopment ? "SpaceMetaDevDb" : "SpaceMetaDb");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public virtual DbSet<Role>? Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        var connectionString = configuration.GetConnectionString(isDevelopment ? "SpaceMetaDevDb" : "SpaceMetaDb");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .ToTable("Role")
            .HasKey(nameof(Role.Id));
    }
}
