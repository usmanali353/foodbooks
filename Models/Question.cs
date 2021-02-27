using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class Question
    {   [Key]
        public int Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public int QuestionType { get; set; }
        [Required]
        public int BusinessId { get; set; }
       
        public virtual Business business { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public virtual Category category { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        
        public virtual Subcategory subcategory { get; set; }
        public List<QuestionOptions> questionOptions { get; set;}

        [JsonIgnore]
        public List<CustomerFeedBack> FeedBacks { get; set; }

        public bool IsVisible { get; set; }
    }
}
