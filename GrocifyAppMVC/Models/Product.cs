﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrocifyAppMVC.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Display(Name = "Productnaam")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Lengte productnaam {0} is te lang en zou tussen {2} en {1} tekens moeten hebben.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Display(Name = "Hoeveelheid")]
        public int Amount { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public Status Status { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Vul jouw naam in")]
        [Display(Name = "Gehaald door")]
        public string BoughtBy { get; set; }

        [Display(Name = "Gemaakte kosten")]
        public decimal Debt { get; set; }
        public string DebtEuro
        { get
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

        public HiddenStatus HiddenStatus { get; set; }
    }
}