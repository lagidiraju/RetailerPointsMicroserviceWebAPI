using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicesWebAPI.Data;
using MicroservicesWebAPI.Constants;

namespace MicroservicesWebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MicroservicesWebAPIContext _context;

        public CustomersController(MicroservicesWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            var customers = await _context.Customer.ToListAsync();
            var transactions = await _context.Transaction.ToListAsync();
            foreach(Customer customer in customers)
            {
                var total = transactions.Where(item => item.CustomerId == customer.CustomerId).Sum(item => item.Amount);
                CalculatePoints(customer, total);
            }

            return customers;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            var transactions = await _context.Transaction.ToListAsync();
            var total = transactions.Where(item => item.CustomerId == customer.CustomerId).Sum(item => item.Amount);
            CalculatePoints(customer, total);
            return customer;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }

        private static void CalculatePoints(Customer customer, double? totalAmount)
        {
            if (totalAmount != null)
                
                customer.Points = Convert.ToInt32(((totalAmount - Constant.FIRST_REWARD_LIMIT) * 1) + ((totalAmount - Constant.SECOND_REWARD_LIMIT) * 1));
        }
    }
}
