using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ComputerShop.Models
{
    public class ProductOrderState
    {
        public int ProductOrderStateID { get; set; }
        [Display(Name = "Status zamówienia")]
        public string Name { get; set; }

    }
}