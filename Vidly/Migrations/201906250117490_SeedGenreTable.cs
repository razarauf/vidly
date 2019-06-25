namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT GENRES ON");

            Sql("INSERT INTO GENRES (Id, Name) VALUES (1, 'Action')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (2, 'Thriller')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (3, 'Family')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (4, 'Romance')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (5, 'Comedy')");

            Sql("SET IDENTITY_INSERT GENRES OFF");
        }

        public override void Down()
        {
        }
    }
}
