using Bookie.DataAcess.Data;
using Bookie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyFoodWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Category.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            // To test if both text value is same
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order can not be same");
            }

            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully !!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Category? catfromdb1 = _db.Category.Find(id); //only work in Promary key
            Category? catfromdb = _db.Category.FirstOrDefault(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
                                                                                // Category? catfromdb2 = _db.Category.Where(u => u.Id == id).FirstOrDefault();//find speciifc then find firstOrdefault


            if (catfromdb == null)
            {
                return NotFound();
            }
            return View(catfromdb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                TempData["success"] = "Category updated successfully !!";
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Category? catfromdb1 = _db.Category.Find(id); //only work in Promary key
            Category? catfromdb = _db.Category.FirstOrDefault(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
                                                                                // Category? catfromdb2 = _db.Category.Where(u => u.Id == id).FirstOrDefault();//find speciifc then find firstOrdefault


            if (catfromdb == null)
            {
                return NotFound();
            }
            return View(catfromdb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category category = _db.Category.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            _db.Category.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully !!";
            return RedirectToAction("Index");
        }

    }
}
