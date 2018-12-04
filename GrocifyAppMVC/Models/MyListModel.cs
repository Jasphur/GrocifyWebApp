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

        public string ProductName { get; set; }

        public int Amount { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        public string BoughtBy { get; set; }

       public decimal Debt { get; set; }
    }
}