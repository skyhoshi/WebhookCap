using System;
using System.Collections.Generic;
using System.Text;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catcher_WebAPI.Data
{    /*
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

        Add-Migration -Name InitialApplicationGoCanvasUser -OutputDir Data/UserManagement/Migrations -Context ApplicationDbContext -Project CatchWatch;
        Adding Additional Migrations
        Add-Migration -Name <migrationName> -OutputDir Data/UserManagement/Migrations -Context ApplicationDbContext -Project CatchWatch;
        Remove-Migration
        Update-Database -migration 0 -Context ApplicationDbContext -Project CatchWatch;
        Remove-Migration -Context ApplicationDbContext -Project CatchWatch;
        Update-Database
        Update-Database -Context ApplicationDbContext -Project CatchWatch;
        ///////////////////////////////////
        Script-DbContext -context ApplicationDbContext -project CatchWatch;
     */
    public class ApplicationDbContext : IdentityDbContext<tblUser, IdentityRole<string>, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override DbSet<tblUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.HasDefaultSchema("UserManagement");

            builder.Entity<tblUser>(iu => iu
                .ToTable("tblUser")
                .HasKey(nameof(tblUser.UserId))
                .HasName("PK_UserId")
                //.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues)
                //.HasData(new List<tblUser>() { new tblUser("administrator")
                //{
                //    PasswordHash = "",
                //    PasswordSeed = "",
                //    PasswordSalt = "",
                //    LockoutEnabled = true, 
                //    LockoutEnd = new DateTimeOffset(DateTime.Now.AddYears(400))
                //} })
            );

            builder.Entity<tblUser>(iu => iu
                .Property(typeof(string), nameof(tblUser.UserId))
                .HasColumnName("UserID").IsRequired(true)
                .HasMaxLength(450));
            builder.Entity<tblUser>(iu => iu.Ignore("Id")
                //.Metadata
                //.RemoveProperty("Id")
                //.Property(typeof(string), nameof(tblUser.Id))
                //.HasColumnName("UserID")
                //.IsRequired(true)
                //.HasMaxLength(450)
                );
            builder.Entity<tblUser>(iu => iu.Ignore("UserName")
                //.Metadata
                //.RemoveProperty("UserName")
                ////.Property(p => p.UserName)
                //.Property(typeof(string), nameof(tblUser.UserName))
                //.HasColumnName("UserID").IsRequired(true)
                //.HasMaxLength(450)
                );

            builder.Entity<tblUser>(tu => tu
                .Property("RowVersion")
                .IsRowVersion()
                .IsRequired(true)
            );

            builder.Entity<tblUser>(u => u.Property(p => p.EmailConfirmed).HasDefaultValue(false));
            builder.Entity<tblUser>(u => u.Property(p => p.PhoneNumberConfirmed).HasDefaultValue(false));
            builder.Entity<tblUser>(u => u.Property(p => p.TwoFactorEnabled).HasDefaultValue(false));
            builder.Entity<tblUser>(u => u.Property(p => p.LockoutEnabled).HasDefaultValue(false));
            builder.Entity<tblUser>(u => u.Property(p => p.AccessFailedCount).HasDefaultValue(0));
            builder.Entity<IdentityUserClaim<string>>(iuc => iuc.ToTable("tblUserClaim"));
            builder.Entity<IdentityUserLogin<string>>(iul => iul.ToTable("tblUserLogin"));
            builder.Entity<IdentityUserToken<string>>(iut => iut.ToTable("tblUserToken"));
            builder.Entity<IdentityUserRole<string>>(iur => iur.ToTable("tblUserUsersRole"));
            builder.Entity<IdentityRole<string>>(ir => ir.ToTable("tblUserRole"));
            builder.Entity<IdentityRoleClaim<string>>(irc => irc.ToTable("tblUserRoleClaim"));



        }
    }
}
