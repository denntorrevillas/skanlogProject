using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Entities;

namespace OrderSystem.Controllers
{

    [Route("orderitem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        public readonly AppDbContext _appDbContext;




        public OrderItemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("{OrderID}/product/{ProductID}")]

        public async Task<IActionResult> addProductOrder(int ProductID, int OrderID, Order order)
        {
            var orderFind = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.orderId == OrderID);
            if (orderFind == null)
            {
                return BadRequest("Order Not Found");
            }

            var productFind = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductID == ProductID);
            if (productFind == null)
            {
                return
                    BadRequest("Invalid Product!");
            }

            _appDbContext.Products.Add(productFind);
            await _appDbContext.SaveChangesAsync();

            return Ok(new
            {
                message="Item added to Order",
                OrderItemsist = await _appDbContext.OrderItems.ToListAsync()
            });


        }
    }

   
}
