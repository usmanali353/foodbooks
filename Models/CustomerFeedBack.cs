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

        public int? QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(Question.FeedBacks))]
        public virtual Question questions {get; set;}
        public int? BusinessId { get; set; }

        [ForeignKey(nameof(BusinessId))]
        [InverseProperty(nameof(Business.FeedBacks))]
        public virtual Business business { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Category.FeedBacks))]
        public virtual Category category { get; set; }

        public double Rating { get; set; }

        public virtual Feedback feedback { get; set; }

        public DateTime? dateTime { get; set; }
    }
}
