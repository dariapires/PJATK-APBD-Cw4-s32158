using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tut7Solution.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_ComponentManufacturers", x => x.Id));

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_ComponentTypes", x => x.Id));

            migrationBuilder.CreateTable(
                name: "PCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_PCs", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentManufacturerId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Code);
                    table.ForeignKey("FK_Components_ComponentManufacturers_ComponentManufacturerId", x => x.ComponentManufacturerId, "ComponentManufacturers", "Id", onDelete: ReferentialAction.Restrict);
                    table.ForeignKey("FK_Components_ComponentTypes_ComponentTypeId", x => x.ComponentTypeId, "ComponentTypes", "Id", onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PCComponents",
                columns: table => new
                {
                    PCId = table.Column<int>(type: "int", nullable: false),
                    ComponentCode = table.Column<string>(type: "char(10)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCComponents", x => new { x.PCId, x.ComponentCode });
                    table.ForeignKey("FK_PCComponents_Components_ComponentCode", x => x.ComponentCode, "Components", "Code", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_PCComponents_PCs_PCId", x => x.PCId, "PCs", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData("ComponentTypes", new[] { "Id", "Abbreviation", "Name" }, new object[,]
            {
                { 1, "CPU", "Processor" }, { 2, "GPU", "Graphics Card" }, { 3, "RAM", "Memory" }
            });

            migrationBuilder.InsertData("ComponentManufacturers", new[] { "Id", "Abbreviation", "FullName", "FoundationDate" }, new object[,]
            {
                { 1, "AMD", "Advanced Micro Devices", new DateTime(1969, 5, 1) },
                { 2, "NV", "NVIDIA Corporation", new DateTime(1993, 4, 5) },
                { 3, "COR", "Corsair Gaming Inc.", new DateTime(1994, 1, 1) }
            });

            migrationBuilder.InsertData("PCs", new[] { "Id", "Name", "Weight", "Warranty", "CreatedAt", "Stock" }, new object[,]
            {
                { 1, "Gaming Beast X", 12.5, 36, new DateTime(2026, 5, 8, 9, 0, 0), 5 },
                { 2, "Office Mini Pro", 4.2, 24, new DateTime(2026, 4, 15, 13, 30, 0), 12 },
                { 3, "Student Basic PC", 6.8, 12, new DateTime(2026, 3, 20, 10, 15, 0), 8 }
            });

            migrationBuilder.InsertData("Components", new[] { "Code", "Name", "Description", "ComponentManufacturerId", "ComponentTypeId" }, new object[,]
            {
                { "CPU0000001", "Ryzen 7 7800X3D", "8-core gaming processor", 1, 1 },
                { "GPU0000001", "RTX 4080 Super", "High-end gaming graphics card", 2, 2 },
                { "RAM0000001", "Corsair Vengeance DDR5 16GB", "DDR5 RAM module 16GB", 3, 3 }
            });

            migrationBuilder.InsertData("PCComponents", new[] { "PCId", "ComponentCode", "Amount" }, new object[,]
            {
                { 1, "CPU0000001", 1 }, { 1, "GPU0000001", 1 }, { 1, "RAM0000001", 2 },
                { 2, "CPU0000001", 1 }, { 2, "RAM0000001", 1 }, { 3, "CPU0000001", 1 }
            });

            migrationBuilder.CreateIndex("IX_Components_ComponentManufacturerId", "Components", "ComponentManufacturerId");
            migrationBuilder.CreateIndex("IX_Components_ComponentTypeId", "Components", "ComponentTypeId");
            migrationBuilder.CreateIndex("IX_PCComponents_ComponentCode", "PCComponents", "ComponentCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PCComponents");
            migrationBuilder.DropTable(name: "Components");
            migrationBuilder.DropTable(name: "PCs");
            migrationBuilder.DropTable(name: "ComponentManufacturers");
            migrationBuilder.DropTable(name: "ComponentTypes");
        }
    }
}
