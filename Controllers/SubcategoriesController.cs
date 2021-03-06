﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using foodbooks.Models;

namespace foodbooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public SubcategoriesController(ApplicationdbContext context)
        {
            _context = context;
        }

        // GET: api/Subcategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategory>>> GetSubcategories([FromQuery]int? BusinessId,[FromQuery] int? CategoryId)
        {
            if (BusinessId != null) 
            {
                return await _context.Subcategories.Where(cid => cid.BusinessId == BusinessId).ToListAsync();
            }
            if (CategoryId != null) 
            {
                return await _context.Subcategories.Where(cid => cid.CategoryId == CategoryId).ToListAsync();
            }

            return await _context.Subcategories.ToListAsync();


        }

    

        // GET: api/Subcategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subcategory>> GetSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);

            if (subcategory == null)
            {
                return NotFound();
            }

            return subcategory;
        }

        // PUT: api/Subcategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategory(int id, Subcategory subcategory)
        {
            if (id != subcategory.id)
            {
                return BadRequest();
            }

            _context.Entry(subcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subcategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subcategory>> PostSubcategory(Subcategory subcategory)
        {
            subcategory.isVisible = true;
            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubcategory", new { id = subcategory.id }, subcategory);
        }

        // DELETE: api/Subcategories/5
        [HttpGet, Route("ChangeVisibility/{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            subcategory.isVisible = !subcategory.isVisible;
            _context.Subcategories.Attach(subcategory).Property(x => x.isVisible).IsModified = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool SubcategoryExists(int id)
        {
            return _context.Subcategories.Any(e => e.id == id);
        }
    }
}
