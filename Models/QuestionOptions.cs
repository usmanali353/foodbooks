using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class QuestionOptions
    {
        [Key]
        public int QuestionOptionId { get; set; }
        [Required]
        public string QuestionOptionText { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(Question.questionOptions))]
        public virtual Question questions { get; set; }
    }
}
