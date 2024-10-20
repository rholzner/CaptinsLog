using Captinslog.Domain;
using Captinslog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Captinslog.Infrastructure;

public class LogEntryDatabaseContext : DbContext
{
    public DbSet<LogEntryEntity> LogEntries { get; set; }
    public DbSet<CorrelationEntity> Correlations { get; set; }
    public DbSet<StoryEntity> Stories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntryEntity>(opt =>
        {
            opt.HasKey(x => x.Id);
            opt.Property(x => x.Id).ValueGeneratedOnAdd();
            opt.Property(x => x.Created).ValueGeneratedOnAdd();

            opt.HasOne(x => x.Correlation).WithMany(x => x.LogEntries).HasForeignKey(x => x.CorrelationId);
        });

        modelBuilder.Entity<CorrelationEntity>(opt =>
        {
            opt.HasKey(x => x.Id);
            opt.Property(x => x.Id).ValueGeneratedOnAdd();
            opt.Property(x => x.Created).ValueGeneratedOnAdd();

            opt.HasIndex(x => x.CorrelationId).IsUnique();

            opt.HasOne(x => x.Story).WithMany(x => x.Correlations).HasForeignKey(x => x.StoryId);
        });

        modelBuilder.Entity<StoryEntity>(opt =>
        {
            opt.HasKey(x => x.Id);
            opt.Property(x => x.Id).ValueGeneratedOnAdd();
            opt.Property(x => x.Created).ValueGeneratedOnAdd();

            opt.HasIndex(x => x.StoryId).IsUnique();

            opt.HasMany(x => x.Correlations).WithOne(x => x.Story).HasForeignKey(x => x.StoryId);
        });
    }
}
