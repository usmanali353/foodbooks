﻿using foodbooks.DTO;
using foodbooks.IRepository;
using foodbooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            if (userManager.FindByIdAsync(business.OwnerId) != null)
            {
                await context.Businesses.AddAsync(business);
                return new OkObjectResult(new { message = "Business Added Sucessfully" });
            }
            else
            
            return new BadRequestObjectResult(new {message ="Invalid Owner Id" });
        }
    }
}