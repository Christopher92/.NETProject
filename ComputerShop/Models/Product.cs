using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Kategoria")]
        public int CategoryID { get; set; }

        [Display(Name = "Zdjęcia")]
        public int ProductPictureID { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Nazwa")]
        [StringLength(200, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        //[RegularExpression(@"/^[\p{L}\d]+([-_][\p{L}\d]+)*$/i", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter oraz cyfr.")]
        public string Name { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Cena")]
        public float Price { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Opis")]
        [StringLength(2000, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        //[RegularExpression(@"/^[\p{L}\d]+([-_][\p{L}\d]+)*$/i", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter oraz cyfr.")]
        public string Description { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [Display(Name = "Dostępność")]
        public bool Enable { get; set; }

        public int x { get; set; }
        public virtual Category Category{get;set;}
        public virtual ProductPicture ProductPicture { get; set; }

    }
}