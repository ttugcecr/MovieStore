using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStore.BusinessLayer.Abstract;
using MovieStore.EntityLayer.Concrete;
using MovieStore.ShopApp.WebUI.Extensions;
using MovieStore.ShopApp.WebUI.Models;
using MovieStore.ShopApp.WebUI.SendMail;

namespace MovieStore.ShopApp.WebUI.Controllers
{
   
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private ISendEMail _sendEMail;
        private ICartService _cartService;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICartService cartService, ISendEMail sendEMail)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
          
            this._cartService = cartService;
            _sendEMail = sendEMail;
        }
        //----------------------------------------------------------------------------
       
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.IsChecked, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user.EmailConfirmed == true)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();

        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser()
            {
                FirtName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
              
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                //email
                _sendEMail.SendMailForRegister(model.FirstName,model.LastName,user.Email,url);
              
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
                                                
            TempData.Put("message", new AlertMessage()
            {
                Title = "Oturum Kapandı.",
                Message = "Hesabınız Güvenli bir şekilde kapatıldı.",
                AlertType = "success"

            });
            return Redirect("~/");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Geçersiz Token",
                    Message = "Geçersiz Token",
                    AlertType = "danger"

                });


                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                   
                    _cartService.InitializeCart(userId);

                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Hesabınız Onaylandı.",
                        Message = "Hesabınız Onaylandı.",
                        AlertType = "success"

                    });

                    return View();
                }
            }
           
            TempData.Put("message", new AlertMessage()
            {
                Title = "Hesabınız Onaylanmadı.",
                Message = "Hesabınız Onaylanmadı tekrar kontrol edınız.",
                AlertType = "warning"

            });

            return View();


        }

      
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();

            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });

            //email
             _sendEMail.SendMailForForgotPassword(user.Email, url);


           
            return RedirectToAction("Login", "Account");

        }


        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {   
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kullanıcı veya kod hatalıdır",
                    Message = "kullancı veya kod hatalıdır.",
                    AlertType = "danger"

                });

                return RedirectToAction("Login", "Account");

            }
            var model = new ResetPasswordModel
            {
                Token = token
            };
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Bu email kayıtlı değildir.",
                    Message = " üyelğiniz yok yada yanlış yazıldı",
                    AlertType = "danger"

                });


                return RedirectToAction("Login", "Account");

            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
