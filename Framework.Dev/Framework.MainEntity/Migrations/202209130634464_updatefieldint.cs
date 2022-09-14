namespace Framework.MainEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefieldint : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DevLogs");
            AlterColumn("dbo.DevLogs", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DevLogs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DevLogs");
            AlterColumn("dbo.DevLogs", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DevLogs", "Id");
        }
    }
}
