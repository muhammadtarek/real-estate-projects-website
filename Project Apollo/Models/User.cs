using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public enum userRole{
        admin,
        customer,
        projectManager,
        teamLeader,
        juniorEngineer
    }
    public class User
    {
        public int ID { get; set; }
        public Byte[] Photo { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String name { get; set; }
        [StringLength(11), Required]
        public String Mobile { get; set; }
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"), Required]
        public String Email { get; set; }

        public userRole UserRole { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Qualifications> Qualifications { get; set; }
        public ICollection<Requests> Requests { get; set; }
        public ICollection<Feedback> Feedback { get; set; }

    }
}