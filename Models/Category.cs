using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BusinessId { get; set; }

        public virtual Business business { get; set; }

        public bool isVisible { get; set; }
        [JsonIgnore]
        public virtual List<Question> Questions { get; set; }
        [JsonIgnore]
        public virtual List<CustomerFeedBack> CustomerFeedBacks { get; set; }
        [JsonIgnore]
        public List<Feedback> FeedBacks { get; set; }
        [JsonIgnore]
        public virtual List<Subcategory> Subcategories { get; set; }
    }
}
