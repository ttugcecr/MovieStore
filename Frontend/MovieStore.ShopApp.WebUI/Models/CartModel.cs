﻿namespace MovieStore.ShopApp.WebUI.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<CartItemModel> CartItems { get; set; }//cart  modelden cartItemlara ulasmak ıcın    public class CartItemModel eklenıyor asagıda

        public double TotalPrice()
        {
            return CartItems.Sum(i => i.Price * i.Quantity);
        }
    }

    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }


    }
}
