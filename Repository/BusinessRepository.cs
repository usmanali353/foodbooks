using foodbooks.IRepository;
using foodbooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
       private readonly ApplicationdbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public BusinessRepository(ApplicationdbContext context, UserManager<ApplicationUser> userManager) 
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<ActionResult> AddBusiness(Business business)
        {
            if (await userManager.FindByIdAsync(business.OwnerId) != null)
            {
                context.Businesses.Add(business);
                await context.SaveChangesAsync();
                return new OkObjectResult(new { message = "Business Added Sucessfully" });
            }
            else
            return new BadRequestObjectResult(new {message ="Invalid Owner Id" });
        }

        public Task<ActionResult> ChangeVisibility(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Business>> GetBusinessById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Business>>> GetBusinessByOwner(string OwnerId)
        {
            if (OwnerId!=null&&await userManager.FindByIdAsync(OwnerId) != null) 
            {
             return new OkObjectResult(await context.Businesses.Where(b => b.OwnerId == OwnerId).Include(bt=>bt.businessType).ToListAsync());
            }else
             return new BadRequestObjectResult(new { message = "Invalid Owner Id" });
        }
    }
}
