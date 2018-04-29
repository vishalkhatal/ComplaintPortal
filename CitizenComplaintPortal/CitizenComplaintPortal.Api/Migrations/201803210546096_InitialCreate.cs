namespace CitizenComplaintPortal.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        text = c.String(nullable: false),
                        ComplaintId = c.Int(nullable: false),
                        UpdatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Complaints", t => t.ComplaintId, cascadeDelete: true)
                .Index(t => t.ComplaintId);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        ComplaintId = c.Int(nullable: false, identity: true),
                        Details = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PinCode = c.String(nullable: false),
                        complaintStatus = c.Int(nullable: false),
                        complaintType = c.Int(nullable: false),
                        RaisedBy = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ComplaintId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        MobileNo = c.String(nullable: false),
                        role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ComplaintId", "dbo.Complaints");
            DropIndex("dbo.Complaints", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ComplaintId" });
            DropTable("dbo.Users");
            DropTable("dbo.Complaints");
            DropTable("dbo.Comments");
        }
    }
}
