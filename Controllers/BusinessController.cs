using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using foodbooks.Models;
using Microsoft.AspNetCore.Authorization;
using foodbooks.IRepository;

namespace foodbooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly ApplicationdbContext _context;
        private readonly IBusinessRepository repository;
        public BusinessController(ApplicationdbContext context, IBusinessRepository repository)
        {
            _context = context;
            this.repository = repository;
        }

        // GET: api/Business
        [HttpGet,Route("GetBusinessByOwner/{id}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinesses(string id)
        {
            return await repository.GetBusinessByOwner(id);
        }

        // GET: api/Business/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBusiness(int id)
        {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return NotFound();
            }

            return business;
        }

        // PUT: api/Business/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusiness(int id, Business business)
        {
            if (id != business.id)
            {
                return BadRequest();
            }

            _context.Entry(business).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExists(id))
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

        // POST: api/Business
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost,Route("CreateBusiness")]
        public async Task<ActionResult<Business>> PostBusiness(Business business)
        {
            //_context.Businesses.Add(business);
            //await _context.SaveChangesAsync();

            return await repository.AddBusiness(business);
        }

        // DELETE: api/Business/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }

            _context.Businesses.Remove(business);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessExists(int id)
        {
            return _context.Businesses.Any(e => e.id == id);
        }
    }
}
