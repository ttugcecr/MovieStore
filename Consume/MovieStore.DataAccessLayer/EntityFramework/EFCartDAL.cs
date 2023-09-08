using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccessLayer.Abstract;
using MovieStore.DataAccessLayer.Concrete;
using MovieStore.DataAccessLayer.Repository;
using MovieStore.EntityLayer.Concrete;

namespace MovieStore.DataAccessLayer.EntityFramework
{
    public class EFCartDAL : GenericRepository<Cart>, ICartDAL
    {
        public void ClearCart(int cartId)
        {
            using (var context = new Context())
            {
                var cmd = @"Delete from CartItems where CartId=@p0";
                context.Database.ExecuteSqlRaw(cmd, cartId);
            }
        }

        public void DeleteFromCart(int CartId, int productId)
        {
            using (var context = new Context())
            {
                var cmd = @"Delete from CartItems where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, CartId, productId);
            }
        }

        public Cart GetByUserId(string userId)
        {
            using (var context = new Context())
            {
                return context.Carts
                              .Include(i => i.CartItems)
                              .ThenInclude(i => i.Product)
                              .FirstOrDefault(i => i.UserId == userId); ;
            }

        }

        public override void Update(Cart entity)
        {
            using (var context = new Context())
            {

                context.Carts.Update(entity);
                context.SaveChanges();

            }
        }
    }
}
