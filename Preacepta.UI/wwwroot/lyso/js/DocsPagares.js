document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsPagares').addEventListener('submit', (e) => {
    const now = new Date();

    const fecha = now.toISOString().slice(0, 10); 
    const hora = now.toTimeString().slice(0, 5);   

    document.getElementById('idFechaFirma').value = fecha;
    document.getElementById('idHoraFirma').value = hora;
});

document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const montoInput = document.getElementById('idMontoNumerico');
    let MontoNumerico = montoInput?.value || '';
    MontoNumerico = MontoNumerico.replace(',', '.');

    const CedulaDeudor = document.getElementById('idCedulaDeudor').value;
    const SociedadDeudor = document.getElementById('idSociedadDeudor').value;

    const TipoSociedad = document.getElementById('idTipoSociedad')?.value || '';
    const UbicacionSociedad = document.getElementById('idUbicacionSociedad')?.value || '';

    const CedulaJuridicaSociedad = document.getElementById('idCedulaJuridicaSociedad').value;
    const AcreedorNombre = document.getElementById('idAcreedorNombre').value;
    const CedulaJuridicaAcreedor = document.getElementById('idCedulaJuridicaAcreedor').value;
    const AcreedorDomicilio = document.getElementById('idAcreedorDomicilio').value;
    const FechaVencimiento = document.getElementById('idFechaVencimiento').value;
    const InteresFormula = document.getElementById('idInteresFormula').value;

    const tasaInput = document.getElementById('idInteresTasaActual');
    let InteresTasaActual = tasaInput?.value || '';
    InteresTasaActual = InteresTasaActual.replace(',', '.');

    const InteresBase = document.getElementById('idInteresBase').value;
    const LugarPago = document.getElementById('distrito').value;
    const CedulaFiador = document.getElementById('idCedulaFiador').value;
    const UbicacionFirma = document.getElementById('distrito2').value;

    
    const Moneda = document.getElementById('idMoneda')?.value || 'USD';     
    const TipoCambio = document.getElementById('idTipoCambio')?.value || ''; 

    const timestamp = new Date().getTime(); 

    const url =
        `/TDocsPagares/PrevisualizarPDF?` +
        `idDocumento=0&` +
        `montoNumerico=${encodeURIComponent(MontoNumerico)}&` +
        `cedulaDeudor=${encodeURIComponent(CedulaDeudor)}&` +
        `sociedadDeudor=${encodeURIComponent(SociedadDeudor)}&` +
        `cedulaJuridicaSociedad=${encodeURIComponent(CedulaJuridicaSociedad)}&` +
        `acreedorNombre=${encodeURIComponent(AcreedorNombre)}&` +
        `cedulaJuridicaAcreedor=${encodeURIComponent(CedulaJuridicaAcreedor)}&` +
        `acreedorDomicilio=${encodeURIComponent(AcreedorDomicilio)}&` +
        `fechaVencimiento=${encodeURIComponent(FechaVencimiento)}&` +
        `interesFormula=${encodeURIComponent(InteresFormula)}&` +
        `interesTasaActual=${encodeURIComponent(InteresTasaActual)}&` +
        `interesBase=${encodeURIComponent(InteresBase)}&` +
        `lugarPago=${encodeURIComponent(LugarPago)}&` +
        `cedulaFiador=${encodeURIComponent(CedulaFiador)}&` +
        `ubicacionFirma=${encodeURIComponent(UbicacionFirma)}&` +

        `TipoSociedad=${encodeURIComponent(TipoSociedad)}&` +
        `UbicacionSociedad=${encodeURIComponent(UbicacionSociedad)}&` +

        `moneda=${encodeURIComponent(Moneda)}&` +
        `tipoCambio=${encodeURIComponent(TipoCambio)}&` +
        `t=${timestamp}`;

    window.open(url, '_blank');
});