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
    public class EFCategoryDAL : GenericRepository<Category>, ICategoryDAL
    {

        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new Context())
            {
                var cmd = "Delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            //using (var context = new Context())
            //{
            //    return context.Categories
            //                  .Where(i => i.CategoryId == categoryId)
            //                  .Include(i => i.Products)
            //                  .ThenInclude(i => i.Product)
            //                  .FirstOrDefault();
            //}
            return null;
        }


    }
}