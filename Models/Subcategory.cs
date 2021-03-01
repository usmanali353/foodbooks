using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class Subcategory
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category category { get; set; }

        public bool isVisible { get; set; }
        [Required]
        public int BusinessId {get; set;}
        [JsonIgnore]
        public Business business { get; set; }

        [JsonIgnore]
        public virtual List<Question> Questions { get; set; }

        [JsonIgnore]
        public virtual List<CustomerFeedBack> CustomerFeedBacks { get; set; }

        [JsonIgnore]
        public List<Feedback> FeedBacks { get; set; }
    }
}
