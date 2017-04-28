using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public class ApplyProject
    {
        public int ID { get; set; }
        public virtual Project project { get; set; }
        public virtual User  projectManager { get; set; }
        public double price { get; set; }
        public String applyingLetter { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}