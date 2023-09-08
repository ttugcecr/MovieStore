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
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public bool Create(Product entity)
        {
            //iş kurulları uygulanablır
            if (Validation(entity))
            {
                _productDAL.Create(entity);
                return true;
            }
            return false;


        }

        public void Delete(Product entity)
        {
            _productDAL.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDAL.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDAL.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productDAL.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDAL.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productDAL.GetHomePageProducts();
        }

        public Product GetProductDetails(string url)
        {
            return _productDAL.GetProductDetails(url);
        }

        public List<Product> GetProductsByCategory(string name, int page, int pagesize)
        {
            return _productDAL.GetProductsByCategory(name, page, pagesize);
        }

        public List<Product> GetSearchResut(string searchString)
        {
            return _productDAL.GetSearchResut(searchString);
        }

        public void Update(Product entity)
        {
            _productDAL.Update(entity);
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            if (Validation(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "film için en az bir kategori seçmelisiniz";
                    return false;
                }
                _productDAL.Update(entity, categoryIds);
                return true;
            }
            return false;


        }

        public string ErrorMessage { get; set; }
        public bool Validation(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "film ismi girmelisiniz.\n";
                isValid = false;
            }
            if (entity.Price < 0)
            {
                ErrorMessage += "film Fiyatı negatif olamaz.\n";
                isValid = false;
            }
            return isValid;

        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDAL.GetProductsByCategory(categoryId);
        }
    }
}