using FluentValidation;
using MovieStore.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLayer.Abstract
{
    public interface ICategoryService : IValidator<Category>
    {
        Category GetById(int id);
        Category GetByIdWithProducts(int categoryId);

        List<Category> GetAll();

        void Create(Category entity);

        void Update(Category entity);

        void Delete(Category entity);
        void DeleteFromCategory(int productId, int categoryId);
    }
}