﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.EntityLayer.Concrete
{
    public class CartItem
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}