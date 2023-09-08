using System.ComponentModel.DataAnnotations;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
