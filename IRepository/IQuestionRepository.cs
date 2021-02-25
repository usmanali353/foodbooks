using foodbooks.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.IRepository
{
    interface IQuestionRepository
    {
        public Task<ActionResult> AddQuestion(QuestionDTO questionDto);
        public Task<ActionResult> ChangeVisibility(int id);
        public Task<ActionResult> UpdateQuestion(int id,QuestionDTO questionDto);
    }
}
