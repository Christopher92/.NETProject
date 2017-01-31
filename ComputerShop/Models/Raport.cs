using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class Raport
    {
        public int RaportID { get; set; }
        public DateTime Date { get; set; }
        public double OrdersQuantity { get; set; }
        public double DailyRevenue { get; set; }
        public double ProductsQuantity { get; set; }
        public int Realised { get; set; }
    }
}