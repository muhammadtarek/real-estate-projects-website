using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public class Reports
    {
        public int ID { get; set; }
        public virtual User projectManager { get; set; }
        public virtual User customer { get; set; }
        public string report { get; set; }
    }
}