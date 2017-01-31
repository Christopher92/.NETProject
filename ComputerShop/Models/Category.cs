using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Display(Name="Kategoria")]
        public int? ParentCategoryID { get; set; }

        [StringLength(100, ErrorMessage = "{0} może posiadać maksymalnie {1} znaków.")]
        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name="Nazwa")]
        public string Name { get; set; }

        //[RegularExpression(@"/^[\p{L}\d]+([-_][\p{L}\d]+)*$/i", ErrorMessage = "{0} może składać się tylko z małych i wielkich liter oraz cyfr.")]
        [StringLength(1000, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Dostępność")]
        public bool Enable { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}