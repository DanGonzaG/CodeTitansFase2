using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesBD
{
    public class TClavePublica
    {
        [Key] 
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        [Column("ClavePublica")]
        public string PublicKeyPem { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }


    }
}
