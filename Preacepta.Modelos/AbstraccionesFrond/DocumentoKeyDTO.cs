using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocumentoKeyDTO
    {
        public int Id { get; set; }
        public int DocumentoId { get; set; }
        public int UsuarioId { get; set; }
        public string EncryptedKeyBase64 { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
