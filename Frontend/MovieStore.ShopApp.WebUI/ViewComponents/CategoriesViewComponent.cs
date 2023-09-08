using Microsoft.AspNetCore.Mvc;
using MovieStore.BusinessLayer.Abstract;

namespace MovieStore.ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //----------------------------------------------------
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["category"] != null)
            {
                ViewBag.SecilenId = RouteData?.Values["category"];
            }
            if (RouteData.Values["action"] != null && RouteData.Values["category"] == null)
            {
                ViewBag.SecilenIdYok = 1;
            }

            return View(_categoryService.GetAll());

        }
    }
}