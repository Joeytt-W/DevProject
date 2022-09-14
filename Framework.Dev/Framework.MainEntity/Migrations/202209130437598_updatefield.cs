namespace Framework.MainEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DevLogs", "CreateTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DevLogs", "CreateTime", c => c.DateTime(nullable: false));
        }
    }
}
