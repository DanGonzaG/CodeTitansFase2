document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsOpcionCompraventaVehiculoes').addEventListener('submit', (e) => {
    const now = new Date();
    const fecha = now.toISOString().slice(0, 10);
    const hora = now.toTimeString().slice(0, 5);
    document.getElementById('idFechaFirma').value = fecha;
    document.getElementById('idHoraFirma').value = hora;
});

document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const NumeroEscritura = document.getElementById('idNumeroEscritura').value;
    const CedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const CedulaPropietario = document.getElementById('idCedulaPropietario').value;
    const CedulaComprador = document.getElementById('idCedulaComprador').value;

    const PlacaVehiculo = document.getElementById('idPlacaVehiculo').value;
    const MarcaVehiculo = document.getElementById('idMarcaVehiculo').value;
    const TipoVehiculo = document.getElementById('idTipoVehiculo').value;
    const ModeloVehiculo = document.getElementById('idModeloVehiculo').value;
    const Carroceria = document.getElementById('idCarroceria').value;
    const Categoria = document.getElementById('idCategoria').value;
    const Chasis = document.getElementById('idChasis').value;
    const Serie = document.getElementById('idSerie').value;
    const Vin = document.getElementById('idVin').value;
    const MarcaMotor = document.getElementById('idMarcaMotor').value;
    const NumeroMotor = document.getElementById('idNumeroMotor').value;
    const Color = document.getElementById('idColor').value;
    const Combustible = document.getElementById('idCombustible').value;
    const Anio = document.getElementById('idAnio').value;
    const Capacidad = document.getElementById('idCapacidad').value;
    const Cilindraje = document.getElementById('idCilindraje').value;

    const precioInput = document.getElementById('idPrecio');
    let Precio = precioInput?.value || '';
    Precio = Precio.replace(',', '.');

    const MonedaPrecio = document.getElementById('idMonedaPrecio').value;
    const PlazoOpcionAnios = document.getElementById('idPlazoOpcionAnios').value;
    const FechaInicio = document.getElementById('idFechaInicio').value;

    const senalInput = document.getElementById('idMontoSenal');
    let MontoSenal = senalInput?.value || '';
    MontoSenal = MontoSenal.replace(',', '.');

    const MonedaSenal = document.getElementById('idMonedaSenal').value;

    const devolverInput = document.getElementById('idMontoADevolver');
    let MontoADevolver = devolverInput?.value || '';
    MontoADevolver = MontoADevolver.replace(',', '.');

    const perderInput = document.getElementById('idMontoAPerder');
    let MontoAPerder = perderInput?.value || '';
    MontoAPerder = MontoAPerder.replace(',', '.');

    const MonedaMontoPerdido = document.getElementById('idMonedaMontoPerdido').value;
    const GastosTraspasoPagadosPor = document.getElementById('idGastosTraspasoPagadosPor').value;

    const LugarFirma = document.getElementById('distrito').value;

    const timestamp = new Date().getTime();

    const url =
        /DocsOpcionCompraventaVehiculoes/PrevisualizarPDF? +
        idDocumento=0& +
        numeroEscritura=${encodeURIComponent(NumeroEscritura)}& +
        cedulaAbogado=${encodeURIComponent(CedulaAbogado)}& +
        cedulaPropietario=${encodeURIComponent(CedulaPropietario)}& +
        cedulaComprador=${encodeURIComponent(CedulaComprador)}& +

        placaVehiculo=${encodeURIComponent(PlacaVehiculo)}& +
        marcaVehiculo=${encodeURIComponent(MarcaVehiculo)}& +
        tipoVehiculo=${encodeURIComponent(TipoVehiculo)}& +
        modeloVehiculo=${encodeURIComponent(ModeloVehiculo)}& +
        carroceria=${encodeURIComponent(Carroceria)}& +
        categoria=${encodeURIComponent(Categoria)}& +
        chasis=${encodeURIComponent(Chasis)}& +
        serie=${encodeURIComponent(Serie)}& +
        vin=${encodeURIComponent(Vin)}& +
        marcaMotor=${encodeURIComponent(MarcaMotor)}& +
        numeroMotor=${encodeURIComponent(NumeroMotor)}& +
        color=${encodeURIComponent(Color)}& +
        combustible=${encodeURIComponent(Combustible)}& +
        anio=${encodeURIComponent(Anio)}& +
        capacidad=${encodeURIComponent(Capacidad)}& +
        cilindraje=${encodeURIComponent(Cilindraje)}& +

        precio=${encodeURIComponent(Precio)}& +
        monedaPrecio=${encodeURIComponent(MonedaPrecio)}& +
        plazoOpcionAnios=${encodeURIComponent(PlazoOpcionAnios)}& +
        fechaInicio=${encodeURIComponent(FechaInicio)}& +

        montoSenal=${encodeURIComponent(MontoSenal)}& +
        monedaSenal=${encodeURIComponent(MonedaSenal)}& +
        montoADevolver=${encodeURIComponent(MontoADevolver)}& +
        montoAPerder=${encodeURIComponent(MontoAPerder)}& +
        monedaMontoPerdido=${encodeURIComponent(MonedaMontoPerdido)}& +
        gastosTraspasoPagadosPor=${encodeURIComponent(GastosTraspasoPagadosPor)}& +

        LugarFirma=${encodeURIComponent(LugarFirma)}& +
        t=${timestamp};

    window.open(url, '_blank');
});