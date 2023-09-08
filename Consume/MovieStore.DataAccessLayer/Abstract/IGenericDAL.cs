using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccessLayer.Abstract
{
    public interface IGenericDAL<T> where T : class
    {
        T GetById(int id);

        List<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
