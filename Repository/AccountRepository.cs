using foodbooks.IRepository;
using foodbooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Text;
using foodbooks.Utils;

namespace foodbooks.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationdbContext applicationdbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        StringBuilder sb = new StringBuilder();
        public AccountRepository(UserManager<ApplicationUser> userManager,ApplicationdbContext applicationdbContext, RoleManager<IdentityRole> roleManager) {
            this.userManager = userManager;
            this.applicationdbContext = applicationdbContext;
            this.roleManager = roleManager;
        }
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new BadRequestObjectResult("Email Cannot be Confirmed");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new OkObjectResult("Email Confirmed");
            }else
                foreach (var error in result.Errors)
                    sb.Append(error.Description + "\n");
            return new BadRequestObjectResult(sb.ToString());
        }

        public async Task<ActionResult> SignIn(LoginViewModel loginViewModel)
        {
            var user = await userManager.FindByEmailAsync(loginViewModel.email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginViewModel.password)) 
            {
                var RolesList= await userManager.GetRolesAsync(user);
                return new OkObjectResult(new { token = utils.GenerateAccessToken(user.Id,user), message = "Login Sucessful", roles =RolesList});
            }else
                return new BadRequestObjectResult("Invalid Username or Password");
        }

    }
}
