using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class OrderSupply
    {
        public int OrderSupplyID { get; set; }
        [Display(Name = "Typ dostarczenia")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        //[RegularExpression(@"^[\p{L}]+([-_][\p{L}]+)*$", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter.")]
        public string Name { get; set; }
        [Display(Name = "Czas dostarczenia")]
        public string Time { get; set; }
        [Display(Name = "Koszt")]
        public float Price { get; set; }

    }
}