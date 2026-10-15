using football_ef_core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Data;

internal class AppDbContext : DbContext
{
    public string MySQLConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MySQL");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (MySQLConnectionString != null)
        {
            optionsBuilder.UseMySql(MySQLConnectionString, ServerVersion.AutoDetect(MySQLConnectionString));
        }
        else
        {
            Console.WriteLine("ERROR: connection string couldn't be loaded from environment");
        }
    }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ========== Player entity configs ==========
        modelBuilder.Entity<Player>(
        p =>
        {
            // congiuring the datatypes
            p.Property(x => x.Name).HasMaxLength(50).IsRequired();
            p.Property(x => x.Age).HasColumnType("tinyint unsigned");

            p.ToTable(t => t.HasCheckConstraint("CK_Player_Age_Range", "`Age` BETWEEN 1 AND 50"));

            p.HasMany(x => x.TrainingSessions)
             .WithMany(t => t.Players)
             .UsingEntity(j => j.ToTable("PlayerSessions"));
        });
        
        // ========== Coach entity configs ==========
        // datatypes config
        modelBuilder.Entity<Coach>(
            p =>
            {
                p.Property(n => n.Name)
                .HasMaxLength(50)
                .IsRequired();
            });

        // 1:M relation, each coach have more than one training sesison, and each training session have one coach.
        modelBuilder.Entity<Coach>()
            .HasMany(c => c.TrainingSessions)
            .WithOne(s => s.Coach)
            .HasForeignKey(s => s.CoachId)
            .IsRequired();

        // ========== TrainingSession entity configs ==========
        modelBuilder.Entity<TrainingSession>(
            p =>
            {
                p.Property(n => n.Title)
                .HasMaxLength(100)
                .IsRequired();

                p.Property(d => d.Date)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("(CURRENT_DATE)");

                p.HasIndex(p => p.Title)
                .IsUnique();
            });


        // setting default delete foreign key behavior to all entities, except the join table

        foreach 
            (
                var fk in modelBuilder.Model.GetEntityTypes()
                .Where(e => !e.IsPropertyBag)           // skips the PlayerSessions join entity
                .SelectMany(e => e.GetForeignKeys())
            )
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
