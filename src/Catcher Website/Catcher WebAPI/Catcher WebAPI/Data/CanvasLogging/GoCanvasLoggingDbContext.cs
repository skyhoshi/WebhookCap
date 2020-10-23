using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Catcher_WebAPI.Data.CanvasLogging
{

    /*
    Adding Initial Migration

//DO NOT DELETE THE INITAL MIGRATION
    Add-Migration -Name InitialGoCanvasMigration -OutputDir Data/CanvasLogging/Migrations -Context GoCanvasLoggingDbContext -Project CatchWatch;
    Adding Additional Migrations
    Add-Migration -Name <migrationName> -OutputDir Data/CanvasLogging/Migrations -Context GoCanvasLoggingDbContext -Project CatchWatch;

    Remove-Migration
    Remove-Migration -Context GoCanvasLoggingDbContext -Project CatchWatch;
    Update-Database
    Update-Database -Context GoCanvasLoggingDbContext -Project CatchWatch;

 */
    public class GoCanvasLoggingDbContext : DbContext
    {
        public GoCanvasLoggingDbContext() : base(new DbContextOptions<GoCanvasLoggingDbContext>()){ }
        public GoCanvasLoggingDbContext(DbContextOptions<GoCanvasLoggingDbContext> options) : base(options){}

        public virtual DbSet<ResponseLog> ResponseLogs { get; set; }
    }

    [Table("tblResponseLog")]
    public class ResponseLog
    {
        [Key]
        [Column("LineID")]
        public int LineId { get; set; }
        [Column("ResponseBody")]
        public string ResponseBody { get; set; }
        [Column("TestRecord")]
        public int ResponseType { get; set; } = 0;
    }
}
