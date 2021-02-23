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

        public string Name { get; set; }
        
        public int BusinessId { get; set; }

        [ForeignKey(nameof(BusinessId))]
        [InverseProperty(nameof(Business.Categories))]
        public virtual Business business { get; set; }

        public bool isVisible { get; set; }
        [JsonIgnore]
        public List<Question> Questions { get; set; }
    }
}
