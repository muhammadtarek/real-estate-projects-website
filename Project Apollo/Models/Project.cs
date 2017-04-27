using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public enum status
    {
        waiting = 0,
        inProgress = 1,
        deliverd = 2,
        pending = 3
    }
    public class Project
    {
        public int ID { get; set; }
        public User customer { get; set; }
        public User projectManager { get; set; }
        public User teamLeader { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        [ScaffoldColumn(false)]
        public status status { get; set; }
        public Double price { get; set; }
        public DateTime createDate { get; set; } = DateTime.Now;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ICollection<User> workers { get; set; }
        public ICollection<Comments> comments { get; set; }

    }
}