namespace CRMFacilitoInicial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _emailtipoChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emails", "Principal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emails", "Principal", c => c.String());
        }
    }
}
