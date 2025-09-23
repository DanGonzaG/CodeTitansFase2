using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesBD
{

    [Table("HistorialDocumentos")]
    public partial class THistorialDocumento
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; }

        // opcionales
        public int? Cliente { get; set; }
        public int? Abogado { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Tipo de documento")]
        public string TipoDocumento { get; set; } = null!;

        [DisplayName("Id del documento")]
        public int IdDocumento { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Título")]
        public string Titulo { get; set; } = null!;

        [ForeignKey(nameof(Cliente))]
        public virtual TGePersona? ClienteNavigation { get; set; }

        [ForeignKey(nameof(Abogado))]
        public virtual TGeAbogado? AbogadoNavigation { get; set; }
    }
}
