using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccessLayer.Abstract;
using MovieStore.DataAccessLayer.Concrete;
using MovieStore.DataAccessLayer.Repository;
using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccessLayer.EntityFramework
{
    public class EFOrderDal : GenericRepository<Order>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new Context())
            {
                var orders = context.Orders
                                  .Include(i => i.OrderItems)
                                  .ThenInclude(i => i.Product)
                                  .AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId);
                }
                return orders.ToList();
            }
        }
    }
}