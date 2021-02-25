using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public double? OverallRating { get; set; }

        public DateTime? dateTime { get; set; }

        public bool isVisible { get; set; }

        public List<CustomerFeedBack> customerFeedBacks { get; set; }
    }
}
