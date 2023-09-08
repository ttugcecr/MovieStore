using Microsoft.AspNetCore.Identity;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class CustomerIdentityValidator: IdentityErrorDescriber
    {

        //regıster esnasın hata kodarını turkcelstırme
        public override IdentityError PasswordTooShort(int legent)
        {
            return new IdentityError
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {legent} karekter olmalıdır"
            };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresLower",
                Description = "Parola en az 1 adet küçük harf içermelidir"
            };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = "PasswordTooUpper",
                Description = "Parola en az 1 adet büyük harf içermelidir"
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Parola en az 1 adet Sembol içermelidir"
            };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = "Parola en az 1 rakam  içermelidir"
            };
        }

    }
}

