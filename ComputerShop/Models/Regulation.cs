using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ComputerShop.Models
{
    public class Regulation
    {
        public int RegulationID { get; set; }
        [Required(ErrorMessage="Pole jest wymagane.")]
        [StringLength(5000, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Rule { get; set; }
    }
}