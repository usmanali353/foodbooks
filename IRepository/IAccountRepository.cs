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
        Task<ActionResult> SignIn(LoginDto loginViewModel);
        Task<ActionResult> ConfirmEmail(string userId,string token);

    }

}
