using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public class Feedback
    {
        public int ID { get; set; }

        public User teamLeader { get; set; }
        public User projectManager { get; set; }
        public User juniorEngineering { get; set; }
        [Required]
        public string feedBack { get; set; }
    }
}