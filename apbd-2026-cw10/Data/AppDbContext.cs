using apbd_2026_cw10.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(50);
            e.Property(p => p.Weight).HasColumnType("float(5)");

            e.ToTable("PCs");
        });

        modelBuilder.Entity<Component>(e =>
        {
            e.HasKey(c => c.Code);
            e.Property(c => c.Code).HasColumnType("char(10)");
            e.Property(c => c.Name).HasMaxLength(300);

            e.HasOne(c => c.ComponentManufacturer)
                .WithMany(cm => cm.Components)
                .HasForeignKey(c => c.ComponentManufacturersId);
            
            e.HasOne(c => c.ComponentType)
                .WithMany(cm => cm.Components)
                .HasForeignKey(c => c.ComponentTypesId);
            
            e.ToTable("Components");
        });

        modelBuilder.Entity<PCComponent>(e =>
        {
            e.HasKey(pcc => new { pcc.PCId, pcc.ComponentCode });
            e.Property(pcc => pcc.ComponentCode).HasColumnType("char(10)");
            
            e.HasOne(pcc => pcc.PC)
                .WithMany(p  => p.PCComponents)
                .HasForeignKey(pcc => pcc.PCId)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.HasOne(pcc => pcc.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(pcc => pcc.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
            
            e.ToTable("PCComponents");
        });

        modelBuilder.Entity<ComponentManufacturer>(e =>
        {
            e.HasKey(cm => cm.Id);
            e.Property(cm => cm.Abbreviation).HasMaxLength(30);
            e.Property(cm => cm.FullName).HasMaxLength(300);
            e.Property(cm => cm.FoundationDate).HasColumnType("date");
            
            e.ToTable("ComponentManufacturers");
        });

        modelBuilder.Entity<ComponentType>(e =>
        {
            e.HasKey(ct => ct.Id);
            e.Property(ct => ct.Abbreviation).HasMaxLength(30);
            e.Property(ct => ct.Name).HasMaxLength(150);

            e.ToTable("ComponentTypes");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}