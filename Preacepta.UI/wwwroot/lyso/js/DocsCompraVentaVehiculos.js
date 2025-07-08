//Vista Formulario caso
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsOpcionCompraventaVehiculoes').addEventListener('submit', (e) => {

});

// Función para abrir el PDF de previsualización en una nueva ventana
document.getElementById("btnPrevisualizar").addEventListener("click", function () {
    const queryParams = new URLSearchParams({
        idDocumento: "TEMP",
        numeroEscritura: document.getElementById("idNumeroEscritura").value,
        cedulaAbogado: document.getElementById("idCedulaAbogado").value,
        cedulaPropietario: document.getElementById("idCedulaPropietario").value,
        cedulaComprador: document.getElementById("idCedulaComprador").value,
        placaVehiculo: document.getElementById("idPlacaVehiculo").value,
        marcaVehiculo: document.getElementById("idMarcaVehiculo").value,
        tipoVehiculo: document.getElementById("idTipoVehiculo").value,
        modeloVehiculo: document.getElementById("idModeloVehiculo").value,
        carroceria: document.getElementById("idCarroceria").value,
        categoria: document.getElementById("idCategoria").value,
        chasis: document.getElementById("idChasis").value,
        serie: document.getElementById("idSerie").value,
        vin: document.getElementById("idVin").value,
        marcaMotor: document.getElementById("idMarcaMotor").value,
        numeroMotor: document.getElementById("idNumeroMotor").value,
        color: document.getElementById("idColor").value,
        combustible: document.getElementById("idCombustible").value,
        anio: document.getElementById("idAnio").value,
        capacidad: document.getElementById("idCapacidad").value,
        cilindraje: document.getElementById("idCilindraje").value,
        precio: document.getElementById("idPrecio").value,
        monedaPrecio: document.getElementById("idMonedaPrecio").value,
        plazoOpcionAnios: document.getElementById("idPlazoOpcionAnios").value,
        fechaInicio: document.getElementById("idFechaInicio").value,
        montoSenal: document.getElementById("idMontoSenal").value,
        monedaSenal: document.getElementById("idMonedaSenal").value,
        montoADevolver: document.getElementById("idMontoADevolver").value,
        montoAPerder: document.getElementById("idMontoAPerder").value,
        monedaMontoPerdido: document.getElementById("idMonedaMontoPerdido").value,
        gastosTraspasoPagadosPor: document.getElementById("idGastosTraspasoPagadosPor").value,
        lugarFirma: document.getElementById("idLugarFirma").value,
        horaFirma: document.getElementById("idHoraFirma").value,
        fechaFirma: document.getElementById("idFechaFirma").value
    });

    const url = "/DocsOpcionCompraventaVehiculoes/PrevisualizarPDF?" + queryParams.toString();
    window.open(url, "_blank");
});










//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
//document.getElementById('PrevioBtn').addEventListener('click', () => {
//    const CedulaAbogado = document.getElementById('idCedulaAbogado').value;
//    const CedulaPropietario = document.getElementById('idCedulaPropietario').value;
//    const CedulaComprador = document.getElementById('idCedulaComprador').value;
//    const PlacaVehiculo = document.getElementById('idPlacaVehiculo').value;
//    const MarcaVehiculo = document.getElementById('idMarcaVehiculo').value;
//    const TipoVehiculo = document.getElementById('idTipoVehiculo').value;
//    const ModeloVehiculo = document.getElementById('idModeloVehiculo').value;
//    const Carroceria = document.getElementById('idCarroceria').value;
//    const Categoria = document.getElementById('idCategoria').value;
//    const Chasis = document.getElementById('idChasis').value;
//    const Serie = document.getElementById('idSerie').value;
//    const Vin = document.getElementById('idVin').value;
//    const MarcaMotor = document.getElementById('idMarcaMotor').value;
//    const NumeroMotor = document.getElementById('idNumeroMotor').value;
//    const Color = document.getElementById('idColor').value;
//    const Combustible = document.getElementById('idCombustible').value;
//    const Anio = document.getElementById('idAnio').value;
//    const Capacidad = document.getElementById('idCapacidad').value;
//    const Cilindraje = document.getElementById('idCilindraje').value;
//    const Precio = document.getElementById('idPrecio').value;
//    const MonedaPrecio = document.getElementById('idMonedaPrecio').value;
//    const PlazoOpcionAnios = document.getElementById('idPlazoOpcionAnios').value;
//    const FechaInicio = document.getElementById('idFechaInicio').value;
//    const MontoSenal = document.getElementById('idMontoSenal').value;
//    const MonedaSenal = document.getElementById('idMonedaSenal').value;
//    const MontoADevolver = document.getElementById('idMontoADevolver').value;
//    const MontoAPerder = document.getElementById('idMontoAPerder').value;
//    const MonedaMontoPerdido = document.getElementById('idMonedaMontoPerdido').value;
//    const GastosTraspasoPagadosPor = document.getElementById('idGastosTraspasoPagadosPor').value;
//    const LugarFirma = document.getElementById('idLugarFirma').value;
//    const HoraFirma = document.getElementById('idHoraFirma').value;
//    const FechaFirma = document.getElementById('idFechaFirma').value;


