using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocumentosCitaDTO
    {
        public int Id { get; set; }
        public int IdCita { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime FechaSubida { get; set; }
        public bool Descargar { get; set; }
        public bool Activo { get; set; }

        public string OwnerId { get; set; }
        public string? IV { get; set; }
        public string? Algoritmo { get; set; }
        public string? ContentType { get; set; }
        public byte[] ArchivoCifrado { get; set; }
    }
}
