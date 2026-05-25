using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Tut7Solution.Data;

#nullable disable

namespace Tut7Solution.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");
            modelBuilder.Entity("Tut7Solution.Models.Component", b =>
            {
                b.Property<string>("Code").HasColumnType("char(10)");
                b.Property<int>("ComponentManufacturerId");
                b.Property<int>("ComponentTypeId");
                b.Property<string>("Description").HasColumnType("nvarchar(max)");
                b.Property<string>("Name").IsRequired().HasMaxLength(300);
                b.HasKey("Code");
                b.HasIndex("ComponentManufacturerId");
                b.HasIndex("ComponentTypeId");
                b.ToTable("Components");
                b.HasData(
                    new { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturerId = 1, ComponentTypeId = 1 },
                    new { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturerId = 2, ComponentTypeId = 2 },
                    new { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturerId = 3, ComponentTypeId = 3 });
            });

            modelBuilder.Entity("Tut7Solution.Models.ComponentManufacturer", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("Abbreviation").IsRequired().HasMaxLength(30);
                b.Property<DateTime>("FoundationDate").HasColumnType("date");
                b.Property<string>("FullName").IsRequired().HasMaxLength(300);
                b.HasKey("Id");
                b.ToTable("ComponentManufacturers");
                b.HasData(
                    new { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
                    new { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
                    new { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateTime(1994, 1, 1) });
            });

            modelBuilder.Entity("Tut7Solution.Models.ComponentType", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("Abbreviation").IsRequired().HasMaxLength(30);
                b.Property<string>("Name").IsRequired().HasMaxLength(150);
                b.HasKey("Id");
                b.ToTable("ComponentTypes");
                b.HasData(
                    new { Id = 1, Abbreviation = "CPU", Name = "Processor" },
                    new { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
                    new { Id = 3, Abbreviation = "RAM", Name = "Memory" });
            });

            modelBuilder.Entity("Tut7Solution.Models.PC", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<DateTime>("CreatedAt").HasColumnType("datetime");
                b.Property<string>("Name").IsRequired().HasMaxLength(50);
                b.Property<int>("Stock");
                b.Property<int>("Warranty");
                b.Property<double>("Weight");
                b.HasKey("Id");
                b.ToTable("PCs");
                b.HasData(
                    new { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
                    new { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
                    new { Id = 3, Name = "Student Basic PC", Weight = 6.8, Warranty = 12, CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0), Stock = 8 });
            });

            modelBuilder.Entity("Tut7Solution.Models.PCComponent", b =>
            {
                b.Property<int>("PCId");
                b.Property<string>("ComponentCode").HasColumnType("char(10)");
                b.Property<int>("Amount");
                b.HasKey("PCId", "ComponentCode");
                b.HasIndex("ComponentCode");
                b.ToTable("PCComponents");
                b.HasData(
                    new { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
                    new { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
                    new { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
                    new { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 },
                    new { PCId = 2, ComponentCode = "RAM0000001", Amount = 1 },
                    new { PCId = 3, ComponentCode = "CPU0000001", Amount = 1 });
            });

            modelBuilder.Entity("Tut7Solution.Models.Component", b =>
            {
                b.HasOne("Tut7Solution.Models.ComponentManufacturer", "Manufacturer").WithMany("Components").HasForeignKey("ComponentManufacturerId").OnDelete(DeleteBehavior.Restrict).IsRequired();
                b.HasOne("Tut7Solution.Models.ComponentType", "Type").WithMany("Components").HasForeignKey("ComponentTypeId").OnDelete(DeleteBehavior.Restrict).IsRequired();
                b.Navigation("Manufacturer");
                b.Navigation("Type");
            });

            modelBuilder.Entity("Tut7Solution.Models.PCComponent", b =>
            {
                b.HasOne("Tut7Solution.Models.Component", "Component").WithMany("PCComponents").HasForeignKey("ComponentCode").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.HasOne("Tut7Solution.Models.PC", "PC").WithMany("PCComponents").HasForeignKey("PCId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.Navigation("Component");
                b.Navigation("PC");
            });
        }
    }
}
