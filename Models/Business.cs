using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class Business
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }

        public string Website { get; set; }
        [Required]
        public string Description { get; set; }
        
        public double? Longitude { get; set; }
        
        public double? Latitude { get; set; }
        public string Address { get; set; }

        public TimeSpan? OpeningTime { get; set; }

        public TimeSpan? ClosingTime { get; set; }
        [Required]
        public string OwnerId { get; set; }

        public string Image { get; set; }

        public string Qrimage { get; set; }

        public double OverallRating { get; set; }

        public bool isVisible { get; set; }
        [Required]
        public int BusinessTypeId { get; set; }
  
        public virtual BusinessType businessType { get; set; }

        [JsonIgnore]
        public List<Category> Categories { get; set; }

        [JsonIgnore]
        public List<Question> Questions { get; set; }

        [JsonIgnore]
        public List<CustomerFeedBack> CustomerFeedBacks { get; set; }

        [JsonIgnore]
        public List<Feedback> FeedBacks { get; set; }

        [JsonIgnore]
        public virtual List<Subcategory> Subcategories { get; set; }
    }
}
