namespace CuraVet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Animale")]
    public partial class Animale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animale()
        {
            Visita = new HashSet<Visita>();
        }

        [Key]
        public int IdAnimale { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataRegistrazione { get; set; }

        public int IdTipologia { get; set; }

        [Required]
        [StringLength(50)]
        public string Razza { get; set; }

        [Required]
        [StringLength(50)]
        public string Colore { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        [StringLength(50)]
        public string Microchip { get; set; }

        public int? IdCliente { get; set; }

        public string Foto { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Tipologia Tipologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visita> Visita { get; set; }

    }
}
