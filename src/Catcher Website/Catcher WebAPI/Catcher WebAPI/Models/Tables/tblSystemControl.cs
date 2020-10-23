using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Catcher_WebAPI.Models.Tables
{
    [Table("tblSystemStatus")]
    public class SystemControl
    {
        [Key]
        public int LineID { get; set; }
        [Column("Status")]
        public int Status{ get; set; }
    }
}
