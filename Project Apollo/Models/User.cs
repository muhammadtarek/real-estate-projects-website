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
        public User()
        {
            Projects = new List<Project>();
            Qualifications = new List<Qualifications>();
            Requests = new List<Requests>();
            Feedback = new List<Feedback>();
            ownProject = new List<Project>();
            manageProject = new List<Project>();
            leadProject = new List<Project>();
            Applyer = new List<ApplyProject>();
        }
        public int ID { get; set; }
        public Byte[] Photo { get; set; }
        public String Description { get; set; }
        public String name { get; set; }
        public String Mobile { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public userRole UserRole { get; set; }
        public virtual ICollection<Project> ownProject { get; set; } // Project -> Customer M->1
        public virtual ICollection<Project> Projects { get; set; } // Project  -Work'sfor> JuniorEng M->M
        public virtual ICollection<Project> manageProject { get; set; } // project -> pm M->1
        public virtual ICollection<Project> leadProject { get; set; }// TeamLeader -> pm M->1
        public virtual ICollection<ApplyProject> Applyer { get; set; } // Pm -> applytoproject 1 -> M
        public virtual ICollection<Qualifications> Qualifications { get; set; }
        public virtual ICollection<Requests> Requests { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }

    }
}