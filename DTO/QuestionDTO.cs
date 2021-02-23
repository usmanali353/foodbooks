using foodbooks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.DTO
{
    public class QuestionDTO
    {
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public int QuestionType { get; set; }
        [Required]
        public int BusinessId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public List<QuestionOptions> questionOptions { get; set; }
    }
}
