using foodbooks.DTO;
using foodbooks.IRepository;
using foodbooks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationdbContext context;

        public QuestionRepository(ApplicationdbContext Context) 
        {
            context = Context;
        }
        public Task<ActionResult> AddQuestion(QuestionDTO questionDto)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> ChangeVisibility(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> UpdateQuestion(int id, QuestionDTO questionDto)
        {
            throw new NotImplementedException();
        }
    }
}
