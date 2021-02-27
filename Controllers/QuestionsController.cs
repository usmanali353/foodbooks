using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using foodbooks.Models;
using foodbooks.Utils;

namespace foodbooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public QuestionsController(ApplicationdbContext context)
        {
            _context = context;
        }

        // GET: api/Questions1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions([FromQuery]int? BusinessId,[FromQuery] int? SubCategoryId,[FromQuery] int? CategoryId)
        {
            if (BusinessId != null)
            {
                return await _context.Questions.Include(op => op.questionOptions).Where(b => b.BusinessId == BusinessId).ToListAsync();
            }
             if (CategoryId != null)
            {
                return await _context.Questions.Include(op => op.questionOptions).Where(b => b.CategoryId == CategoryId).ToListAsync();
            }
            if (SubCategoryId != null)
            {
                return await _context.Questions.Include(op => op.questionOptions).Where(b => b.SubCategoryId == SubCategoryId).ToListAsync();
            }
            else
                return await _context.Questions.Include(op => op.questionOptions).ToListAsync();
        }

        // GET: api/Questions1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.Include(op => op.questionOptions).Where(q=>q.Id==id).FirstOrDefaultAsync();

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            question.IsVisible = true;
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Questions1/5
        [HttpGet,Route("ChangeVisibility/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            question.IsVisible = !question.IsVisible;
            _context.Questions.Attach(question).Property(x => x.IsVisible).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

        [HttpGet, Route("getQuestionTypeDropdown")]
        public List<Dropdown> getQuestionTypes()
        {
            return utils.getQuestionTypes();
        }
    }
}
