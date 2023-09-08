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
    public class EFProductDAL : GenericRepository<Product>, IProductDAL
    {
        public Product GetByIdWithCategories(int id)
        {
            //using (var context = new Context())
            //{
            //    return context.Products
            //                   .Where(p => p.ProductId == id)
            //                   .Include(i => i.ProductCategories)
            //                   .ThenInclude(i => i.Category)
            //                   .FirstOrDefault();
            //}
            return null;
        }

        public int GetCountByCategory(string category)
        {
            //using (var context = new Context())
            //{
            //    var urunler = context.Products.Where(i => i.IsApproved).AsQueryable();  //sorgu calısmadıgı ıcın bekletıyor
            //    if (!string.IsNullOrEmpty(category))
            //    {
            //        urunler = urunler.Include(i => i.ProductCategories)
            //                       .ThenInclude(i => i.Category)
            //                       .Where(i => i.ProductCategories.Any(a => a.Category.Url == category));

            //    }
            //    return urunler.Count();
            //}
            return 1;
        }

        public List<Product> GetHomePageProducts()
        {
            using (var context = new Context())
            {
                return context.Products
                              .Where(i => i.IsApproved == true && i.IsHome == true)
                              .ToList();
            }

        }



        public Product GetProductDetails(string url)
        {
            using (var context = new Context())
            {
                return context.Products.Where(p => p.Url == url).Include(x=>x.Category).FirstOrDefault();                         
                          
            }
            return null;
        }



        public List<Product> GetProductsByCategory(string name, int page, int pagesize)
        {
            //using (var context = new Context())
            //{
            //    var urunler = context.Products
            //                        .Where(i => i.IsApproved)
            //                        .AsQueryable();  //sorgu calısmadıgı ıcın bekletıyor
            //    if (!string.IsNullOrEmpty(name))
            //    {
            //        urunler = urunler.Include(i => i.ProductCategories)
            //                       .ThenInclude(i => i.Category)
            //                       .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));

            //    }
            //    return urunler.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            //}
            return null;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            using (var context = new Context())
            {
                return context.Products
                              .Where(i => i.CategoryId == categoryId && i.IsApproved == true && i.IsHome == true)
                              .ToList();
            }
        }

        public List<Product> GetSearchResut(string searchString)
        {
            using (var context = new Context())
            {
                var urunler = context.Products
                                    .Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())))
                                    .AsQueryable();  //sorgu calısmadıgı ıcın bekletıyor

                return urunler.ToList();
            }

        }

        public void Update(Product entity, int[] categoryIds)
        {

            //using (var context = new Context())
            //{
            //    var products = context.Products
            //                         .Include(i => i.ProductCategories)
            //                         .FirstOrDefault(i => i.ProductId == entity.ProductId);

            //    if (products != null)
            //    {
            //        products.Name = entity.Name;
            //        products.Description = entity.Description;
            //        products.Price = entity.Price;
            //        products.Url = entity.Url;
            //        products.ImageUrl = entity.ImageUrl;
            //        products.IsApproved = entity.IsApproved;
            //        products.IsHome = entity.IsHome;

            //        products.ProductCategories = categoryIds.Select(catid => new ProductCategory()
            //        {
            //            ProductId = entity.ProductId,
            //            CategoryId = catid

            //        }).ToList();
            //        context.SaveChanges();
            //    }
            //}
        }
    }
}