using System.ComponentModel.DataAnnotations;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class ProductModel : EditImageViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }


        [Required(ErrorMessage = "Url zorunlu bir alan")]
        [Display(Name = "Url", Prompt = "Enter product Url")]
        public string Url { get; set; }


        public double? Price { get; set; }

        //[Required(ErrorMessage = "ImageUrl zorunlu bir alan")]
        //[Display(Name = "ImageUrl", Prompt = "Enter product ImageUrl")]
        //public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = "Description zorunlu bir alan")]
        [Display(Name = "Description", Prompt = "Enter product Description")]
        public string Description { get; set; }

        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public int CategoryId { get; set; }

        // public List<Category>? SelectedCategories { get; set; }
    }
}