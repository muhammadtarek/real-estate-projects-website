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
            //Database.SetInitializer<DBase>(new DropCreateDatabaseAlways<DBase>());
            Database.SetInitializer<DBase>(new MigrateDatabaseToLatestVersion<DBase, Project_Apollo.Migrations.Configuration>());
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
            modelBuilder.Entity<Project>().
              HasMany(c => c.workers).
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