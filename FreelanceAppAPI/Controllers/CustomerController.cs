using FreelanceAppAPI.Context;
using FreelanceAppAPI.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public CustomerController(
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }    

    #region Get Methods
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
    public async Task<ActionResult<Customer>> GetCustomerList(){

        return Ok(await _context.Customes.ToListAsync());
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
    public async Task<ActionResult<Customer>> GetCustomerById(Guid id){

        var customer = await _context.Customes.FindAsync(id);
        if (customer != null) {
            return Ok(customer);
        }else {
            
            return BadRequest("Cliente incorrecto");
        }
    }
    #endregion

    #region Post method
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
    public async Task<ActionResult> PostCustomer([FromBody] Customer customerData){

        var customer = new Customer() {
            CutomerId = new Guid(),
            CutomerName = customerData.CutomerName,
            CustomerAddress= customerData.CustomerAddress,
            CustomerEmail = customerData.CustomerEmail,
            CustomerPhone = customerData.CustomerPhone,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now
        };

        await _context.Customes.AddAsync(customer);
        await _context.SaveChangesAsync();

        return Ok($"{customer.CutomerName}  ha sido creado");
    }
    
    #endregion

    #region Put methods
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
    public async Task<ActionResult> UpdateCustomer(Customer customerData, Guid id)
    {
        var customer = await _context.Customes.FindAsync(id);

        if(customer != null) {
            customer.CutomerName = customerData.CutomerName;
            customer.CustomerEmail = customerData.CustomerEmail;
            customer.CustomerPhone = customerData.CustomerPhone;
            customer.CustomerAddress = customerData.CustomerAddress;
            customer.ModificationDate = DateTime.Now;
        
            await _context.SaveChangesAsync();
            return Ok($"El cliente {customer.CutomerName} ha sido actualizado.");

        }else {

            return NotFound();
        }
    
    }
    #endregion

    #region Delete methods
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
    public async Task<ActionResult> DeleteCustomer(Guid id) 
    {
        var customer = await _context.Customes.FindAsync(id);

        if (customer !=null) {

            _context.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok($"El cliente {customer.CutomerName} has sido eliminado.");

        }else {

            return NotFound();
        }
    }
    #endregion
    }
}