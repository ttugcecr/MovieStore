using MovieStore.BusinessLayer.Abstract;
using MovieStore.DataAccessLayer.Abstract;
using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Create(Order entity)
        {
            _orderDal.Create(entity);
        }

        public void Delete(Order entity)
        {
            _orderDal.Delete(entity);
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderDal.GetById(id);
        }

        public List<Order> GetOrders(string userId)
        {
            return _orderDal.GetOrders(userId);
        }

        public void Update(Order entity)
        {
            _orderDal.Update(entity);
        }
    }
}