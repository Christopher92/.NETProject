using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public int? OrderUserID { get; set; }
        public int OrderStateID { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public int OrderPaymentID { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public int OrderSupplyID { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime? IncomingDate { get; set; }
        [Display(Name = "Data realizacji")]
        public DateTime? OutcomingDate { get; set; }
        [Display(Name = "Suma")]
        public double Suma { get; set; }
        public virtual User User { get; set; }
        public virtual OrderUser OrderUser { get; set; } 
        public virtual OrderPayment OrderPayment { get; set; }
        public virtual OrderSupply OrderSupply { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

    }
}