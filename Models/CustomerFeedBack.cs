using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class CustomerFeedBack
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; }
       
        public virtual Question questions { get; set; }
        [Required]
        public int BusinessId { get; set; }

        public virtual Business business { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public virtual Category category { get; set; }
        [Required]
        public int SubcategoryId {get; set;}
       
        public virtual Subcategory subCategory { get; set; }
        [Required]
        public double Rating { get; set; }

        public virtual Feedback feedback { get; set; }

        public DateTime? dateTime { get; set; }
        [Required]
        public int QuestionOptionsId { get; set; }
        [ForeignKey(nameof(QuestionOptionsId))]
        [InverseProperty(nameof(QuestionOptions.customerFeedBacks))]
        public virtual QuestionOptions questionOptions { get; set; }
    }
}
