namespace EstoqueEntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_de_Banco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProdutoEstoques",
                c => new
                    {
                        NumeroProduto = c.String(nullable: false, maxLength: 128),
                        NomeProduto = c.String(),
                        DescricaoProduto = c.String(),
                        EstoqueProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumeroProduto);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProdutoEstoques");
        }
    }
}
