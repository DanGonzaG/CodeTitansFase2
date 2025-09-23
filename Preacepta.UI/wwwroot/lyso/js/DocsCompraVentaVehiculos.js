document.addEventListener('DOMContentLoaded', () => {
    const $ = (id) => document.getElementById(id);
    const val = (id) => $(id)?.value ?? '';
    const fixDecimal = (id) => {
        const el = $(id);
        if (!el) return '';
        if (el.value.includes(',')) el.value = el.value.replace(',', '.');
        return el.value;
    };

    if ($('btnCrearCaso')) $('btnCrearCaso').style.display = 'block';

    $('btnPrevisualizar')?.addEventListener('click', () => {

        const LugarFirma = document.getElementById('distrito').value;

        const queryParams = new URLSearchParams({
            idDocumento: 'TEMP',
            numeroEscritura: val('idNumeroEscritura'),
            cedulaAbogado: val('idCedulaAbogado'),
            cedulaPropietario: val('idCedulaPropietario'),
            cedulaComprador: val('idCedulaComprador'),
            placaVehiculo: val('idPlacaVehiculo'),
            marcaVehiculo: val('idMarcaVehiculo'),
            tipoVehiculo: val('idTipoVehiculo'),
            modeloVehiculo: val('idModeloVehiculo'),
            carroceria: val('idCarroceria'),
            categoria: val('idCategoria'),
            chasis: val('idChasis'),
            serie: val('idSerie'),
            vin: val('idVin'),
            marcaMotor: val('idMarcaMotor'),
            numeroMotor: val('idNumeroMotor'),
            color: val('idColor'),
            combustible: val('idCombustible'),
            anio: val('idAnio'),
            capacidad: val('idCapacidad'),
            cilindraje: val('idCilindraje'),
            precio: fixDecimal('idPrecio'),
            monedaPrecio: val('idMonedaPrecio'),
            plazoOpcionAnios: val('idPlazoOpcionAnios'),
            fechaInicio: val('idFechaInicio'),
            montoSenal: fixDecimal('idMontoSenal'),
            monedaSenal: val('idMonedaSenal'),
            montoADevolver: fixDecimal('idMontoADevolver'),
            montoAPerder: fixDecimal('idMontoAPerder'),
            monedaMontoPerdido: val('idMonedaMontoPerdido'),
            gastosTraspasoPagadosPor: val('idGastosTraspasoPagadosPor'),

            LugarFirma: ($('distrito')?.value || $('inputDistrito')?.value || '')
        });

        const url = '/DocsOpcionCompraventaVehiculoes/PrevisualizarPDF?' + queryParams.toString();
        window.open(url, '_blank');
    });
});
