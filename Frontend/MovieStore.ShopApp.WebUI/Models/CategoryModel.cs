﻿using Microsoft.Identity.Client;
using MovieStore.EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "Name", Prompt = "Enter category name")]
        [Required(ErrorMessage = "Name zorunlu bir alan")]
        public string Name { get; set; }

        [Display(Name = "Url", Prompt = "Enter category Url")]
        [Required(ErrorMessage = "Url zorunlu bir alan")]
        public string Url { get; set; }
        // public List<Product> Products { get; set; }

    }
}