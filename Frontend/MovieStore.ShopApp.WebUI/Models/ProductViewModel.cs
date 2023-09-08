﻿using MovieStore.EntityLayer.Concrete;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }
        public int TotalPage()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);//10:3 gıbı bır an geldıgınde sayfayı yuvarlayack
        }
    }

    public class ProductListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }
    }
}