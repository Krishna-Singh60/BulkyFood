using BulkyFoodWeb.Data;
using BulkyFoodWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyFoodWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
                _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Category.ToList();
            return View(objCategoryList);
        }
    }
}
