using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieStore.BusinessLayer.Abstract;
using MovieStore.EntityLayer.Concrete;
using MovieStore.ShopApp.WebUI.Extensions;
using MovieStore.ShopApp.WebUI.FakePaymentService;
using MovieStore.ShopApp.WebUI.Models;
using Newtonsoft.Json;


namespace MovieStore.ShopApp.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<AppUser> _userManager;
        private IOrderService _orderService;
        private readonly IFakePaymentService _fakePaymentService;

        public CartController(ICartService cartService, UserManager<AppUser> userManager, IOrderService orderService, IFakePaymentService fakePaymentService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
            _fakePaymentService = fakePaymentService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            if (cart != null)
            {
                return View(new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.ProductId,
                        Name = i.Product.Name,
                        Price = (double)i.Product.Price,
                        ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                });
            }
            else
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Bilgilendirme",
                    Message = "Onayınız Admin Tarafınfan Onaylandığı için Kart tanımı yapılmamış",
                    AlertType = "danger"

                });

                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(userId, productId, quantity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, productId);

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();
            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity

                }).ToList()
            };

            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel model)
        {
            var userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userId);

            model.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity

                }).ToList()
            };

            var fakePaymentInfoInput = new FakePaymentInfoInput()
            {
                CardName = model.CardName,
                CardNumber = model.CardNumber,
                ExpirationMonth = model.ExpirationMonth,
                ExpirationYear = model.ExpirationYear,
                CVV = model.Cvc,
                TotalPrice = model.CartModel.TotalPrice()
            };

            var payment = await _fakePaymentService.ReceivePayment(fakePaymentInfoInput);            

            if (payment)
            {
                SaveOrder(model, userId);
                ClearCart(model.CartModel.CartId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var msg = new AlertMessage()
                {
                    Message = $"{payment}"+"Ödeme Başarısız",
                    AlertType = "danger"
                };

                TempData["Message"] = JsonConvert.SerializeObject(msg);
            }

            return View(model);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private void SaveOrder(OrderModel model, string userId)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.completed;
            order.PaymentType = EnumPaymentType.CreditCart;

            order.PaymentId = "123";
            order.ConversationId = "456";

            order.OrderDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            order.UserId = userId;
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Address = model.Address;
            order.Phone = model.Phone;
            order.Email = model.Email;
            order.City = model.City;
            order.Note = model.Note;

            order.OrderItems = new List<EntityLayer.Concrete.OrderItem>();
            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new EntityLayer.Concrete.OrderItem()
                {
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                order.OrderItems.Add(orderItem);
            }

            _orderService.Create(order);
        }
    }
}
