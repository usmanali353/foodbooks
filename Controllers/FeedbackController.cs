using System;
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
    public class FeedbackController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public FeedbackController(ApplicationdbContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks([FromQuery] int? CategoryId,[FromQuery] int? SubcategoryId,[FromQuery] string email)
        {
            if (CategoryId != null) 
            {
                return await _context.Feedbacks.Include(cf => cf.customerFeedBacks).ThenInclude(q => q.questionOptions).Where(cid=>cid.CategoryId==CategoryId).ToListAsync();
            }
            if (SubcategoryId != null) 
            {
                return await _context.Feedbacks.Include(cf => cf.customerFeedBacks).ThenInclude(q => q.questionOptions).Where(cid => cid.SubcategoryId == SubcategoryId).ToListAsync();
            }
            if (email != null) 
            {
                return await _context.Feedbacks.Include(cf => cf.customerFeedBacks).ThenInclude(q => q.questionOptions).Where(cid => cid.Email == email).ToListAsync();
            }

            return await _context.Feedbacks.Include(cf=>cf.customerFeedBacks).ThenInclude(q=>q.questionOptions).ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.Include(cf => cf.customerFeedBacks).ThenInclude(q => q.questionOptions).Where(fid=>fid.Id==id).FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/Feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            var TotalRatingByCustomer=0.0;
            feedback.isVisible = true;
            feedback.dateTime = DateTime.Now;
            foreach(var CustomerFeedBack in feedback.customerFeedBacks) 
            {
                CustomerFeedBack.dateTime = DateTime.Now;
                TotalRatingByCustomer += CustomerFeedBack.Rating;
            }
            feedback.OverallRating = TotalRatingByCustomer / double.Parse(feedback.customerFeedBacks.Count.ToString());
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = feedback.Id }, feedback);
        }

        // DELETE: api/Feedback/5
        [HttpGet,Route("ChangeVisibility/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            feedback.isVisible = !feedback.isVisible;
            _context.Feedbacks.Attach(feedback).Property(x => x.isVisible).IsModified = true;
           await _context.SaveChangesAsync();
            return Ok();
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
