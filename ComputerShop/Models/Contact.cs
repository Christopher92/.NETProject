using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ComputerShop.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Numer telefonu")]
        public int Telephone { get; set; }

        //[RegularExpression(@"/^[\p{L}\d]+([-_][\p{L}\d]+)*$/i", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter oraz cyfr.")]
        [Required(ErrorMessage="Pole jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} może składać się tylko z cyfr.")]
        [Required(ErrorMessage="Pole jest wymagane.")]
        [StringLength(26, ErrorMessage = "{0} musi posiadać {1} znaków.",MinimumLength =26)]
        [Display(Name = "Numer konta")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        [EmailAddress(ErrorMessage = "Wymagany jest email.")]
        public string Email { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Godziny otwarcia (pon-pt)")]
        [StringLength(20, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string WeekOpenTime { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Godziny otwarcia (so)")]
        [StringLength(20, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string SaturdayOpenTime { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [StringLength(20, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        [Display(Name = "Godziny otwarcia (nd)")]
        public string SundayOpenTime { get; set; }

        [StringLength(1000, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string GoogleMapUrl { get; set; }
    }
}