namespace CuraVet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotto")]
    public partial class Prodotto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotto()
        {
            Vendita = new HashSet<Vendita>();
        }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public int IdDitta { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipologia { get; set; }

        [StringLength(50)]
        public string Descrizione { get; set; }

        public int Armadio { get; set; }

        public int Cassetto { get; set; }

        public bool Presente { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        public virtual Ditta Ditta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendita> Vendita { get; set; }
    }
}
