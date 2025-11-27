
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Preacepta.Modelos.AbstraccionesFrond
    {
            public class SubirDocumentoCifradoRequest
            {
                public int IdCita { get; set; }
                public string OwnerId { get; set; }              // Usuario que sube el doc
                public string NombreArchivo { get; set; }
                public string ContentType { get; set; }
                public string Algoritmo { get; set; } = "AES-GCM-256";
                public string IV { get; set; }                // Base64
                public string EncryptedKeyBase64 { get; set; } // Clave AES cifrada con RSA del usuario
                public IFormFile ArchivoCifrado { get; set; }  // Binario cifrado
            }
        }
    
