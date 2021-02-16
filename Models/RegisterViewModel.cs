using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [Phone]
        public string phone { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string city { get; set; }
    }
}
