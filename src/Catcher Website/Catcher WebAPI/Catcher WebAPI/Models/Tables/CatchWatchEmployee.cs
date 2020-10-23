using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Catcher_WebAPI.Data.CanvasData;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Catcher_WebAPI.Models.Tables
{
    [Table("tblEmployees")]
    public class CatchWatchEmployee
    {
        [Key]
        public int LineID { get; set; }
        public int? BadgeID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Location { get; set; }
        public bool Active { get; set; } = true;
        public bool Suspended { get; set; } = false;



        [NotMapped]
        public static List<string> AvailableLocationsAsStrings { get; set; } = new List<string>();

        //private List<SelectListItem> _locationsAsSelectItems;
        //[NotMapped]
        //public virtual List<SelectListItem> AvailableLocationsAsSelectItems
        //{
        //    get
        //    {

        //        _locationsAsSelectItems = new List<SelectListItem>();


        //    }
        //}

        //public void PopulateAvailableLocations(GoCanvasDbContext dbContext)
        //{
        //    var locaitons = dbContext.Locations.Select(a=>a);

        //}

    }
}
