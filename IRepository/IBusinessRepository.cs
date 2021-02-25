using foodbooks.DTO;
using foodbooks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.IRepository
{
    interface IBusinessRepository
    {
        public Task<ActionResult> AddBusiness(Business business);
    }
}
