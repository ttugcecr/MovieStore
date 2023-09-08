using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccessLayer.Abstract
{
    public interface IProductDAL : IGenericDAL<Product>
    {
        Product GetProductDetails(string url);
        Product GetByIdWithCategories(int id);
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        List<Product> GetSearchResut(string searchString);

        List<Product> GetHomePageProducts();

        int GetCountByCategory(string category);
        void Update(Product entity, int[] categoryIds);
        List<Product> GetProductsByCategory(int categoryId);
    }
}