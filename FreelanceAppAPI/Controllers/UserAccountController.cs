using System.Net;
using System.Security.Claims;
using System.Xml;
using FreelanceAppAPI.Context;
using FreelanceAppAPI.Models;
using FreelanceAppAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly ITokenBuilder _tokenBuilder;

        
        public UserAccountController(UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                ApplicationDbContext context,
                ITokenBuilder tokenBuilder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

    #region Post Methods
        [HttpPost]
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
                var role = "User";
                var userToAssign = await _userManager.FindByIdAsync(user.Id);
                await _userManager.AddClaimAsync(userToAssign,new System.Security.Claims.Claim(ClaimTypes.Role, role));
                await _userManager.AddToRoleAsync(userToAssign,role);
                var token = _tokenBuilder.BuildToken(userModel,new List<string>(),user.Id);

                return Ok("Usuario Creado");

            }else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser (UserAccountModel userLogin) 
        {

            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            var result = await _signInManager.PasswordSignInAsync(user,userLogin.Password,isPersistent: false,lockoutOnFailure: false );
            

            if(result.Succeeded) {

                var role = await _userManager.GetRolesAsync(user);

                //return Ok($"El usuario {userLogin.UserName} ha sido verificado.");
                return Ok(_tokenBuilder.BuildToken(userLogin, role, user.Id));

            }else {

                return BadRequest("Verifique su usuario");
            }
        }
        #endregion

    #region Get Methods

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
        public async Task<IActionResult> GetUserList() 
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
        public async Task<IActionResult> GetUserById(string id){

            var user = await _context.Users.FindAsync(id);

            return Ok(user);
        }
        #endregion

    #region Put Methods
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
        public async Task<ActionResult> UpdateUserById([FromBody] UserAccountModel appUser, string id ){

            var user = await  _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                
                if (user != null) {
                user.Email = appUser.Email;
                var result = await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
                }
            return Ok("Usuario actualizado");
        }

        [HttpPut("PasswordReset")]
        public async Task<ActionResult>  ResetPassword(ResetPasswordModel  resetPasswordData) {

            var user  = await _userManager.FindByEmailAsync(resetPasswordData.Email);
            var result = await _userManager.ChangePasswordAsync(user, resetPasswordData.CurrentPassword,resetPasswordData.NewPassword);

            if (result.Succeeded) {
                return Ok($"El password para el usuario {resetPasswordData.UserName} ha sido cambiado");
            }else {
                return BadRequest("Verifique su data");
            }



        } 

    #endregion

    #region Delete method
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles= "User")]
    public async Task<ActionResult<ApplicationUser>> DeleteUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var result = await _userManager.DeleteAsync(user);
    if (user != null) {

        await _context.SaveChangesAsync();
    }
        
        return Ok(result.Succeeded);

    }

    #endregion

    }
}
