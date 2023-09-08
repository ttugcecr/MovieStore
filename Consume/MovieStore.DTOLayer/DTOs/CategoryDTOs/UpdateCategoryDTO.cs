namespace MovieStore.DTOLayer.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}