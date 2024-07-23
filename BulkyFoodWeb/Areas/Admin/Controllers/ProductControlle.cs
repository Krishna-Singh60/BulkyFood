using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork) // Adding/Active this service to program file
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        { 
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully !!";
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

            // Product? catfromdb1 = _db.Product.Find(id); //only work in Promary key
            Product? prodfromdb = _unitOfWork.Product.Get(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
                                                                            // Product? catfromdb2 = _db.Product.Where(u => u.Id == id).FirstOrDefault();//find speciifc then find firstOrdefault


            if (prodfromdb == null)
            {
                return NotFound();
            }
            return View(prodfromdb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                TempData["success"] = "Product updated successfully !!";
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

            // Product? catfromdb1 = _db.Product.Find(id); //only work in Promary key
            Product? prodfromdb = _unitOfWork.Product.Get(u => u.Id == id); //Not specific to primary other tabel as well like name/display order and all 
                                                                            // Product? catfromdb2 = _db.Product.Where(u => u.Id == id).FirstOrDefault();//find speciifc then find firstOrdefault


            if (prodfromdb == null)
            {
                return NotFound();
            }
            return View(prodfromdb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully !!";
            return RedirectToAction("Index");
        }
    }
}
