namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasoUnionEtapasPL
    {
        public CasoDTO? casoDTO { get; set; }
        public CasosEtapaDTO? casosEtapaDTO { get; set; }
        public IEnumerable<CasosEtapaDTO>? listarCasoEtapas { get; set; }
    }
}
