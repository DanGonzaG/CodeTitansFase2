using Preacepta.Modelos.AbstraccionesBD;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class PersonaAbogadoCasoDocCita
    {
        public GePersonaDTO? personaDTO { get; set; }
        public GeAbogadoDTO? geAbogadoDTO { get; set; }
        public IEnumerable<CasoDTO>? TresUltimosCasosXcliente { get; set; } //lista de casos por cliente
        public IEnumerable<CasoDTO>? TresUltimosCasosXabogado { get; set; } //lista de casos por abogado
        public IEnumerable<DocsCompraventaFincaDTO>? TresUltimosDocs { get; set; } //lista de documentos por ABOGADO
        public IEnumerable<DocsCompraventaFincaDTO>? ListarTresUltimosDocsXCliente { get; set; } //lista de documentos por cliente
        public IEnumerable<CitasDTO>? TresCitasProximasXAfitrion { get; set; } //lista de CITAS por ABOGADO      
        public IEnumerable<TCitasCliente>? TresCitasMasProximasXCliente { get; set; } //lista de CITAS por CLIENTE 

    }
}