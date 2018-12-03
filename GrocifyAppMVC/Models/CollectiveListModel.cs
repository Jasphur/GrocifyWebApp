using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrocifyAppMVC.Models
{
    public class CollectiveListModel
    {
        public int Id { get; set; }

        [Display(Name = "Productnaam")]
        public string ProductName { get; set; }

        [Display(Name = "Hoeveelheid")]
        public int Amount { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Gehaald door")]
        public string BoughtBy { get; set; }

        public virtual IEnumerable<SelectListItem> UserNameDrop { get; set; }
    }
}