using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace foodbooks.Models
{
    public class BusinessType
    {
      [Key]
      public int id { get; set; }
      public string name { get; set; }

      public bool isVisible { get; set; }

        [JsonIgnore]
        public List<Business> businesses { get; set; }
    }
}
