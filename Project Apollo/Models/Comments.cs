using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public User projectManager { get; set; }
        public Project project { get; set; }
        public string comment { get; set; }
    }
}