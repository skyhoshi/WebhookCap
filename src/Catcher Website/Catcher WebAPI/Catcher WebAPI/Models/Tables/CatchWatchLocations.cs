using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Catcher_WebAPI.Models.Tables
{
    [Table("tblLocations")]
    public class CatchWatchLocations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50, ErrorMessage = "Location Name must be shorter than 50 Chars")]
        [DisallowNull]
        [NotNull]
        public string Location { get; set; }

        public string? FormName { get; set; }
    }
}