//    // Actualiza el contenido del modal
//    document.getElementById('previoCedulaAbogado').textContent = CedulaAbogado;
//    document.getElementById('previoCedulaPropietario').textContent = CedulaPropietario;
//    document.getElementById('previoCedulaComprador').textContent = CedulaComprador;
//    document.getElementById('previoPlacaVehiculo').textContent = PlacaVehiculo;
//    document.getElementById('previoMarcaVehiculo').textContent = MarcaVehiculo;
//    document.getElementById('previoTipoVehiculo').textContent = TipoVehiculo;
//    document.getElementById('previoModeloVehiculo').textContent = ModeloVehiculo;
//    document.getElementById('previoCarroceria').textContent = Carroceria;
//    document.getElementById('previoCategoria').textContent = Categoria;
//    document.getElementById('previoChasis').textContent = Chasis;
//    document.getElementById('previoSerie').textContent = Serie;
//    document.getElementById('previoVin').textContent = Vin;
//    document.getElementById('previoMarcaMotor').textContent = MarcaMotor;
//    document.getElementById('previoNumeroMotor').textContent = NumeroMotor;
//    document.getElementById('previoColor').textContent = Color;
//    document.getElementById('previoCombustible').textContent = Combustible;
//    document.getElementById('previoAnio').textContent = Anio;
//    document.getElementById('previoCapacidad').textContent = Capacidad;
//    document.getElementById('previoCilindraje').textContent = Cilindraje;
//    document.getElementById('previoPrecio').textContent = Precio;
//    document.getElementById('previoMonedaPrecio').textContent = MonedaPrecio;
//    document.getElementById('previoPlazoOpcionAnios').textContent = PlazoOpcionAnios;
//    document.getElementById('previoFechaInicio').textContent = FechaInicio;
//    document.getElementById('previoMontoSenal').textContent = MontoSenal;
//    document.getElementById('previoMonedaSenal').textContent = MonedaSenal;
//    document.getElementById('previoMontoADevolver').textContent = MontoADevolver;
//    document.getElementById('previoMontoAPerder').textContent = MontoAPerder;
//    document.getElementById('previoMonedaMontoPerdido').textContent = MonedaMontoPerdido;
//    document.getElementById('previoGastosTraspasoPagadosPor').textContent = GastosTraspasoPagadosPor;
//    document.getElementById('previoLugarFirma').textContent = LugarFirma;
//    document.getElementById('previoHoraFirma').textContent = HoraFirma;
//    document.getElementById('previoFechaFirma').textContent = FechaFirma;

//    // Muestra el modal usando Bootstrap
//    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
//    previewModal.show();

