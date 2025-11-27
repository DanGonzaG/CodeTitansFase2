using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesBD
{
    public class TDocumentosCita
    {
        public int Id { get; set; }
        public int IdCita { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime FechaSubida { get; set; }
        public virtual TCita Cita { get; set; }
        public bool Descargar { get; set; } = true;
        public bool Activo { get; set; } = true;

        /// <summary>
        /// Id del usuario que subió / propietario del documento.
        /// Preferible que coincida con el tipo de Id de tu tabla de usuarios.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// IV para AES-GCM (almacenada en Base64).
        /// </summary>
        public string? IV { get; set; }

        /// <summary>
        /// Algoritmo usado (ej: "AES-GCM-256").
        /// </summary>
        public string? Algoritmo { get; set; }

        /// <summary>
        /// MIME type original (opcional).
        /// </summary>
        public string? ContentType { get; set; }

        // Navegación hacia las claves cifradas por usuario

        public byte[] ArchivoCifrado { get; set; }
        public virtual ICollection<TDocumentoKey> DocumentoKeys { get; set; }
    }
}