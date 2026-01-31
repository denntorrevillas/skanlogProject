using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using OrderSystem.Entities;

namespace OrderSystem.Controllers
{

    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<IActionResult> addCustomer([FromBody] Customer customer)
        {
            await _context.AddAsync(customer);
            await  _context.SaveChangesAsync();

            var allCustomer = await _context.Customers.ToListAsync();
;
            return(Ok(
                new
                {
                    message="Customer Added!",
                    AllCustomer = allCustomer
                }   
                ));
        }

        [HttpGet("{CustomerID}")]

        public async Task<IActionResult> searchCustomer(int CustomerID)
        {
            var  viewCustomer = await _context.Customers.FirstOrDefaultAsync(c=> c.CustomerID
            == CustomerID);

            if (viewCustomer == null)
            {
                return NotFound("Invalid Customer");

            }

            return (Ok(
                new
                {
                    message = $"Customer ID: {CustomerID} Info",
                    viewCustomer
                }
                ));
        }

        [HttpPut("CustomerID")]

        public async Task<IActionResult> updateCustomer(int CustomerID, Customer customer)
        {
            var updateCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == CustomerID);

            if (updateCustomer == null)
            {
                return NotFound("Invalid customer");
            }
            updateCustomer.CustomerName = customer.CustomerName;
            updateCustomer.CustomerPhone = customer.CustomerPhone;
            updateCustomer.CustomerEmail = customer.CustomerEmail;

            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message = $"Customer {CustomerID} updated",
                    CurrentList = await _context.Customers.ToListAsync()
                }
                );

        }

        [HttpDelete("CustomerID")]

        public async Task<IActionResult> deleteCustomer(int CustomerID)
        {
            var delCus = await _context.Customers.FirstOrDefaultAsync(c=>c.CustomerID == CustomerID);
            
            if(delCus == null)
            {
                return BadRequest("invaid cusomer");
            }

            _context.Customers.Remove(delCus);
            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message =$"Customer {delCus.CustomerName} Deleted!",
                    UpdatedList = await _context.Customers.ToListAsync()
                }
                );
        }



    }
}
