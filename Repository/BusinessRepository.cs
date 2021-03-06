using foodbooks.IRepository;
using foodbooks.Models;
using foodbooks.Utils;
using Microsoft.AspNetCore.Hosting;
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
       private readonly IWebHostEnvironment _env;
        public BusinessRepository(ApplicationdbContext context, UserManager<ApplicationUser> userManager) 
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<ActionResult> AddBusiness(Business business,string token)
        {
            business.isVisible = true;
            business.OwnerId = utils.ParseToken(token).id;
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

        public async Task<ActionResult<IEnumerable<Business>>> GetBusinessByOwner(string token)
        {
            var totalFeedBack=0.0;
            if (utils.ParseToken(token).id != null&&await userManager.FindByIdAsync(utils.ParseToken(token).id) != null) 
            {
               
                var business = await context.Businesses.Where(b => b.OwnerId == utils.ParseToken(token).id).Include(bt => bt.businessType).Include(f=>f.FeedBacks).ToListAsync();
                for (int i = 0; i < business.Count; i++) 
                {
                    if (business[i].FeedBacks.Count > 0) 
                    {
                        for (int j = 0; j < business[i].FeedBacks.Count; j++) 
                        {
                           
                          if(business[i].FeedBacks[j].OverallRating.HasValue)
                          totalFeedBack += business[i].FeedBacks[j].OverallRating.Value;
                            
                        }
                        business[i].OverallRating = totalFeedBack / double.Parse(business[i].FeedBacks.Count.ToString());
                    }
                }
             return new OkObjectResult(await context.Businesses.Where(b => b.OwnerId == utils.ParseToken(token).id).Include(bt=>bt.businessType).ToListAsync());
            }else
             return new BadRequestObjectResult(new { message = "Invalid Owner Id" });
        }
    }
}
