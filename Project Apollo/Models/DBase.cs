using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Project_Apollo.Models
{
    public class DBase : DbContext
    {
        public DBase(): base("DBase")
        {
            Database.SetInitializer<DBase>(new MigrateDatabaseToLatestVersion<DBase, Project_Apollo.Migrations.Configuration>());
            //Database.SetInitializer<DBase>(new DropCreateDatabaseAlways<DBase>());
        }
        public DbSet<User> userTable { get; set; }
        public DbSet<Requests> RequestsTable { get; set; }
        public DbSet<Reports> ReportsTable { get; set; }
        public DbSet<Qualifications> qualificationsTable { get; set; }
        public DbSet<Project> ProjectTable { get; set; }
        public DbSet<Feedback> FeedbackTable { get; set; }
        public DbSet<Comments> CommentsTable { get; set; }
        public DbSet<ApplyProject> ApplyProjectTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasMany(s => s.comments).WithOptional(s=>s.project).WillCascadeOnDelete(true); // delete cascade project -> comment
            modelBuilder.Entity<Project>().HasMany(s => s.applied).WithOptional(s => s.project).WillCascadeOnDelete(true); // delete cascade project -> appliers
            modelBuilder.Entity<Project>().HasMany(s => s.Requests).WithOptional(s => s.project).WillCascadeOnDelete(true);// delete cascade project -> requests
            //map relation on user table to project with different behavior  1 - > m
            modelBuilder.Entity<Project>().HasOptional(s => s.customer).WithMany(s => s.ownProject); 
            modelBuilder.Entity<Project>().HasOptional(s => s.projectManager).WithMany(s => s.manageProject);
            modelBuilder.Entity<Project>().HasOptional(s => s.teamLeader).WithMany(s => s.leadProject);
            //map relation on user table to project with different behavior  M - > m
            modelBuilder.Entity<Project>().
              HasMany(u => u.workers).
              WithMany(p => p.Projects).
              Map(
               m =>
               {
                   m.MapLeftKey("ProjectId");
                   m.MapRightKey("UserId");
                   m.ToTable("WorksFor");
               });
        }
        
    }
}