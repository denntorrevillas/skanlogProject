using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Entities;
using System.Net.Http.Headers;

namespace OrderSystem.Controllers
{

    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly AppDbContext _AppContext;

        public ProductController(AppDbContext context)
        {
            _AppContext = context;
        }

        [HttpPost("{categoryID}")]

        public async Task<IActionResult> addProduct(int categoryID, Product product)
        {
            var catID = await _AppContext.Categories.FirstOrDefaultAsync(cat => cat.CategoryID == categoryID);

            if (catID == null)
            {
                return NotFound(
                    new
                    {
                        message = "Invalid Category!"
                    });
            }
            product.CategoryID = categoryID;

            _AppContext.Products.Add(product);
            await _AppContext.SaveChangesAsync();

            var updateProd = await _AppContext.Products.ToListAsync();

            return Ok(
                new
                {
                    message = "Product Added!",
                    AlProducts = updateProd
                }
                );

        }

        [HttpGet]
        public async Task<IActionResult> viewProds()
        {
            var allProd = await _AppContext.Products.ToListAsync();
            return Ok(
                new
                {
                    message ="All Products",
                    AllProducts = allProd
                }
                );
        }


        [HttpPut("{productID}")]

        public async Task<IActionResult> updateProd(int productID, Product updateProd)
        {

            var findProd = await _AppContext.Products.FirstAsync(p => p.ProductID == productID);

            if (findProd == null)
            {
                return NotFound("No Product Found");

            }

   
            findProd.ProductName = updateProd.ProductName;
            findProd.price = updateProd.price;
            findProd.stock= updateProd.stock;

            await _AppContext.SaveChangesAsync();

            return Ok(
                new
                {
                    message = "Produdt Updated",
                    UpdateProduct = _AppContext.Products.ToListAsync()
                }
                );


        }

        [HttpDelete("productID")]

        public async  Task<IActionResult>  delProd(int productID)
        {
            var delProd = await _AppContext.Products.FirstOrDefaultAsync(p=> p.ProductID == productID);

            if(delProd == null)
            {
                return NotFound("Invalid Product");
            }

            _AppContext.Products.Remove(delProd);
            await _AppContext.SaveChangesAsync();

            return Ok(
                new
                {
                    message ="Product Deleted",
                    UpdateProduct = _AppContext.Products.ToListAsync()
                }
                );
            

        }
    }
}
