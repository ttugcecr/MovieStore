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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }
        public void Create(Category entity)
        {
            _categoryDAL.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryDAL.Delete(entity);
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            _categoryDAL.DeleteFromCategory(productId, categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryDAL.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryDAL.GetById(id);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return _categoryDAL.GetByIdWithProducts(categoryId);
        }

        public void Update(Category entity)
        {
            _categoryDAL.Update(entity);
        }
        public string ErrorMessage { get; set; }

        public bool Validation(Category entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
