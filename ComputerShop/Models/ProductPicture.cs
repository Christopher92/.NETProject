using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class ProductPicture
    {
        public int ProductPictureID { get; set; }
        [Display(Name = "Ścieżka do zdjęcia")]
        [StringLength(150, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.")]
        public string Path { get; set; }
    }
}