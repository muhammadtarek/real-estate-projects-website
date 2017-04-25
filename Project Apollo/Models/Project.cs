using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public enum status
    {
        waiting,
        inProgress,
        deliverd
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
        [Required]
        public int price { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
        public ICollection<User> users { get; set; }
        public ICollection<Comments> Comments { get; set; }

    }
}