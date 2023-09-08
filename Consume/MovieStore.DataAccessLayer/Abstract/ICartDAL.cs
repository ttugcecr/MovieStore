using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccessLayer.Abstract
{
    public interface ICartDAL : IGenericDAL<Cart>
    {
        Cart GetByUserId(string userId);
        void DeleteFromCart(int CartId, int productId);
        void ClearCart(int cartId);
    }
}
