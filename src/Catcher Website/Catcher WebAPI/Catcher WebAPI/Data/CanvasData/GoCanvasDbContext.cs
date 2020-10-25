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
        Adding Initial Migration
    //DO NOT DELETE THE INITAL MIGRATION
        Add-Migration -Name InitialGoCanvasMigration -OutputDir Data/CanvasData/Migrations -Context GoCanvasDbContext -Project CatchWatch;
        Adding Additional Migrations
        Add-Migration -Name <migrationName> -OutputDir Data/CanvasData/Migrations -Context GoCanvasDbContext -Project CatchWatch;

        Remove-Migration
        Remove-Migration -Context GoCanvasDbContext -Project CatchWatch;
        Update-Database
        Update-Database -Context GoCanvasDbContext -Project CatchWatch;    
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
