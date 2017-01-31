using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class OrderPayment
    {
        public int OrderPaymentID { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        //[RegularExpression(@"^[\p{L}]+([-_][\p{L}]+)*$", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter.")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        [Display(Name="Płatność")]
        public string Name { get; set; }

        [Display(Name = "Koszt")]
        public double Price { get; set; }
    }
}