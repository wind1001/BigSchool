namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Lecturer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Lecturer_Id" });
            RenameColumn(table: "dbo.Courses", name: "Lecturer_Id", newName: "LecturerId");
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        AttendeId = c.String(nullable: false, maxLength: 128),
                        Attendee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.AttendeId })
                .ForeignKey("dbo.AspNetUsers", t => t.Attendee_Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId)
                .Index(t => t.Attendee_Id);
            
            AlterColumn("dbo.Courses", "LecturerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Courses", "LecturerId");
            AddForeignKey("dbo.Courses", "LecturerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Courses", "LectureId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "LectureId", c => c.String(nullable: false));
            DropForeignKey("dbo.Courses", "LecturerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "LecturerId" });
            DropIndex("dbo.Attendances", new[] { "Attendee_Id" });
            DropIndex("dbo.Attendances", new[] { "CourseId" });
            AlterColumn("dbo.Courses", "LecturerId", c => c.String(maxLength: 128));
            DropTable("dbo.Attendances");
            RenameColumn(table: "dbo.Courses", name: "LecturerId", newName: "Lecturer_Id");
            CreateIndex("dbo.Courses", "Lecturer_Id");
            AddForeignKey("dbo.Courses", "Lecturer_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
