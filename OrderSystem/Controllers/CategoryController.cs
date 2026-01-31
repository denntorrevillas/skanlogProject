using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Entities;

namespace OrderSystem.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _AppContext;

        public CategoryController(AppDbContext context)
        {
            _AppContext = context;
        }

        // POST: category
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Category is required");

            await _AppContext.Categories.AddAsync(category);
            await _AppContext.SaveChangesAsync(); // ✅ Save first

            var allCat = await _AppContext.Categories.ToListAsync(); // ✅ Fetch after save

            return Ok(new
            {
                message = "Category Added!",
                allCat
            });
        }

        // GET: category
        [HttpGet]
        public async Task<IActionResult> ViewCategories()
        {
            var categories = await _AppContext.Categories
     .Include(c => c.Products)
     .ToListAsync();

            return Ok(
                new
                {
                    categories,
                    count = categories.Count
                });
        }

        [HttpDelete("{categoryID}")]

        public async Task <IActionResult> DeleteCategory(int categoryID)
        {
           
            var catDelete = await _AppContext.Categories
            .FirstOrDefaultAsync(c => c.CategoryID == categoryID);

        

                    if (catDelete == null)
                    {
                        return BadRequest(new { message = "Category not found" });
                    }
                    _AppContext.Categories.Remove(catDelete);
                    await _AppContext.SaveChangesAsync();

            var allCat = _AppContext.Categories.ToListAsync();

            return Ok(
                new
                {
                    message = "Category Deleted!",
                    allCat
                });

            
        }

        [HttpPut("{categoryID}")]

        public async Task<IActionResult> updateCat(int categoryID, Category updateCat)
        {
            var catUpdate= await _AppContext.Categories.FirstOrDefaultAsync(c=>c.CategoryID == categoryID);

           if(catUpdate== null)
            {
                return NotFound("Category Not Found"
                    );
            }

            catUpdate.CategoryName = updateCat.CategoryName;

            await _AppContext.SaveChangesAsync();
            return Ok(new
            {
                message ="Category Updated",
                catUpdate
            });

        }

        [HttpGet("{categoryID}")]

        public async Task<IActionResult> searchCat(int  categoryID)
        {
            var searchCat = await _AppContext.Categories.FirstOrDefaultAsync(c=>c.CategoryID==categoryID);
            if (searchCat == null)
            {
                return BadRequest("Invalid Category");
            }

            return Ok(
                new
                {
                    message = "Category Searched",
                    Category = searchCat
                }
                );
        }
    }
}
