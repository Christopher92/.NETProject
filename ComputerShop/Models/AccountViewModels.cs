using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ComputerShop.Validators;

namespace ComputerShop.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage="Pole jest wymagane.")]
        public string Provider { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamiętaj przeglądarkę")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Wymagany jest email.")]
        public string Email { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Imię")]
        [StringLength(200, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Nazwisko")]
        [StringLength(200, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Telefon")]
        public long Telephone { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Adres")]
        [StringLength(200, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        public string Address { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [EmailAddress(ErrorMessage = "Email jest nieprawidłowy")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Powtórzone hasło nie jest zgodne z hasłem powyżej.")]
        public string ConfirmPassword { get; set; }

        [IsTrue(ErrorMessage = "Musisz zaakceptować regulamin sklepu.")]
        public bool Agreement { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage="Pole jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage="Pole jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
