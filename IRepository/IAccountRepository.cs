using foodbooks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.IRepository
{
   public interface IAccountRepository
    {
        Task<ActionResult> SignIn(LoginViewModel loginViewModel);
        Task<ActionResult> ConfirmEmail(string userId,string token);
        Task<ActionResult> CreateRole(string name);
    }

}
