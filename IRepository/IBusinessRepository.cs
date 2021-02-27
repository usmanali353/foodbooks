﻿using foodbooks.DTO;
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
        public Task<ActionResult> AddBusiness(Business business);
        public Task<ActionResult> ChangeVisibility(int id);
        public Task<ActionResult<IEnumerable<Business>>> GetBusinessByOwner(string OwnerId);
        public Task<ActionResult<Business>> GetBusinessById(int id);
    }
}
