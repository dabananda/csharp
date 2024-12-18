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

        // Get the edit product form
        [HttpGet]
        public IActionResult PutProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                var updatedProduct = new PutProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    Price = product.Price,
                    Quantity = product.Quantity,
                };
                return View(updatedProduct);
            }
            return View(null);
        }

        // Update the product
        [HttpPost]
        public IActionResult PutProduct(PutProductModel putProductModel)
        {
            var existingProduct = _context.Products.Find(putProductModel.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = putProductModel.Name;
                existingProduct.Category = putProductModel.Category;
                existingProduct.Price = putProductModel.Price;
                existingProduct.Quantity = putProductModel.Quantity;
                _context.SaveChanges();
            }
            return RedirectToAction("GetProducts");
        }
    }
}
