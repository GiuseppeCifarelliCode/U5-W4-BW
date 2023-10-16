namespace CuraVet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vendita")]
    public partial class Vendita
    {
        [Key]
        public int IdVendita { get; set; }

        public int IdCliente { get; set; }

        public int IdProdotto { get; set; }

        public int Quantita { get; set; }

        [Required]
        [StringLength(50)]
        public string RicettaMedica { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Prodotto Prodotto { get; set; }
    }
}
