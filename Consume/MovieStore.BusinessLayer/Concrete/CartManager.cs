using MovieStore.BusinessLayer.Abstract;
using MovieStore.DataAccessLayer.Abstract;
using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDAL _cartDAL;

        public CartManager(ICartDAL cartDAL)
        {
            _cartDAL = cartDAL;
        }
        //------------------------------------------------------------------
        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);//userıd uzerınden  cart varmı yokmu bakılıyor
            if (cart != null)//kart varsa
            {
                //eklenmek istenilen ürün seppette varmı (güncelleme )
                //eklenmek istenilen ürün sepette yok ama yeni kayıt oluştur(kayıt ekleme)

                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);//karta ürün varmı
                if (index < 0)//o ürün yok,yeni  üründür eklenebılır 
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id


                    });
                }
                else//ürün varsada aynı ürünü ekler mikarınıartırız
                {
                    cart.CartItems[index].Quantity = cart.CartItems[index].Quantity + quantity;
                }



                _cartDAL.Update(cart);//cart update edıyoruz



            }

        }

        public void ClearCart(int CartId)
        {
            _cartDAL.ClearCart(CartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _cartDAL.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDAL.GetByUserId(userId);
        }



        // depensinh ınjection yapılıyor ********************
        public void InitializeCart(string userId)
        {
            _cartDAL.Create(new Cart()
            {
                UserId = userId
            });
        }
    }
}