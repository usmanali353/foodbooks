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
    public class QuestionOptionsController : ControllerBase
    {
        private readonly ApplicationdbContext _context;

        public QuestionOptionsController(ApplicationdbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionOptions>>> GetQuestionOptions()
        {
            return await _context.QuestionOptions.ToListAsync();
        }

        // GET: api/QuestionOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionOptions>> GetQuestionOptions(int id)
        {
            var questionOptions = await _context.QuestionOptions.FindAsync(id);

            if (questionOptions == null)
            {
                return NotFound();
            }

            return questionOptions;
        }

        // PUT: api/QuestionOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionOptions(int id, QuestionOptions questionOptions)
        {
            if (id != questionOptions.QuestionOptionId)
            {
                return BadRequest();
            }

            _context.Entry(questionOptions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionOptionsExists(id))
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

        // POST: api/QuestionOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionOptions>> PostQuestionOptions(QuestionOptions questionOptions)
        {
            _context.QuestionOptions.Add(questionOptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionOptions", new { id = questionOptions.QuestionOptionId }, questionOptions);
        }

        // DELETE: api/QuestionOptions/5
        [HttpGet, Route("ChangeVisibility/{id}")]
        public async Task<IActionResult> DeleteQuestionOptions(int id)
        {
            var questionOptions = await _context.QuestionOptions.FindAsync(id);
            if (questionOptions == null)
            {
                return NotFound();
            }
            questionOptions.IsVisible = !questionOptions.IsVisible;
            _context.QuestionOptions.Attach(questionOptions).Property(x=>x.IsVisible).IsModified=true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool QuestionOptionsExists(int id)
        {
            return _context.QuestionOptions.Any(e => e.QuestionOptionId == id);
        }
    }
}
