namespace CuraVet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animale",
                c => new
                    {
                        IdAnimale = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        DataRegistrazione = c.DateTime(nullable: false, storeType: "date"),
                        IdTipologia = c.Int(nullable: false),
                        Razza = c.String(nullable: false, maxLength: 50),
                        Colore = c.String(nullable: false, maxLength: 50),
                        DataNascita = c.DateTime(storeType: "date"),
                        Microchip = c.String(maxLength: 50),
                        IdCliente = c.Int(),
                        Foto = c.String(),
                    })
                .PrimaryKey(t => t.IdAnimale)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .ForeignKey("dbo.Tipologia", t => t.IdTipologia)
                .Index(t => t.IdTipologia)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdCliente = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                        CF = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCliente);
            
            CreateTable(
                "dbo.Vendita",
                c => new
                    {
                        IdVendita = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        IdProdotto = c.Int(nullable: false),
                        Quantita = c.Int(nullable: false),
                        RicettaMedica = c.String(nullable: false, maxLength: 50),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.IdVendita)
                .ForeignKey("dbo.Prodotto", t => t.IdProdotto)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .Index(t => t.IdCliente)
                .Index(t => t.IdProdotto);
            
            CreateTable(
                "dbo.Prodotto",
                c => new
                    {
                        IdProdotto = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        IdDitta = c.Int(nullable: false),
                        Tipologia = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(maxLength: 50),
                        Armadio = c.Int(nullable: false),
                        Cassetto = c.Int(nullable: false),
                        Presente = c.Boolean(nullable: false),
                        Prezzo = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.IdProdotto)
                .ForeignKey("dbo.Ditta", t => t.IdDitta)
                .Index(t => t.IdDitta);
            
            CreateTable(
                "dbo.Ditta",
                c => new
                    {
                        IdDitta = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Recapito = c.String(nullable: false, maxLength: 50),
                        Indirizzo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdDitta);
            
            CreateTable(
                "dbo.Tipologia",
                c => new
                    {
                        IdTipologia = c.Int(nullable: false, identity: true),
                        Tipo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdTipologia);
            
            CreateTable(
                "dbo.Visita",
                c => new
                    {
                        IdVisita = c.Int(nullable: false, identity: true),
                        DataVisita = c.DateTime(nullable: false, storeType: "date"),
                        TipoEsame = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(),
                        Attiva = c.Boolean(nullable: false),
                        IdAnimale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdVisita)
                .ForeignKey("dbo.Animale", t => t.IdAnimale)
                .Index(t => t.IdAnimale);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Ruolo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visita", "IdAnimale", "dbo.Animale");
            DropForeignKey("dbo.Animale", "IdTipologia", "dbo.Tipologia");
            DropForeignKey("dbo.Vendita", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.Vendita", "IdProdotto", "dbo.Prodotto");
            DropForeignKey("dbo.Prodotto", "IdDitta", "dbo.Ditta");
            DropForeignKey("dbo.Animale", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.Visita", new[] { "IdAnimale" });
            DropIndex("dbo.Prodotto", new[] { "IdDitta" });
            DropIndex("dbo.Vendita", new[] { "IdProdotto" });
            DropIndex("dbo.Vendita", new[] { "IdCliente" });
            DropIndex("dbo.Animale", new[] { "IdCliente" });
            DropIndex("dbo.Animale", new[] { "IdTipologia" });
            DropTable("dbo.User");
            DropTable("dbo.Visita");
            DropTable("dbo.Tipologia");
            DropTable("dbo.Ditta");
            DropTable("dbo.Prodotto");
            DropTable("dbo.Vendita");
            DropTable("dbo.Cliente");
            DropTable("dbo.Animale");
        }
    }
}
