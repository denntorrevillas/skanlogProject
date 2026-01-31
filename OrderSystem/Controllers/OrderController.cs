using Microsoft.AspNetCore.Mvc;
using OrderSystem.Data;
using OrderSystem.Entities;
using OrderSystem.Migrations;

namespace OrderSystem.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController:ControllerBase
    {
       public readonly AppDbContext _Order;

       public OrderController(AppDbContext orderContext)
        {
            _Order = orderContext;
        }

        [HttpPost("customer/{CustomerID}")]

        public async Task<IActionResult>addOrder(int CustomerID, Order order)
        {
            if(order == null)
            {
                return NotFound("Invalid Order");
            }
               _Order.Orders.Add(order);
             await _Order.SaveChangesAsync();

            return Ok(
                new
                {
                    message = "Order Added!",
                    OrderList = _Order.Orders.ToList()
                }
               );
        }

        [HttpGet]

        public async Task<IActionResult> viewOrders()
        {
            return Ok(_Order.Orders.ToList());
        }

        
    }
}
