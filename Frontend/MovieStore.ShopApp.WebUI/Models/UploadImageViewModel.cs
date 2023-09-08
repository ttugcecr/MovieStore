using System.ComponentModel.DataAnnotations;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class UploadImageViewModel
    {
        //[Required]
        [Display(Name = "Image")]
        public IFormFile? ProductPicture { get; set; }
    }
}
