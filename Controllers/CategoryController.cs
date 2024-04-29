using ECommerceShoppingApplication.Data;
using ECommerceShoppingApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceShoppingApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private ApplicationDBContext db;
        public CategoryController(ApplicationDBContext _db) { 
            db = _db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories =
                db.Categories
                .OrderBy(c => c.DisplayOrder)
                .OrderBy(c => c.CategoryName)
                .Select(x => x);
            return View(categories);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["Success"] = "Record(s) created successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit (int? id) 
        {
            if (id == 0 || id == null) {
                return NotFound();
            }
            var category = db.Categories
                .Where(x => x.categoryId == id)
                .FirstOrDefault();
            if (category == null) {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category) {
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                db.SaveChanges();
                TempData["Success"] = "Record(s) updated successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var category = db.Categories
                .Where(x => x.categoryId == id)
                .FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var category = db.Categories
                .Where(x => x.categoryId == id)
                .FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            db.Remove(category);
            db.SaveChanges();
            TempData["Success"] = "Record(s) deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