//    document.getElementById('BtnConfirmar').addEventListener('click', () => {
//        console.log('Confirmando');
//        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
//        document.getElementById('idCedulaPropietario').value = CedulaPropietario;
//        document.getElementById('idCedulaComprador').value = CedulaComprador;
//        document.getElementById('idPlacaVehiculo').value = PlacaVehiculo;
//        document.getElementById('idMarcaVehiculo').value = MarcaVehiculo;
//        document.getElementById('idTipoVehiculo').value = TipoVehiculo;
//        document.getElementById('idModeloVehiculo').value = ModeloVehiculo;
//        document.getElementById('idCarroceria').value = Carroceria;
//        document.getElementById('idCategoria').value = Categoria;
//        document.getElementById('idChasis').value = Chasis;
//        document.getElementById('idSerie').value = Serie;
//        document.getElementById('idVin').value = Vin;
//        document.getElementById('idMarcaMotor').value = MarcaMotor;
//        document.getElementById('idNumeroMotor').value = NumeroMotor;
//        document.getElementById('idColor').value = Color;
//        document.getElementById('idCombustible').value = Combustible;
//        document.getElementById('idAnio').value = Anio;
//        document.getElementById('idCapacidad').value = Capacidad;
//        document.getElementById('idCilindraje').value = Cilindraje;
//        document.getElementById('idPrecio').value = Precio;
//        document.getElementById('idMonedaPrecio').value = MonedaPrecio;
//        document.getElementById('idPlazoOpcionAnios').value = PlazoOpcionAnios;
//        document.getElementById('idFechaInicio').value = FechaInicio;
//        document.getElementById('idMontoSenal').value = MontoSenal;
//        document.getElementById('idMonedaSenal').value = MonedaSenal;
//        document.getElementById('idMontoADevolver').value = MontoADevolver;
//        document.getElementById('idMontoAPerder').value = MontoAPerder;
//        document.getElementById('idMonedaMontoPerdido').value = MonedaMontoPerdido;
//        document.getElementById('idGastosTraspasoPagadosPor').value = GastosTraspasoPagadosPor;
//        document.getElementById('idLugarFirma').value = LugarFirma;
//        document.getElementById('idHoraFirma').value = HoraFirma;
//        document.getElementById('idFechaFirma').value = FechaFirma;
//        const previewModalInstance = bootstrap.Modal.getInstance(document.getElementById('PrevioModal'));
//        if (previewModalInstance) {
//            previewModalInstance.hide(); // Cerrar modal correctamente
//            console.log('Cerrando modal');
//        };
//        const modalBackdrops = document.querySelectorAll('.modal-backdrop');
//        modalBackdrops.forEach((backdrop) => {
//            backdrop.parentNode.removeChild(backdrop); // Quitar el fondo
//        });
//        // Reactiva el scroll de la página
//        document.body.style.overflow = 'auto';
//        document.documentElement.style.overflow = 'auto';
//        document.body.style.position = 'static'; // Asegura que el body pueda desp

//        document.getElementById('PrevioBtn').style.display = 'none'; // oculata el boton de Revisar
//        document.getElementById('btnCrearCaso').style.display = 'block';
//    });


//    document.getElementById('Retroceso').addEventListener('click', () => {
//        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
//        document.getElementById('idCedulaPropietario').value = CedulaPropietario;
//        document.getElementById('idCedulaComprador').value = CedulaComprador;
//        document.getElementById('idPlacaVehiculo').value = PlacaVehiculo;
//        document.getElementById('idMarcaVehiculo').value = MarcaVehiculo;
//        document.getElementById('idTipoVehiculo').value = TipoVehiculo;
//        document.getElementById('idModeloVehiculo').value = ModeloVehiculo;
//        document.getElementById('idCarroceria').value = Carroceria;
//        document.getElementById('idCategoria').value = Categoria;
//        document.getElementById('idChasis').value = Chasis;
//        document.getElementById('idSerie').value = Serie;
//        document.getElementById('idVin').value = Vin;
//        document.getElementById('idMarcaMotor').value = MarcaMotor;
//        document.getElementById('idNumeroMotor').value = NumeroMotor;
//        document.getElementById('idColor').value = Color;
//        document.getElementById('idCombustible').value = Combustible;
//        document.getElementById('idAnio').value = Anio;
//        document.getElementById('idCapacidad').value = Capacidad;
//        document.getElementById('idCilindraje').value = Cilindraje;
//        document.getElementById('idPrecio').value = Precio;
//        document.getElementById('idMonedaPrecio').value = MonedaPrecio;
//        document.getElementById('idPlazoOpcionAnios').value = PlazoOpcionAnios;
//        document.getElementById('idFechaInicio').value = FechaInicio;
//        document.getElementById('idMontoSenal').value = MontoSenal;
//        document.getElementById('idMonedaSenal').value = MonedaSenal;
//        document.getElementById('idMontoADevolver').value = MontoADevolver;
//        document.getElementById('idMontoAPerder').value = MontoAPerder;
//        document.getElementById('idMonedaMontoPerdido').value = MonedaMontoPerdido;
//        document.getElementById('idGastosTraspasoPagadosPor').value = GastosTraspasoPagadosPor;
//        document.getElementById('idLugarFirma').value = LugarFirma;
//        document.getElementById('idHoraFirma').value = HoraFirma;
//        document.getElementById('idFechaFirma').value = FechaFirma;
//        const previewModalInstance = bootstrap.Modal.getInstance(document.getElementById('PrevioModal'));
//        if (previewModalInstance) {
//            previewModalInstance.hide(); // Cerrar modal correctamente
//        };

