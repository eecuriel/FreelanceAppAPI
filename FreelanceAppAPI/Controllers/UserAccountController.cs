using FreelanceAppAPI.Context;
using FreelanceAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UserAccountController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                ApplicationDbContext context
                )
        {
             _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;

        }

        #region Post Methods
        [HttpPost("CreateUser")]
        public async Task<IActionResult> PostCreateUser([FromBody] UserAccountModel userModel)
        {
            var user = new ApplicationUser()
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
            };

            var result = await _userManager.CreateAsync(user,userModel.Password);
            
            if (result.Succeeded)
            {
                return Ok("Usuario Creado");
            }else
            {
                return BadRequest(result.Errors);
            }
 
        }

        #endregion


    }
}
