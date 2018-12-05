using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrocifyAppMVC.Models
{
    public enum Status
    {
        [Display(Name = "Ik haal het zelf")]
        GetItMyself = 1,

        [Display(Name = "Iemand anders mag dit halen")]
        GetItForMe,

        [Display(Name = "Ik heb het in huis")]
        HaveIt,

        [Display(Name = "Gehaald")]
        Bought
    }

    public enum HiddenStatus
    {
        [Display(Name = "Actief")]
        Active = 1,

        [Display(Name = "Gearchiveerd")]
        Archived
    }
}