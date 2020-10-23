using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Catcher_WebAPI.Data.CanvasData
{
    /*
    CommandType     Name                                               Version    Source                                                                                                                                                                             
    -----------     ----                                               -------    ------                                                                                                                                                                             
    Function        Add-Migration                                      3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Drop-Database                                      3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Enable-Migrations                                  3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Get-DbContext                                      3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Remove-Migration                                   3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Scaffold-DbContext                                 3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Script-DbContext                                   3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Script-Migration                                   3.1.8      EntityFrameworkCore                                                                                                                                                                
    Function        Update-Database                                    3.1.8      EntityFrameworkCore
     */
    /*
        Adding Initial Migration

    //DO NOT DELETE THE INITAL MIGRATION
        Add-Migration -Name InitialGoCanvasMigration -OutputDir Data/CanvasData/Migrations -Context GoCanvasDbContext -Project CatchWatch;
        Adding Additional Migrations
        Add-Migration -Name <migrationName> -OutputDir Data/CanvasData/Migrations -Context GoCanvasDbContext -Project CatchWatch;

        Remove-Migration
        Remove-Migration -Context GoCanvasDbContext -Project CatchWatch;
        Update-Database
        Update-Database -Context GoCanvasDbContext -Project CatchWatch;


    ///////////////////////////////////
    P:\Source\CanvasBadgeAutomation\Catcher Website\Catcher WebAPI\Catcher WebAPI\Data\CanvasData\Migrations\GoCanvasDbContextModelSnapshot.cs
                modelBuilder.Entity("Catcher_WebAPI.Models.Tables.CatchWatchLocations", b =>
                {
                    b.Property<string>("FormName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.ToTable("tblLocations");
                });
    P:\Source\CanvasBadgeAutomation\Catcher Website\Catcher WebAPI\Catcher WebAPI\Data\CanvasData\Migrations\20201002161257_InitialGoCanvasMigration.cs
    migrationBuilder.CreateTable(
                name: "tblLocations",
                columns: table => new
                {
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    FormName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

    
     */

    public class GoCanvasDbContext : DbContext
    {
        public GoCanvasDbContext() : base(new DbContextOptions<GoCanvasDbContext>())
        {

        }
        public GoCanvasDbContext(DbContextOptions<GoCanvasDbContext> options) : base(options)
        {

        }

        public DbSet<MySubmissions> MySubmissions { get; set; }
        public DbSet<CatchWatchEmployee> Employees { get; set; }
        public DbSet<CatchWatchLocations> Locations { get; set; }
        public DbSet<SystemControl> SystemControls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatchWatchLocations>(l =>l.HasNoKey() );

            //modelBuilder.Entity<CatchWatchLocations>(builder => builder.HasData(new List<CatchWatchLocations>()
            //{
            //        new CatchWatchLocations() {Location = "Location test 1"},
            //        new CatchWatchLocations() {Location = "Test Location 2"},
            //        new CatchWatchLocations() {Location = "Location Test 3"},
            //}));


            base.OnModelCreating(modelBuilder);
        }
    }
}
