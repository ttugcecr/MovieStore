using Microsoft.AspNetCore.Mvc;
using MovieStore.BusinessLayer.Abstract;
using MovieStore.DTOLayer.DTOs.CategoryDTOs;
using MovieStore.EntityLayer.Concrete;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult ListCategory()
        {
            var values = _categoryService.GetAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddCategory(ResultCategoryDTO resultCategoryDTO)
        {
            Category category = new Category()
            {
                Name = resultCategoryDTO.Name,
                Url = resultCategoryDTO.Url,
            };
            _categoryService.Create(category);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            Category category = new Category()
            {
                CategoryId = updateCategoryDTO.CategoryId,
                Name = updateCategoryDTO.Name,
                Url = updateCategoryDTO.Url
            };
            _categoryService.Update(category);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var values = _categoryService.GetById(id);
            return Ok(values);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.GetById(id);
            _categoryService.Delete(value);
            return Ok();
        }
    }
}