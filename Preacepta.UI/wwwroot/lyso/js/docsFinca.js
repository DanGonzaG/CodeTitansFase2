// Mostrar el botón de "Crear Documento"
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsCompraventaFincas').addEventListener('submit', (e) => {

});

// Función para abrir el PDF de previsualización en una nueva ventana
document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const numeroEscritura = document.getElementById('idNumeroEscritura').value;
    const cedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const cedulaVendedor = document.getElementById('idCedulaVendedor').value;
    const cedulaComprador = document.getElementById('idCedulaComprador').value;
    const montoVenta = document.getElementById('idMontoVenta').value;
    const partidoFinca = document.getElementById('idPartidoFinca').value;
    const matriculaFinca = document.getElementById('idMatriculaFinca').value;
    const naturalezaFinca = document.getElementById('idNaturalezaFinca').value;
    const distritoFinca = document.getElementById('idDistritoFinca').value;
    const cantonFinca = document.getElementById('idCantonFinca').value;
    const provinciaFinca = document.getElementById('idProvinciaFinca').value;
    const areaFincaM2 = document.getElementById('idAreaFincaM2').value;
    const planoCatastrado = document.getElementById('idPlanoCatastrado').value;
    const colindaNorte = document.getElementById('idColindaNorte').value;
    const colindaSur = document.getElementById('idColindaSur').value;
    const colindaEste = document.getElementById('idColindaEste').value;
    const colindaOeste = document.getElementById('idColindaOeste').value;
    const formaPago = document.getElementById('idFormaPago').value;
    const medioPago = document.getElementById('idMedioPago').value;
    const origenFondos = document.getElementById('idOrigenFondos').value;
    const lugarFirma = document.getElementById('idLugarFirma').value;
    const horaFirma = document.getElementById('idHoraFirma').value;
    const fechaFirma = document.getElementById('idFechaFirma').value;

    const timestamp = new Date().getTime();

    const url = `/DocsCompraventaFincas/PrevisualizarPDFCompraventaFinca?` +
        `&numeroEscritura=${encodeURIComponent(numeroEscritura)}` +
        `&cedulaAbogado=${encodeURIComponent(cedulaAbogado)}` +
        `&cedulaVendedor=${encodeURIComponent(cedulaVendedor)}` +
        `&cedulaComprador=${encodeURIComponent(cedulaComprador)}` +
        `&montoVenta=${encodeURIComponent(montoVenta)}` +
        `&partidoFinca=${encodeURIComponent(partidoFinca)}` +
        `&matriculaFinca=${encodeURIComponent(matriculaFinca)}` +
        `&naturalezaFinca=${encodeURIComponent(naturalezaFinca)}` +
        `&distritoFinca=${encodeURIComponent(distritoFinca)}` +
        `&cantonFinca=${encodeURIComponent(cantonFinca)}` +
        `&provinciaFinca=${encodeURIComponent(provinciaFinca)}` +
        `&areaFincaM2=${encodeURIComponent(areaFincaM2)}` +
        `&planoCatastrado=${encodeURIComponent(planoCatastrado)}` +
        `&colindaNorte=${encodeURIComponent(colindaNorte)}` +
        `&colindaSur=${encodeURIComponent(colindaSur)}` +
        `&colindaEste=${encodeURIComponent(colindaEste)}` +
        `&colindaOeste=${encodeURIComponent(colindaOeste)}` +
        `&formaPago=${encodeURIComponent(formaPago)}` +
        `&medioPago=${encodeURIComponent(medioPago)}` +
        `&origenFondos=${encodeURIComponent(origenFondos)}` +
        `&lugarFirma=${encodeURIComponent(lugarFirma)}` +
        `&horaFirma=${encodeURIComponent(horaFirma)}` +
        `&fechaFirma=${encodeURIComponent(fechaFirma)}`;

    window.open(url, '_blank');
});
