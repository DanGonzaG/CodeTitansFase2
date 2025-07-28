// Mostrar el botón de "Crear Documento"
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsContratoPrestacionServicios').addEventListener('submit', (e) => {
});

// Función para abrir el PDF de previsualización en una nueva ventana
document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const razonSocialEmpresa = document.getElementById('idRazonSocialEmpresa').value;
    const provincia = document.getElementById('idProvincia').value;
    const cedulaJuridicaEmpresa = document.getElementById('idCedulaJuridicaEmpresa').value;
    const cedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const cedulaCliente = document.getElementById('idCedulaCliente').value;
    const tipoServicios = document.getElementById('idTipoServicios').value;
    const fechaInicio = document.getElementById('idFechaInicio').value;
    const fechaFinal = document.getElementById('idFechaFinal').value;
    const montoHonorarios = document.getElementById('idMontoHonorarios').value;
    const informacionConfidencial = document.getElementById('idInformacionConfidencial').value;
    const ciudadFirma = document.getElementById('idCiudadFirma').value;
    const horaFirma = document.getElementById('idHoraFirma').value;
    const fechaFirma = document.getElementById('idFechaFirma').value;

    const timestamp = new Date().getTime();

    const url = `/DocsContratoPrestacionServicios/PrevisualizarPDFPrestacionServicios?` +
        `razonSocialEmpresa=${encodeURIComponent(razonSocialEmpresa)}` +
        `&provincia=${encodeURIComponent(provincia)}` +
        `&cedulaJuridicaEmpresa=${encodeURIComponent(cedulaJuridicaEmpresa)}` +
        `&cedulaAbogado=${encodeURIComponent(cedulaAbogado)}` +
        `&cedulaCliente=${encodeURIComponent(cedulaCliente)}` +
        `&tipoServicios=${encodeURIComponent(tipoServicios)}` +
        `&fechaInicio=${encodeURIComponent(fechaInicio)}` +
        `&fechaFinal=${encodeURIComponent(fechaFinal)}` +
        `&montoHonorarios=${encodeURIComponent(montoHonorarios)}` +
        `&informacionConfidencial=${encodeURIComponent(informacionConfidencial)}` +
        `&ciudadFirma=${encodeURIComponent(ciudadFirma)}` +
        `&horaFirma=${encodeURIComponent(horaFirma)}` +
        `&fechaFirma=${encodeURIComponent(fechaFirma)}`;

    window.open(url, '_blank');
});
