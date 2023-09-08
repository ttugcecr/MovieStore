namespace MovieStore.DTOLayer.DTOs.ProductDTOs
{
    public class ResultProjectDTO
    {
        public int ProductID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FullText { get; set; }
        public string? ISBN { get; set; }
        public string? Director { get; set; }
        public int ReleaseYear { get; set; }
        public int Length { get; set; }
        public double Rating { get; set; }
        public double ListPrice { get; set; }
        public double Price { get; set; }
        public double Price50 { get; set; }
        public double Price100 { get; set; }
        public string? ImageURL { get; set; }

        public int CategoryID { get; set; }
    }
}
