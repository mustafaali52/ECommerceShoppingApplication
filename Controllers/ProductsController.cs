using ECommerceShoppingApplication.Data;
using ECommerceShoppingApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Create() {
            LoadCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Products product)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            _db.Products.Add(product);
            _db.SaveChanges();
            TempData["Success"] = "Product(s) added successfully!";
            return RedirectToAction("Index");
        }

        private void LoadCategories()
        {
            var categories = _db.Categories.Select(x=>x).ToList();
            var categoryList = new List<SelectListItem>();
            foreach (var category in categories) {
                categoryList.Add(
                        new SelectListItem
                        {
                            Value = category.categoryId.ToString()
                        ,
                            Text = category.CategoryName
                        }
                    );
            }
            ViewBag.Categories = categoryList;  
        }
    
    }
}
