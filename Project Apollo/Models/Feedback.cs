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

        public virtual User teamLeader { get; set; }
        public virtual User projectManager { get; set; }
        public virtual User juniorEngineering { get; set; }
        public string feedBack { get; set; }
    }
}