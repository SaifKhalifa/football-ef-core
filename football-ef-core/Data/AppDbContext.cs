using football_ef_core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Data;

internal class AppDbContext : DbContext
{
    public const string MySQLConnectionString = """
        Server=localhost;
        Database=footballDB;
        Uid=root;
        Pwd=saif8808;
        """;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(MySQLConnectionString, ServerVersion.AutoDetect(MySQLConnectionString));
    }

    public DbSet<Player> Player { get; set; }
    public DbSet<Coach> Coach { get; set; }
    public DbSet<TrainingSession> TrainingSession { get; set; }
}
