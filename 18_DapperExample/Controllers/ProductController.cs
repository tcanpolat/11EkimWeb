using _18_DapperExample.Data;
using _18_DapperExample.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace _18_DapperExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly DapperContext _context;

        public ProductController(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // ürünleri ve kategori adlarını getiren sql sorgusu
            var query = "select * from Product join Category on Category.CategoryId = Product.CategoryId";
            using (var connection = _context.CreateConnection())
            {
                // Çoklu tablo sorgusu
                var products = await connection.QueryAsync
                    <Product, Category, Product>
                (
                    query,
                    (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "CategoryId" // Kategorilere göre ayırma

                );

                return View(products.ToList());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        // Yei ürün ekleme
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var query = "INSERT INTO Product (Name, Price, CategoryId) " +
                        "VALUES (@Name, @Price, @CategoryId)";

            using (var connection = _context.CreateConnection())
            {
                // Dapper ile ürün ekleme işlemi
                await connection.ExecuteAsync(query,product);
                return RedirectToAction("Index");

            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var query = "SELECT * FROM Product WHERE ProductId = @Id";

            using (var connection = _context.CreateConnection())
            {
                // Dapper ile ürün sorgulama işlemi
                var product = await connection.QuerySingleOrDefaultAsync
                    <Product>
                    (query, new { Id = id });
                if(product is null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        // Edit işlemi post metodu
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var query = "update Product set Name = @Name,Price = @Price, CategoryId = @CategoryId where ProductId = @ProductId";
            using (var connection = _context.CreateConnection())
            {
                // Dapper ile ürün güncelleme işlemi
                await connection.ExecuteAsync(query, product);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var query = "select * from Product where ProductId = @Id";

            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync
                    <Product>
                    (query, new { Id = id });
                if (product is null)
                {
                   return NotFound();
                }

                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var query = "select * from Product where ProductId = @Id";

            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync
                    <Product>
                    (query, new { Id = id });
                if (product is null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var query = "delete from Product where ProductId = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result =  await connection.ExecuteAsync(query, new { Id = id });

                if(result > 0)
                {
                    ViewBag.Message = "Product deleted Successful";
                }
                else
                {
                   ViewBag.Message = "Product delete failed";
                }

                return View("DeleteResult");
            }
        }
    }
}
