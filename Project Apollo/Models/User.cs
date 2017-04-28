using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Apollo.Models
{
    public enum userRole{
        admin = 0,
        customer = 1,
        projectManager = 2,
        teamLeader = 3,
        juniorEngineer = 4
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
        public String Password { get; set; }

        public userRole UserRole { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Qualifications> Qualifications { get; set; }
        public virtual ICollection<Requests> Requests { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }

    }
}