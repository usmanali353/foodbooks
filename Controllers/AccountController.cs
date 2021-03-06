using foodbooks.IRepository;
using foodbooks.Models;
using foodbooks.Repository;
using foodbooks.Utils;
using foodbooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foodbooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAccountRepository _accountRepository;
        StringBuilder sb = new StringBuilder();

        public AccountController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._accountRepository = accountRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public async Task<ActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            var user = new ApplicationUser
            {
                UserName = registerViewModel.email,
                name = registerViewModel.name,
                PhoneNumber = registerViewModel.phone,
                Email = registerViewModel.email,
                city = registerViewModel.city,
                country = registerViewModel.country
            };

            var result = await userManager.CreateAsync(user, registerViewModel.password);

            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = user.Id, token = token }, Request.Scheme);
                var role = await roleManager.FindByIdAsync(registerViewModel.roleId);
                if (role != null)
                {
                    var roleAddingResult = await userManager.AddToRoleAsync(user, role.Name);
                    if (roleAddingResult.Succeeded)
                    {
                        utils.sendMail(registerViewModel.email, confirmationLink, "Please Click Below Button to Verify your email and continue using FoodBooks", "Please Verify your Email to continue using foodBooks");
                        return new OkObjectResult("verification Email is sent on your mentioned email address please verify to Continue");
                    }
                    else
                        foreach (var error in roleAddingResult.Errors)
                            sb.Append(error.Description + "\n");
                    return new BadRequestObjectResult(sb.ToString());
                }



            }

            else
                foreach (var error in result.Errors)
                    sb.Append(error.Description + "\n");
            return new BadRequestObjectResult(sb.ToString());
        }
        [AllowAnonymous]
        [HttpGet, Route("VerifyEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {

            return await _accountRepository.ConfirmEmail(userId, token);
        }
        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<ActionResult> SignIn(LoginViewModel loginViewmodel)
        {

            return await _accountRepository.SignIn(loginViewmodel);
        }
        [AllowAnonymous]
        [HttpPost, Route("AddRoles/{name}")]
        public async Task<ActionResult> AddRole(string name)
        {
            return await _accountRepository.CreateRole(name);
        }

        [AllowAnonymous]
        [HttpPost, Route("ForgotPassword")]
        public async Task<ActionResult> ResetPassword(ResetpasswordViewModel resetPasswordViewModel)
        {
            return await _accountRepository.ResetPassword(resetPasswordViewModel);
        }
        [AllowAnonymous]
        [HttpGet, Route("GetRoles")]
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _accountRepository.GetAllRoles();
        }
        [AllowAnonymous]
        [HttpGet, Route("GetRolesById/{id}")]
        public async Task<ActionResult> GetRoleById(string id) 
        {
            return await _accountRepository.GetRoleById(id);
        }
    }
}
