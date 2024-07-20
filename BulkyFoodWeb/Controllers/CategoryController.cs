using Bookie.DataAccess.Repository.IRepository;
using Bookie.DataAcess.Data;
using Bookie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyFoodWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo) // Adding/Active this service to program file
        {
           _categoryRepo = categoryRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(category);
               _categoryRepo.Save();
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
            Category? catfromdb = _categoryRepo.Get(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
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
              _categoryRepo.Update(category);
                TempData["success"] = "Category updated successfully !!";
                _categoryRepo.Save();
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
            Category? catfromdb = _categoryRepo.Get(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
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
            Category category = _categoryRepo.Get(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully !!";
            return RedirectToAction("Index");
        }

    }
}
