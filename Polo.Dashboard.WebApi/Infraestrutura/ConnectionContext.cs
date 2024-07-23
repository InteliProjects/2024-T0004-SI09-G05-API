using Microsoft.EntityFrameworkCore;
using Polo.Dashboard.WebApi.Domain.Model;

namespace Polo.Dashboard.WebApi.Infraestrutura;

public class ConnectionContext : DbContext
{
    public DbSet<Empregados> Empregados { get; set; }
    public DbSet<Cid2023> Cid2023 { get; set; }
    public DbSet<Gptw> Gptw { get; set; }

    public DbSet<Stiba> Stiba { get; set; }
    public DbSet<Zenklub> Zenklub { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        connectionString = Environment.ExpandEnvironmentVariables(connectionString);

        optionsBuilder.UseNpgsql(connectionString);
    }
}