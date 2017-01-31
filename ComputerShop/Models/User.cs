using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComputerShop.Validators;

namespace ComputerShop.Models
{
    public class User
    {
        public string UserID { get; set; }

        [Display(Name = "Rola")]
        public int UserRoleID { get; set; }
        [Display(Name = "Email")]

        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        [EmailAddress(ErrorMessage = "Wymagany jest email.")]
        public string Email { get; set; }

        [Display(Name = "Imię")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Surname { get; set; }

        [Display(Name = "Hasło")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Password { get; set; }

        //pomyśleć o zamianie na nową tabelę
        [Display(Name = "Adres")]
        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} może składać się tylko z cyfr.")]
        //[StringLength(12, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków i maksymalnie {12} znaków.", MinimumLength = 7)]
        public string Telephone { get; set; }

        [IsTrue(ErrorMessage = "Musisz zaakceptować regulamin sklepu.")]
        public bool Agreement { get; set; }


        public virtual UserRole UserRole { get; set; }



    }
}