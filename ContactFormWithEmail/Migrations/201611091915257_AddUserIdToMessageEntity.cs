namespace ContactFormWithEmail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToMessageEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "UserId");
        }
    }
}
