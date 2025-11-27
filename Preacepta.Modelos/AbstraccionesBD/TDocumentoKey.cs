using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesBD
{
    [Table("T_DocumentoKeys")]
    public class TDocumentoKey
    {
        public int Id { get; set; }
        public int DocumentoId { get; set; }
        public string UsuarioId { get; set; }
        public string EncryptedKeyBase64 { get; set; }
        public string IV { get; set; }    
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual TDocumentosCita Documento { get; set; }
    }
}
