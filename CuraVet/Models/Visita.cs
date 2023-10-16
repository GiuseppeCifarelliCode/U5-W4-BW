namespace CuraVet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visita")]
    public partial class Visita
    {
        [Key]
        public int IdVisita { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataVisita { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoEsame { get; set; }

        public string Descrizione { get; set; }

        public bool Attiva { get; set; }

        public int IdAnimale { get; set; }

        public virtual Animale Animale { get; set; }
    }
}
