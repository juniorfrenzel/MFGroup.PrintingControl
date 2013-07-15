namespace MFGroup.PrintingControl.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiraMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        MaterialID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 500),
                        Quantidade = c.Int(),
                        Gramatura = c.String(maxLength: 6),
                        Tamanho = c.String(maxLength: 50),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        Marca_MarcaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialID)
                .ForeignKey("dbo.Marca", t => t.Marca_MarcaID, cascadeDelete: true)
                .Index(t => t.Marca_MarcaID);
            
            CreateTable(
                "dbo.Marca",
                c => new
                    {
                        MarcaID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MarcaID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Material", new[] { "Marca_MarcaID" });
            DropForeignKey("dbo.Material", "Marca_MarcaID", "dbo.Marca");
            DropTable("dbo.Marca");
            DropTable("dbo.Material");
        }
    }
}
