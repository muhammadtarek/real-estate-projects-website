using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public enum request
    {
        teamLeaderRequest = 0,
        juniorEngineerRequest = 1
    }
    public class Requests
    {
        public int ID { get; set; }
        public virtual User sender { get; set; }
        public virtual User reciever { get; set; }
        public virtual Project project { get; set; }
        public request requestType { get; set; }
    }
}