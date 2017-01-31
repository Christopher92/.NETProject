using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerShop.Models
{
    public class ProductFeedback
    {
        public int ProductFeedbackID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public DateTime PublicationDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

    }
}