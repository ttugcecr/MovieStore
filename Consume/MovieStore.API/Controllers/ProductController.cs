using Microsoft.AspNetCore.Mvc;
using MovieStore.BusinessLayer.Abstract;
using MovieStore.DTOLayer.DTOs.ProductDTOs;
using MovieStore.EntityLayer.Concrete;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult ListProduct()
        {
            var values = _productService.GetAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddProduct(CreateProductDTO createProductDTO)
        {
            Product product = new Product()
            {
                Name = createProductDTO.Name,
                Url = createProductDTO.Url,
                Price = createProductDTO.Price,
                ImageUrl = createProductDTO.ImageUrl,
                Description = createProductDTO.Description,
                IsApproved = createProductDTO.IsApproved,
                IsHome = createProductDTO.IsHome,
                CategoryId = createProductDTO.CategoryId,
            };
            _productService.Create(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            Product product = new Product()
            {
                ProductId = updateProductDTO.ProductId,
                Name = updateProductDTO.Name,
                Url = updateProductDTO.Url,
                Price = updateProductDTO.Price,
                ImageUrl = updateProductDTO.ImageUrl,
                Description = updateProductDTO.Description,
                IsApproved = updateProductDTO.IsApproved,
                IsHome = updateProductDTO.IsHome,
                CategoryId = updateProductDTO.CategoryId,
            };
            _productService.Update(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var values = _productService.GetById(id);
            _productService.Delete(values);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var values = _productService.GetById(id);
            return Ok(values);
        }
    }
}
