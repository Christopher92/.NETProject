using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ComputerShop.Models
{
    public class OrderProductHistory
    {
        public int OrderProductHistoryID { get; set; }
        public int OrderProductID { get; set; }
        [Display(Name = "Cena")]
        public double Price { get; set; }

        public virtual OrderProduct OrderProduct { get; set; }
    }
}