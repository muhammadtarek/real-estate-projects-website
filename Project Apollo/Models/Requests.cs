using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public class Requests
    {
        public int ID { get; set; }
        public virtual User sender { get; set; }
        public virtual User reciever { get; set; }
        public virtual Project project { get; set; }
        [ScaffoldColumn(false)]
        public string requestType { get; set; }
    }
}