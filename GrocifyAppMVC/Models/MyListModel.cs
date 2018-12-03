using GrocifyAppMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrocifyAppMVC.Models
{ 
    public class MyListModel
    {
        public int Id { get; set; }

        [Display(Name = "Productnaam")]
        public string ProductName { get; set; }

        [Display(Name = "Hoeveelheid")]
        public int Amount { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        [Display(Name = "Gehaald door")]
        public string BoughtBy { get; set; }
    }
}