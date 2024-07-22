using Bookie.DataAccess.Repository.IRepository;
using Bookie.DataAcess.Data;
using Bookie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork) // Adding/Active this service to program file
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
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
            Category? catfromdb = _unitOfWork.Category.Get(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
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
                _unitOfWork.Category.Update(category);
                TempData["success"] = "Category updated successfully !!";
                _unitOfWork.Save();
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
            Category? catfromdb = _unitOfWork.Category.Get(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
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
            Category category = _unitOfWork.Category.Get(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully !!";
            return RedirectToAction("Index");
        }

    }
}
