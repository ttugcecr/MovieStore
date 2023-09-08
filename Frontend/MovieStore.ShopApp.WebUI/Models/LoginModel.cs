using System.ComponentModel.DataAnnotations;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class LoginModel
    {
        //[Required]
        //[DataType(DataType.EmailAddress)]
        ////public string UserName { get; set; }
        //public string Email { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        //public string ReturnUrl { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsChecked { get; set; } = true;

    }
}
