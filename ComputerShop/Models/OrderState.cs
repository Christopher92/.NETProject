using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class OrderState
    {
        public int OrderStateID { get; set; }
        [Display(Name = "Status")]
        //[RegularExpression(@"^[\p{L}]+([-_][\p{L}]+)*$", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter.")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Name { get; set; }
    }
}