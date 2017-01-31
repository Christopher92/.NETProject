using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class OrderProduct
    {
        public int OrderProductID { get; set; }
        public int ProductID { get; set; }
        public int? OrderID { get; set; }
        public int ProductOrderStateID { get; set; }
        public string userID { get; set; }
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }
        [Display(Name = "Suma")]
        public double Suma { get; set; }
    

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
    public virtual ProductOrderState ProductOrderState { get; set; }


}
}