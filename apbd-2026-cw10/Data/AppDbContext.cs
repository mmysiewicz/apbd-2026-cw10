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

        modelBuilder.Entity<PC>().HasData(new List<PC>()
        {
            new PC()
            {
                Id = 1, Name = "PC 1", Weight = 2, Warranty = 1, CreatedAt = new DateTime(2026, 01, 15), Stock = 3
            },
            new PC()
            {
                Id = 2, Name = "PC 2", Weight = 4, Warranty = 2, CreatedAt = new DateTime(2026, 03, 22), Stock = 2
            },
            new PC()
            {
                Id = 3, Name = "PC 3", Weight = 5, Warranty = 3, CreatedAt = new DateTime(2026, 05, 07), Stock = 4
            }
        });

        modelBuilder.Entity<ComponentManufacturer>().HasData(new List<ComponentManufacturer>()
        {
            new ComponentManufacturer()
            {
                Id = 1, Abbreviation = "SVD", FoundationDate =  new DateTime(2026, 02, 02), FullName = "SVD Manufacturer"
            },
            new ComponentManufacturer()
            {
            Id = 2, Abbreviation = "XYZ", FoundationDate =  new DateTime(2026, 04, 01), FullName = "XYZ Manufacturer"
            },
            new ComponentManufacturer()
            {
                Id = 3, Abbreviation = "ABC", FoundationDate =  new DateTime(2026, 05, 14), FullName = "ABC Manufacturer"
            }
        });

        modelBuilder.Entity<ComponentType>().HasData(new List<ComponentType>()
        {
            new ComponentType() { Id = 1, Name = "RAM" },
            new ComponentType() { Id = 2, Name = "Graphics card" },
            new ComponentType() { Id = 3, Name = "Processor" }
        });

        modelBuilder.Entity<Component>().HasData(new List<Component>()
        {
            new Component()
            {
                Code = "X01", Name = "RTX 4060", Description = "Good graphics card", ComponentManufacturersId = 2,
                ComponentTypesId = 2
            },
            new Component()
            {
                Code = "A01", Name = "NICERAM 16GB", Description = "Nice RAM", ComponentManufacturersId = 3,
                ComponentTypesId = 1
            },
            new Component()
            {
                Code = "Y01", Name = "Intel i7 12th", Description = "Good processor", ComponentManufacturersId = 1,
                ComponentTypesId = 3
            }
        });

        modelBuilder.Entity<PCComponent>().HasData(new List<PCComponent>()
        {
            new PCComponent()
            {
                PCId = 1, ComponentCode = "Y01", Amount = 1000
            },
            new PCComponent()
            {
                PCId = 2, ComponentCode = "A01", Amount = 500
            },
            new PCComponent()
            {
                PCId = 3, ComponentCode = "X01", Amount = 3000
            }
        });
        
        base.OnModelCreating(modelBuilder);
    }
}