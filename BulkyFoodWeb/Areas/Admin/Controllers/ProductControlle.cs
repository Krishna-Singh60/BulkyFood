using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models;
using Bookie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //Injection webHost we can use WWWroot folder

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) // Adding/Active this service to program file
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.Product.GetAll(includeproperties: "Category").ToList();
            return View(productsList);
        }

        public IActionResult Upsert(int? id)
        {
            // pass addtional funtionality
            //Each item of Category we can be select using Projection in EF Core
            // IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.
            //   GetAll().ToList().Select(u => new SelectListItem
            //   {
            //       Text = u.Name,
            //     Value = u.Id.ToString()
            //  });
            //View_Bag use
            // ViewBag.CategoryList = categoryList;
            //View_Data Use  // Instead of that we are creating View Model and use it in Upsert View Using @Model 
            // ViewData["CategoryList"] = categoryList;
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)  // We are creating Product
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Update        // We are Updating Product
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string sourcePath = @"Images\product\";
                //Uploading the the file
                string wwwRootpath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string prodPath = Path.Combine(wwwRootpath, sourcePath);
                    if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                    {
                        //Delete Old image
                        var oldImagePath = Path.Combine(wwwRootpath, productVm.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(prodPath, fileName), FileMode.Create)) // Use fileStream to read file and store
                    {
                        file.CopyTo(fileStream);
                    }
                    // Store the file in the path 
                    productVm.Product.ImageUrl = sourcePath + fileName;

                }
                if (productVm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                    TempData["success"] = "Product created successfully !!";
                }
                else
                {
                    _unitOfWork.Product.Update(productVm.Product);
                    TempData["success"] = "Product updated successfully !!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVm.CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVm);
            }
        }

        /*  public IActionResult Edit(int? id)
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
          public IActionResult Edit(ProductVM obj)
          {
              if (ModelState.IsValid)
              {
                  _unitOfWork.Product.Update(obj.Product);
                  TempData["success"] = "Product updated successfully !!";
                  _unitOfWork.Save();
                  return RedirectToAction("Index");
              }
              return View();
          }
        */

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

        /* #region APICALLS
         [HttpGet]
         public IActionResult GetAll()
         {
             List<Product> objprod = _unitOfWork.Product.GetAll(includeproperties: "Category").ToList();
             return Json (new {data = objprod });
         }

         #endregion
     */
    }
}