//        const modalBackdrops = document.querySelectorAll('.modal-backdrop');
//        modalBackdrops.forEach((backdrop) => {
//            backdrop.parentNode.removeChild(backdrop); // Quitar el fondo
//        });

//        // Reactiva el scroll de la página
//        document.body.style.overflow = 'auto';
//        document.documentElement.style.overflow = 'auto';
//        document.body.style.position = 'static'; // Asegura que el body pueda desp


//        console.log('Cerrando el modal y eliminando el fondo...');




//    });

//    document.getElementById('Retroceso2').addEventListener('click', () => {

//        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
//        document.getElementById('idCedulaPropietario').value = CedulaPropietario;
//        document.getElementById('idCedulaComprador').value = CedulaComprador;
//        document.getElementById('idPlacaVehiculo').value = PlacaVehiculo;
//        document.getElementById('idMarcaVehiculo').value = MarcaVehiculo;
//        document.getElementById('idTipoVehiculo').value = TipoVehiculo;
//        document.getElementById('idModeloVehiculo').value = ModeloVehiculo;
//        document.getElementById('idCarroceria').value = Carroceria;
//        document.getElementById('idCategoria').value = Categoria;
//        document.getElementById('idChasis').value = Chasis;
//        document.getElementById('idSerie').value = Serie;
//        document.getElementById('idVin').value = Vin;
//        document.getElementById('idMarcaMotor').value = MarcaMotor;
//        document.getElementById('idNumeroMotor').value = NumeroMotor;
//        document.getElementById('idColor').value = Color;
//        document.getElementById('idCombustible').value = Combustible;
//        document.getElementById('idAnio').value = Anio;
//        document.getElementById('idCapacidad').value = Capacidad;
//        document.getElementById('idCilindraje').value = Cilindraje;
//        document.getElementById('idPrecio').value = Precio;
//        document.getElementById('idMonedaPrecio').value = MonedaPrecio;
//        document.getElementById('idPlazoOpcionAnios').value = PlazoOpcionAnios;
//        document.getElementById('idFechaInicio').value = FechaInicio;
//        document.getElementById('idMontoSenal').value = MontoSenal;
//        document.getElementById('idMonedaSenal').value = MonedaSenal;
//        document.getElementById('idMontoADevolver').value = MontoADevolver;
//        document.getElementById('idMontoAPerder').value = MontoAPerder;
//        document.getElementById('idMonedaMontoPerdido').value = MonedaMontoPerdido;
//        document.getElementById('idGastosTraspasoPagadosPor').value = GastosTraspasoPagadosPor;
//        document.getElementById('idLugarFirma').value = LugarFirma;
//        document.getElementById('idHoraFirma').value = HoraFirma;
//        document.getElementById('idFechaFirma').value = FechaFirma;
//        const previewModalInstance = bootstrap.Modal.getInstance(document.getElementById('PrevioModal'));
//        if (previewModalInstance) {
//            previewModalInstance.hide(); // Cerrar modal correctamente
//        };

//        const modalBackdrops = document.querySelectorAll('.modal-backdrop');
//        modalBackdrops.forEach((backdrop) => {
//            backdrop.parentNode.removeChild(backdrop); // Quitar el fondo
//        });

//        // Reactiva el scroll de la página
//        document.body.style.overflow = 'auto';
//        document.documentElement.style.overflow = 'auto';
//        document.body.style.position = 'static'; // Asegura que el body pueda desp


//        console.log('Cerrando el modal y eliminando el fondo...');
//    });

//Para convertirt todo a comentario se preciona Ctrl + k despues c
//});