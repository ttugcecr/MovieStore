using MovieStore.EntityLayer.Concrete;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
    }
}