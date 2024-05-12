using ECommerceShoppingApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceShoppingApplication.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDBContext _db;

        public ProductsController(ApplicationDBContext db) { 
            _db = db;
        }

        public IActionResult Index()
        {
            LoadCategories();
            return View();
        }

        private void LoadCategories()
        {
            var categories = _db.Categories.Select(x => x).ToList();
            var categoriesList = new List<SelectListItem>();
            foreach (var cat in categories)
            {
                categoriesList.Add(
                    new SelectListItem { Value = cat.categoryId.ToString(), Text = cat.CategoryName.ToString() }
                );
            }
            ViewBag.Categories = categoriesList;
        }
    }
}
