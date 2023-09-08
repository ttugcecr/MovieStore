using MovieStore.EntityLayer.Concrete;

namespace MovieStore.BusinessLayer.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        void Update(Order entity);
        void Delete(Order entity);
        Order GetById(int id);
        List<Order> GetAll();
        List<Order> GetOrders(string userId);
    }
}
