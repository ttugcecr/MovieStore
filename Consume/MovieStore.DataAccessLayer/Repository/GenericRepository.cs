using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccessLayer.Abstract;
using MovieStore.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccessLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericDAL<TEntity> where TEntity : class
    {
        public void Create(TEntity entity)
        {
            using (var context = new Context())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new Context())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public List<TEntity> GetAll()
        {
            using (var context = new Context())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new Context())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        //virtual yaparak bu metotdu overide edebılıyurz ezıyorz yanı burdan efcorecartreposıtorde ezıyoruz
        public virtual void Update(TEntity entity)

        {
            using (var context = new Context())
            {
                context.Entry(entity).State = EntityState.Modified;//burda form uzerınde gucelleme yaptıgımızda hangı alanlar degıstıgını otomatık anlıyor ve ona gore degısen kısım ıcın guncelleme yapıyor
                context.SaveChanges();
            }
        }
    }
}
