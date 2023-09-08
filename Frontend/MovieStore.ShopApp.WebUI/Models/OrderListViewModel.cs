using MovieStore.EntityLayer.Concrete;

namespace MovieStore.ShopApp.WebUI.Models
{
    public class OrderListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Order> Orders { get; set; }
    }
}
