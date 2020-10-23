using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Catcher_WebAPI.Models.Tables
{
    [Table("tblMySubmissions")]
    public class MySubmissions
    {
        /*
        [LineID] [int] IDENTITY(1,1) NOT NULL,
        [GoCanSubmissionGUID] [nvarchar](50) NULL,
        [FormName] [nvarchar](50) NULL, 
        */
        [Key]
        public int LineID { get; set; }
        [StringLength(50)]
        public string GoCanSubmissionGUID { get; set; }
        [StringLength(50)]
        public string FormName { get; set; }
    }
}
