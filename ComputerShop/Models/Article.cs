using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ComputerShop.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Data publikacji")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage="Pole jest wymagane.")]
        [Display(Name = "Dostępność")]
        public bool Enable { get; set; }

        [Display(Name = "Treść")]
        public string ShortContent
        {
            get
            {
                return Content != null ? Content.Substring(0,
              Content.Length <= 350 ? Content.Length : 350
              ) + "..." : "";
            }
        }

        public int x { get; set; }
    }
}