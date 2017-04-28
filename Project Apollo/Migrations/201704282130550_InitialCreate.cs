namespace Project_Apollo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyProjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        price = c.Double(nullable: false),
                        applyingLetter = c.String(),
                        startDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                        project_ID = c.Int(),
                        projectManager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.project_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.projectManager_ID)
                .Index(t => t.project_ID)
                .Index(t => t.projectManager_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        status = c.Int(nullable: false),
                        price = c.Double(),
                        createDate = c.DateTime(nullable: false),
                        startDate = c.DateTime(),
                        endDate = c.DateTime(),
                        customer_ID = c.Int(),
                        projectManager_ID = c.Int(),
                        teamLeader_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.customer_ID)
                .ForeignKey("dbo.Users", t => t.projectManager_ID)
                .ForeignKey("dbo.Users", t => t.teamLeader_ID)
                .Index(t => t.customer_ID)
                .Index(t => t.projectManager_ID)
                .Index(t => t.teamLeader_ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comment = c.String(),
                        projectManager_ID = c.Int(),
                        project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.projectManager_ID)
                .ForeignKey("dbo.Projects", t => t.project_ID, cascadeDelete: true)
                .Index(t => t.projectManager_ID)
                .Index(t => t.project_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Photo = c.Binary(),
                        Description = c.String(nullable: false),
                        name = c.String(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 11),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        feedBack = c.String(nullable: false),
                        juniorEngineering_ID = c.Int(),
                        projectManager_ID = c.Int(),
                        teamLeader_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.juniorEngineering_ID)
                .ForeignKey("dbo.Users", t => t.projectManager_ID)
                .ForeignKey("dbo.Users", t => t.teamLeader_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.juniorEngineering_ID)
                .Index(t => t.projectManager_ID)
                .Index(t => t.teamLeader_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        qialificationName = c.String(nullable: false),
                        percentage = c.Int(nullable: false),
                        user_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.user_ID)
                .Index(t => t.user_ID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        requestType = c.String(),
                        reciever_ID = c.Int(),
                        sender_ID = c.Int(),
                        User_ID = c.Int(),
                        project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.reciever_ID)
                .ForeignKey("dbo.Users", t => t.sender_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Projects", t => t.project_ID, cascadeDelete: true)
                .Index(t => t.reciever_ID)
                .Index(t => t.sender_ID)
                .Index(t => t.User_ID)
                .Index(t => t.project_ID);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        report = c.String(nullable: false),
                        customer_ID = c.Int(),
                        projectManager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.customer_ID)
                .ForeignKey("dbo.Users", t => t.projectManager_ID)
                .Index(t => t.customer_ID)
                .Index(t => t.projectManager_ID);
            
            CreateTable(
                "dbo.WorksFor",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.UserId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "projectManager_ID", "dbo.Users");
            DropForeignKey("dbo.Reports", "customer_ID", "dbo.Users");
            DropForeignKey("dbo.WorksFor", "UserId", "dbo.Users");
            DropForeignKey("dbo.WorksFor", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "teamLeader_ID", "dbo.Users");
            DropForeignKey("dbo.Requests", "project_ID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "projectManager_ID", "dbo.Users");
            DropForeignKey("dbo.Projects", "customer_ID", "dbo.Users");
            DropForeignKey("dbo.Comments", "project_ID", "dbo.Projects");
            DropForeignKey("dbo.Comments", "projectManager_ID", "dbo.Users");
            DropForeignKey("dbo.Requests", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Requests", "sender_ID", "dbo.Users");
            DropForeignKey("dbo.Requests", "reciever_ID", "dbo.Users");
            DropForeignKey("dbo.Qualifications", "user_ID", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "teamLeader_ID", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "projectManager_ID", "dbo.Users");
            DropForeignKey("dbo.Feedbacks", "juniorEngineering_ID", "dbo.Users");
            DropForeignKey("dbo.ApplyProjects", "projectManager_ID", "dbo.Users");
            DropForeignKey("dbo.ApplyProjects", "project_ID", "dbo.Projects");
            DropIndex("dbo.WorksFor", new[] { "UserId" });
            DropIndex("dbo.WorksFor", new[] { "ProjectId" });
            DropIndex("dbo.Reports", new[] { "projectManager_ID" });
            DropIndex("dbo.Reports", new[] { "customer_ID" });
            DropIndex("dbo.Requests", new[] { "project_ID" });
            DropIndex("dbo.Requests", new[] { "User_ID" });
            DropIndex("dbo.Requests", new[] { "sender_ID" });
            DropIndex("dbo.Requests", new[] { "reciever_ID" });
            DropIndex("dbo.Qualifications", new[] { "user_ID" });
            DropIndex("dbo.Feedbacks", new[] { "User_ID" });
            DropIndex("dbo.Feedbacks", new[] { "teamLeader_ID" });
            DropIndex("dbo.Feedbacks", new[] { "projectManager_ID" });
            DropIndex("dbo.Feedbacks", new[] { "juniorEngineering_ID" });
            DropIndex("dbo.Comments", new[] { "project_ID" });
            DropIndex("dbo.Comments", new[] { "projectManager_ID" });
            DropIndex("dbo.Projects", new[] { "teamLeader_ID" });
            DropIndex("dbo.Projects", new[] { "projectManager_ID" });
            DropIndex("dbo.Projects", new[] { "customer_ID" });
            DropIndex("dbo.ApplyProjects", new[] { "projectManager_ID" });
            DropIndex("dbo.ApplyProjects", new[] { "project_ID" });
            DropTable("dbo.WorksFor");
            DropTable("dbo.Reports");
            DropTable("dbo.Requests");
            DropTable("dbo.Qualifications");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Projects");
            DropTable("dbo.ApplyProjects");
        }
    }
}
