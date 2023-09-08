using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStore.BusinessLayer.Abstract;
using MovieStore.EntityLayer.Concrete;
using MovieStore.ShopApp.WebUI.Extensions;
using MovieStore.ShopApp.WebUI.Models;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Text;

namespace MovieStore.ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IOrderService _orderService;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IProductService productService, ICategoryService categoryService, IOrderService orderService, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, IHttpClientFactory httpClientFactory)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _roleManager = roleManager;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _httpClientFactory = httpClientFactory;
        }
        //----------------------------------------------------
        //user bölümü****************
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {  
                    UserId = user.Id.ToString(),
                    FirstName = user.FirtName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/Admin/UserList/");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirtName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());


                        return Redirect("~/Admin/UserList/");
                    }



                }

                return Redirect("~/Admin/UserList/");

            }
            return View(model);
        }
        public IActionResult UserList()
        {
            var userList = _userManager.Users;
            return View(userList);
        }

        //************ users bölümü******************

        public IActionResult RoleList()
        {
            var roleList = _roleManager.Roles;
            return View(_roleManager.Roles);
        }

        public IActionResult RoleEdit(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            return View(value);

            /*
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<AppUser>();
            var nonmembers = new List<AppUser>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);

            }

            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
            */
        }
        
        [HttpPost]
        public async Task<IActionResult> RoleEdit(AppRole appRole)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == appRole.Id);
            value.Name = appRole.Name;

            await _roleManager.UpdateAsync(value);

            return RedirectToAction("RoleList");

            /*
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                        }
                    }
                }


            }
            //return Redirect("/admin/role/" + model.RoleId);
            return Redirect("/Admin/RoleEdit/");
            */
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)// bu kod tanımlıolan role tablosundakı olabılecek hatalar nelerdır bılmedıgımızden hepsını burda sıraladık
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> RoleDelete(int id)
        {
            var entity = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                await _roleManager.DeleteAsync(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Kayıt Silindi.",
                Message = $"{entity.Name} isimli role Silindi",
                AlertType = "danger"
            });
           
            return RedirectToAction("RoleList");
        }
        //******************roller bölümü son

        public async Task<IActionResult> OrderList()
        {
            /*
            return View(new OrderListViewModel()
            {
                Orders = _orderService.GetAll()
            });
            */

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7126/api/Order");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<OrderListModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7126/api/Order/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Silindi.",
                    Message = $"{id} numaralı Sipariş Silindi",
                    AlertType = "danger"
                });

                return RedirectToAction("OrderList");
            }

            return View();
        }

        public async Task<IActionResult> ProductList()
        {
            /*
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
            */

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7126/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            /*
            var values = _categoryService.GetAll();
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;

            return View();
            */

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7126/api/Category");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonData);
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                var entity = new Product()
                {
                    Name = model.Name,
                    Url = model.Url,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = uniqueFileName,
                    IsApproved = model.IsApproved,
                    IsHome = model.IsHome,
                    CategoryId = model.CategoryId
                };

                /*
                if (_productService.Create(entity))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Ürün başarıyla eklendi.",
                        Message = "Ürün eklendi.",
                        AlertType = "success"
                    });
                    return RedirectToAction("ProductList");
                }
                */

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(entity);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7126/api/Product", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Ürün başarıyla eklendi.",
                        Message = "Ürün eklendi.",
                        AlertType = "success"
                    });

                    return RedirectToAction("ProductList");
                }

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = _productService.ErrorMessage,
                    AlertType = "danger"
                });

                return View(model);
            }

            return View(model);
        }

        private string UploadedFile(ProductModel model)
        {
            string uniqueFileName = null;

            if (model.ProductPicture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductPicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryClient = _httpClientFactory.CreateClient();
            var categoryResponse = await categoryClient.GetAsync("https://localhost:7126/api/Category");
            var jsonCategoryData = await categoryResponse.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonCategoryData);
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7126/api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<ResultProductViewModel>(jsonData);
                var model = new ProductModel()
                {
                    ProductId = entity.ProductId,
                    Name = entity.Name,
                    Url = entity.Url,
                    ExistingImage = entity.ImageUrl,
                    Description = entity.Description,
                    Price = entity.Price,
                    IsApproved = entity.IsApproved,
                    IsHome = entity.IsHome,
                };
                return View(model);
            }

            return View();

            /*
            var entity = _productService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                ExistingImage = entity.ImageUrl,
                Description = entity.Description,
                Price = entity.Price,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
            };

            var values = _categoryService.GetAll();
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;

            return View(model);
            */
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var productClient = _httpClientFactory.CreateClient();
                var productResponse = await productClient.GetAsync($"https://localhost:7126/api/Product/{model.ProductId}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var jsonProductData = await productResponse.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ResultProductViewModel>(jsonProductData);

                    if (product == null)
                    {
                        NotFound();
                    }
                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.Url = model.Url;
                    product.CategoryId = model.CategoryId;
                    product.ImageUrl = model.ExistingImage;
                    product.IsApproved = model.IsApproved;
                    product.IsHome = model.IsHome;

                    if (file != null)
                    {
                        var extentionUzanti = Path.GetExtension(file.FileName);
                        var randomName = string.Format($"{Guid.NewGuid()}{extentionUzanti}");

                        product.ImageUrl = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    var client = _httpClientFactory.CreateClient();
                    var jsonData = JsonConvert.SerializeObject(product);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync("https://localhost:7126/api/Product/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData.Put("message", new AlertMessage()
                        {
                            Title = "Kayıt Güncellendi.",
                            Message = "Kayıt Güncellendi.",
                            AlertType = "success"
                        });

                        return RedirectToAction("ProductList");
                    }
                }

                /*
                var entity = _productService.GetById(model.ProductId);
                if (entity == null)
                {
                    NotFound();
                }
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;
                entity.Url = model.Url;
                entity.CategoryId = model.CategoryId;
                entity.ImageUrl = model.ExistingImage;

                entity.IsApproved = model.IsApproved;
                entity.IsHome = model.IsHome;

                if (file != null)
                {
                    var extentionUzanti = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extentionUzanti}");

                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                */

                //if (_productService.Update(entity, categoryIds))
                //{
                //    TempData.Put("message", new AlertMessage()
                //    {
                //        Title = "Kayıt Güncellendi.",
                //        Message = "Kayıt Güncellendi.",
                //        AlertType = "success"
                //    });
                //    return RedirectToAction("ProductList");
                //}

                /*
                _productService.Update(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Güncellendi.",
                    Message = "Kayıt Güncellendi.",
                    AlertType = "success"
                });

                return RedirectToAction("ProductList");
                */

                //TempData.Put("message", new AlertMessage()
                //{
                //    Title = "Hata",
                //    Message = _productService.ErrorMessage,
                //    AlertType = "danger"
                //});           
            }
            //ViewBag.Categories = _categoryService.GetAll();
            /*
            var values = _categoryService.GetAll();
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;
            */

            var categoryClient = _httpClientFactory.CreateClient();
            var categoryResponse = await categoryClient.GetAsync("https://localhost:7126/api/Category");
            var jsonCategoryData = await categoryResponse.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonCategoryData);
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.categories = category;

            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7126/api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Silindi.",
                    Message = $"{id} isimli ürün Silindi",
                    AlertType = "danger"
                });

                return RedirectToAction("ProductList");
            }

            return View();

            /*
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Kayıt Silindi.",
                Message = $"{entity.Name} isimli ürün Silindi",
                AlertType = "danger"

            });

            //var msg = new AlertMessage () {
            //    Message = $"{entity.Name} isimli ürün Silindi",
            //    AlertType = "danger"
            //};
            //TempData["Message"] = JsonConvert.SerializeObject (msg);

            return RedirectToAction("ProductList");
            */
        }

        public async Task<IActionResult> CategoryList()
        {
            /*
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
            */

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7126/api/Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet] 
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7126/api/Category", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt eklendi.",
                    Message = $"{model.Name} isimli Kategori eklendi",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }

            /*
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
                _categoryService.Create(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt eklendi.",
                    Message = $"{entity.Name} isimli Kategori eklendi",
                    AlertType = "success"

                });

                return RedirectToAction("CategoryList");
            }
            */
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7126/api/Category/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryViewModel>(jsonData);

                return View(values);
            }

            return View();

            /*
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
            };
            return View(model);
            */
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(UpdateCategoryViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7126/api/Category/", content);
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Güncellendi.",
                    Message = $"{model.Name} isimli Kategori güncellendi",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");
            }

            return View();

            /*
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.CategoryId);
                if (entity == null)
                {
                    NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Güncellendi.",
                    Message = $"{entity.Name} isimli Kategori güncellendi",
                    AlertType = "success"
                });

                //var msg = new AlertMessage () {
                //    Message = $"{entity.Name} isimli Kategori güncellendi",
                //    AlertType = "success"
                //};
                //TempData["Message"] = JsonConvert.SerializeObject (msg);

                return RedirectToAction("CategoryList");
            }
            //if (model.Products == null)
            //{
            //    model.Products = new List<Product>();
            //}
            return View(model);
            */
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7126/api/Category/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Silindi.",
                    Message = $"{id} numaralı Kategori Silindi",
                    AlertType = "danger"
                });

                return RedirectToAction("CategoryList");
            }

            return View();

            /*
            var entity = _categoryService.GetById(id);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Kayıt Silindi.",
                Message = $"{entity.Name} isimli Kategori Silindi",
                AlertType = "danger"
            });

            return RedirectToAction("CategoryList");
            */
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }  
    }
}