using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.Data;

namespace ProductInventoryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
