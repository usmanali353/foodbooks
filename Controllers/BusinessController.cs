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
        [HttpGet,Route("GetBusinessByOwner")]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinesses()
        {
            return await repository.GetBusinessByOwner(Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim());
        }

        // GET: api/Business/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBusiness(int id)
        {
            var totalFeedBack = 0.0;
            var business = await _context.Businesses.Include(bt=>bt.businessType).Include(f=>f.FeedBacks).Where(bid=>bid.id==id).FirstOrDefaultAsync();

            if (business == null)
            {
                return NotFound();
            }
            
                if (business.FeedBacks.Count > 0)
                {
                    for (int j = 0; j < business.FeedBacks.Count; j++)
                    {

                        if (business.FeedBacks[j].OverallRating.HasValue)
                            totalFeedBack += business.FeedBacks[j].OverallRating.Value;

                    }
                    business.OverallRating = totalFeedBack / double.Parse(business.FeedBacks.Count.ToString());
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

            return await repository.AddBusiness(business, Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim());
        }

        // DELETE: api/Business/5
        [HttpGet, Route("ChangeVisibility/{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }
            business.isVisible = !business.isVisible;
            _context.Businesses.Attach(business).Property(x => x.isVisible).IsModified = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool BusinessExists(int id)
        {
            return _context.Businesses.Any(e => e.id == id);
        }
    }
}
