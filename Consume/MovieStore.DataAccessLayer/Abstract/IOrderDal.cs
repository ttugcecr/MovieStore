using MovieStore.EntityLayer.Concrete;

namespace MovieStore.DataAccessLayer.Abstract
{
    public interface IOrderDal: IGenericDAL<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
