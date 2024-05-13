using ECommerceShoppingApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var products = _db.Products.Include("Category").Select(x=>x);
            return View(products);
        }
    }
}
