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
        public User sender { get; set; }
        public User reciever { get; set; }
        public Project project { get; set; }
        [ScaffoldColumn(false)]
        public string requestType { get; set; }
    }
}