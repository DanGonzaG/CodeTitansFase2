// Mostrar el botón de "Crear Documento"
document.getElementById('btnCrearCaso').style.display = 'block';

// Evento para el formulario (si querés hacer algo con submit)
document.getElementById('CreateDocsInscripcionVehiculo').addEventListener('submit', (e) => {
    // Podés agregar lógica si necesitás validar antes de enviar
});

// Función para abrir el PDF de previsualización en una nueva ventana
document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const cedulaCliente = document.getElementById('idCedulaCliente').value;
    const cedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const marca = document.getElementById('idMarcaVehiculo').value;
    const estilo = document.getElementById('idEstiloVehiculo').value;
    const modelo = document.getElementById('idModeloVehiculo').value;
    const categoria = document.getElementById('idCategoria').value;
    const marcaMotor = document.getElementById('idMarcaMotor').value;
    const numeroMotor = document.getElementById('idNumeroMotor').value;
    const serieChasis = document.getElementById('idNumeroSerieChasis').value;
    const vin = document.getElementById('idVin').value;
    const anio = document.getElementById('idAnio').value;
    const carroceria = document.getElementById('idCarroceria').value;
    const pesoNeto = document.getElementById('idPesoNeto').value;
    const pesoBruto = document.getElementById('idPesoBruto').value;
    const potencia = document.getElementById('idPotencia').value;
    const color = document.getElementById('idColor').value;
    const capacidad = document.getElementById('idCapacidad').value;
    const combustible = document.getElementById('idCombustible').value;
    const cilindraje = document.getElementById('idCilindraje').value;
    const lugarFirma = document.getElementById('idLugarFirma').value;
    const fechaFirma = document.getElementById('idFechaFirma').value;

    const timestamp = new Date().getTime();

    const url = `/DocsInscripcionVehiculo/PrevisualizarPDFInscripcionVehiculo?` +
        `&cedulaCliente=${encodeURIComponent(cedulaCliente)}` +
        `&cedulaAbogado=${encodeURIComponent(cedulaAbogado)}` +
        `&marca=${encodeURIComponent(marca)}` +
        `&estilo=${encodeURIComponent(estilo)}` +
        `&modelo=${encodeURIComponent(modelo)}` +
        `&categoria=${encodeURIComponent(categoria)}` +
        `&marcaMotor=${encodeURIComponent(marcaMotor)}` +
        `&numeroMotor=${encodeURIComponent(numeroMotor)}` +
        `&serieChasis=${encodeURIComponent(serieChasis)}` +
        `&vin=${encodeURIComponent(vin)}` +
        `&anio=${encodeURIComponent(anio)}` +
        `&carroceria=${encodeURIComponent(carroceria)}` +
        `&pesoNeto=${encodeURIComponent(pesoNeto)}` +
        `&pesoBruto=${encodeURIComponent(pesoBruto)}` +
        `&potencia=${encodeURIComponent(potencia)}` +
        `&color=${encodeURIComponent(color)}` +
        `&capacidad=${encodeURIComponent(capacidad)}` +
        `&combustible=${encodeURIComponent(combustible)}` +
        `&cilindraje=${encodeURIComponent(cilindraje)}` +
        `&lugarFirma=${encodeURIComponent(lugarFirma)}` +
        `&fechaFirma=${encodeURIComponent(fechaFirma)}`;

    window.open(url, '_blank');
});
