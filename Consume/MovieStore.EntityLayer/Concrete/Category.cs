using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MovieStore.EntityLayer.Concrete
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<Product>? Products { get; set; }

        //public List<ProductCategory> ProductCategories { get; set; }
    }
}
