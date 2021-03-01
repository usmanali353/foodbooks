using foodbooks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.IRepository
{
   public interface IBusinessRepository
    {
        public Task<ActionResult> AddBusiness(Business business,string token);
        public Task<ActionResult> ChangeVisibility(int id);
        public Task<ActionResult<IEnumerable<Business>>> GetBusinessByOwner(string token);
        public Task<ActionResult<Business>> GetBusinessById(int id);
    }
}
