using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.Data;
using ProductInventoryApp.Models;

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

        // Get "Add Product" page
        [HttpGet]
        public IActionResult PostProduct()
        {
            return View();
        }

        // Add new product
        [HttpPost]
        public IActionResult PostProduct(PostProductModel postProductModel)
        {
            var product = new Product()
            {
                Name = postProductModel.Name,
                Category = postProductModel.Category,
                Price = postProductModel.Price,
                Quantity = postProductModel.Quantity,
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("GetProducts");
        }
    }
}
