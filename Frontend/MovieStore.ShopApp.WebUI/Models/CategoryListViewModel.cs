using MovieStore.EntityLayer.Concrete;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class CategoryListViewModel
    {
        //public List<Category> Categories { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}