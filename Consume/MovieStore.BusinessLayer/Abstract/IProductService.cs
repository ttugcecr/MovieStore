using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLayer.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        Product GetById(int id);
        Product GetByIdWithCategories(int id);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name, int page, int pagesize);

        List<Product> GetAll();

        bool Create(Product entity);

        void Update(Product entity);

        void Delete(Product entity);
        int GetCountByCategory(string category);
        List<Product> GetHomePageProducts();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetSearchResut(string searchString);
        bool Update(Product entity, int[] categoryIds);
    }
}

