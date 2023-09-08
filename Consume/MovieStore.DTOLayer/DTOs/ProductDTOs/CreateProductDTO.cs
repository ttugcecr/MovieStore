﻿namespace MovieStore.DTOLayer.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public double? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }

        public int CategoryId { get; set; }
    }
}