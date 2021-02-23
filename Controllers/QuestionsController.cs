using foodbooks.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        [HttpGet,Route("getQuestionTypeDropdown")]
        public List<Dropdown> getQuestionTypes() 
        {
            return  utils.getQuestionTypes();
        }
    }
}
