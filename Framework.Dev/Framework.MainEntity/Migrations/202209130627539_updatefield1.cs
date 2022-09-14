namespace Framework.MainEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefield1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DevLogs");
            AlterColumn("dbo.DevLogs", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DevLogs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DevLogs");
            AlterColumn("dbo.DevLogs", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.DevLogs", "Id");
        }
    }
}
