using foodbooks.IRepository;
using foodbooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Text;
using foodbooks.Utils;
using foodbooks.ViewModels;
using System.Collections.Generic;

namespace foodbooks.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        StringBuilder sb = new StringBuilder();
        public AccountRepository(UserManager<ApplicationUser> userManager, ApplicationdbContext applicationdbContext, RoleManager<IdentityRole> roleManager) {
            this.userManager = userManager;
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
            } else
                foreach (var error in result.Errors)
                    sb.Append(error.Description + "\n");
            return new BadRequestObjectResult(sb.ToString());
        }

        public async Task<ActionResult> CreateRole(string name)
        {
            await roleManager.CreateAsync(new IdentityRole(name));
            return new OkObjectResult(new { message = "Role Added Sucessfully" });
        }

        public async Task<ActionResult> ResetPassword(ResetpasswordViewModel resetpasswordViewModel)
        {
            var user = await userManager.FindByEmailAsync(resetpasswordViewModel.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var resetPasswordResult = await userManager.ResetPasswordAsync(user, token, resetpasswordViewModel.ConfirmPassword);
                if (resetPasswordResult.Succeeded)
                {
                    return new OkObjectResult("Your Password is Reset Now you can login using your new Password");
                } else
                    foreach (var error in resetPasswordResult.Errors)
                        return new BadRequestObjectResult(error.Description);
            }
            return new BadRequestObjectResult("No User Exist with this Email");

        }

        public async Task<ActionResult> SignIn(LoginViewModel loginViewModel)
        {
            var user = await userManager.FindByEmailAsync(loginViewModel.email);

            if (user != null && await userManager.CheckPasswordAsync(user, loginViewModel.password))
            {
                if (await userManager.IsEmailConfirmedAsync(user))
                {
                    var RolesList = await userManager.GetRolesAsync(user);
                    return new OkObjectResult(new { token = utils.GenerateAccessToken(user.Id, user, RolesList), message = "Login Sucessful", roles = RolesList });
                } else
                    return new BadRequestObjectResult("Email is not Confirmed");
            }
            else
                return new BadRequestObjectResult("Invalid Username or Password");
        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return roleManager.Roles;
        }

        public async Task<ActionResult> GetRoleById(string id)
        {
            if (id != null) 
            {
                var role = await roleManager.FindByIdAsync(id);
                return new OkObjectResult(new { name=role.Name});
            }

            return new BadRequestObjectResult("Role Id must not be null");
        }

       
    }
}
