using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public class Qualifications
    {
        public int ID { get; set; }
        public virtual User user { get; set; }
        public String qialificationName { get; set; }
        public int percentage { get; set; }
        
    }
}