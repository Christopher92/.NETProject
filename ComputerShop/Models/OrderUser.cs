using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComputerShop.Validators;

namespace ComputerShop.Models
{
    public class OrderUser
    {
        public int OrderUserID { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Wymagany jest email.")]
        [StringLength(200, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Email { get; set; }

        [Display(Name = "Imię")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        //[RegularExpression(@"^([\p{L}\d])*$", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter.")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        //[RegularExpression(@"^([\p{L}\d])*$", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter.")]
        public string Surname { get; set; }

        //pomyśleć o zamianie na nową tabelę
        [Display(Name = "Adres")]
        [StringLength(200, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        [StringLength(12, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.",MinimumLength =7)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} może składać się tylko z cyfr.")]
        public string Telephone { get; set; }
    }
}