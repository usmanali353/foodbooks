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
        
        public int QuestionId { get; set; }

        public virtual Question questions { get; set; }

        public bool IsVisible { get; set; }
    }
}
