using foodbooks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.IRepository
{
   public interface ICategoryRepository
    {
        public Task<ActionResult> AddCategory(Category category);

        public Task<ActionResult<IEnumerable<Category>>> GetCategoryByBusiness(int id);

        public Task<ActionResult<Category>> GetCategoryById(int id);

        public Task<ActionResult> ChangeVisibility(int id);
    }
}
