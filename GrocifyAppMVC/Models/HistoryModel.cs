using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GrocifyAppMVC.Models
{
    public class HistoryModel
    {

        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }

        public Status Status { get; set; }

        public string Name { get; set; }

        public string BoughtBy { get; set; }

        public decimal Debt { get; set; }
        public string DebtEuro
        {
            get
            {
                if (Debt == 0)
                {
                    return Debt.ToString("-");
                }
                return Debt.ToString("C", new CultureInfo("nl-NL"));
            }
            set
            {
                var Debt = value;
            }
        }

        //[Display(Name = "Status")]
        //public HiddenStatus HiddenStatus { get; set; }
    }
}