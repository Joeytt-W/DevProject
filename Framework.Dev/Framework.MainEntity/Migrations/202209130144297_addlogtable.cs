namespace Framework.MainEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlogtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DevLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LogLevel = c.String(),
                        Message = c.String(),
                        StackTrace = c.String(),
                        Account = c.String(),
                        RealName = c.String(),
                        Operation = c.String(),
                        IP = c.String(),
                        IPAddress = c.String(),
                        Browser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DevLogs");
        }
    }
}
