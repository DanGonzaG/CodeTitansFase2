//Vista Formulario caso
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsPagares').addEventListener('submit', (e) => {
    const now = new Date();

    const fecha = now.toISOString().slice(0, 10); // yyyy-MM-dd
    const hora = now.toTimeString().slice(0, 5);   // HH:mm

    // Asignar a inputs ocultos
    document.getElementById('idFechaFirma').value = fecha;
    document.getElementById('idHoraFirma').value = hora;
});

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    /*const MontoNumerico = document.getElementById('CreateDocsPagares').addEventListener('submit', (e) => {
        const montoInput = document.getElementById('idMontoNumerico');
        if (montoInput && montoInput.value.includes(',')) {
            montoInput.value = montoInput.value.replace(',', '.');
        }
        const MontoNumerico = montoInput.value;*/

    const montoInput = document.getElementById('idMontoNumerico');
    let MontoNumerico = montoInput?.value || '';
    MontoNumerico = MontoNumerico.replace(',', '.');


    const CedulaDeudor = document.getElementById('idCedulaDeudor').value;
    const SociedadDeudor = document.getElementById('idSociedadDeudor').value;
    const CedulaJuridicaSociedad = document.getElementById('idCedulaJuridicaSociedad').value;
    const AcreedorNombre = document.getElementById('idAcreedorNombre').value;
    const CedulaJuridicaAcreedor = document.getElementById('idCedulaJuridicaAcreedor').value;
    const AcreedorDomicilio = document.getElementById('idAcreedorDomicilio').value;
    const FechaVencimiento = document.getElementById('idFechaVencimiento').value;
    const InteresFormula = document.getElementById('idInteresFormula').value;
    /*const InteresTasaActual = document.getElementById('CreateDocsPagares').addEventListener('submit', (e) => {
        const tasaInput = document.getElementById('idInteresTasaActual');
        if (tasaInput && tasaInput.value.includes(',')) {
            tasaInput.value = tasaInput.value.replace(',', '.');
        }
        const InteresTasaActual = tasaInput.value;*/

    const tasaInput = document.getElementById('idInteresTasaActual');
    let InteresTasaActual = tasaInput?.value || '';
    InteresTasaActual = InteresTasaActual.replace(',', '.');

    const InteresBase = document.getElementById('idInteresBase').value;
    const LugarPago = document.getElementById('idLugarPago').value;
    const CedulaFiador = document.getElementById('idCedulaFiador').value;
    const UbicacionFirma = document.getElementById('idUbicacionFirma').value;

    // ✅ CORREGIDO: Tomar directamente lo que el usuario escribió en los inputs
    /*const fechaFirmaNuevo = document.getElementById('idFechaFirma').value;
    const horaFirmaNuevo = document.getElementById('idHoraFirma').value;*/
    const timestamp = new Date().getTime(); // Para evitar cacheo

    const url = `/TDocsPagares/PrevisualizarPDF?` +
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
        /*`fechaFirmaNuevo=${encodeURIComponent(fechaFirmaNuevo)}&` +
        `horaFirmaNuevo=${encodeURIComponent(horaFirmaNuevo)}&` +*/
        `t=${timestamp}`;

    window.open(url, '_blank');
});


/*document.getElementById('idHoraFirma').addEventListener('input', function () {
    const valor = this.value;
    if (valor) {
        const [hour, minute] = valor.split(':');
        const hora = parseInt(hour, 10);

        const ampm = hora >= 12 ? 'PM' : 'AM';
        document.getElementById('horaAmPm').textContent = ampm;
    } else {
        document.getElementById('horaAmPm').textContent = '';
    }
});*/



//    // Actualiza el contenido del modal
//    document.getElementById('previoMontoNumerico').textContent = MontoNumerico;
//    document.getElementById('previoCedulaDeudor').textContent = CedulaDeudor;
//    document.getElementById('previoSociedadDeudor').textContent = SociedadDeudor;
//    document.getElementById('previoCedulaJuridicaSociedad').textContent = CedulaJuridicaSociedad;
//    document.getElementById('previoAcreedorNombre').textContent = AcreedorNombre;
//    document.getElementById('previoCedulaJuridicaAcreedor').textContent = CedulaJuridicaAcreedor;
//    document.getElementById('previoAcreedorDomicilio').textContent = AcreedorDomicilio;
//    document.getElementById('previoFechaVencimiento').textContent = FechaVencimiento;
//    document.getElementById('previoInteresFormula').textContent = InteresFormula;
//    document.getElementById('previoInteresTasaActual').textContent = InteresTasaActual;
//    document.getElementById('previoInteresBase').textContent = InteresBase;
//    document.getElementById('previoLugarPago').textContent = LugarPago;
//    document.getElementById('previoCedulaFiador').textContent = CedulaFiador;
//    document.getElementById('previoUbicacionFirma').textContent = UbicacionFirma;

//    // Muestra el modal usando Bootstrap
//    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
//    previewModal.show();

//    document.getElementById('BtnConfirmar').addEventListener('click', () => {
//        console.log('Confirmando');
//        document.getElementById('previoMontoNumerico').value = MontoNumerico;
//        document.getElementById('previoCedulaDeudor').value = CedulaDeudor;
//        document.getElementById('previoSociedadDeudor').value = SociedadDeudor;
//        document.getElementById('previoCedulaJuridicaSociedad').value = CedulaJuridicaSociedad;
//        document.getElementById('previoAcreedorNombre').value = AcreedorNombre;
//        document.getElementById('previoCedulaJuridicaAcreedor').value = CedulaJuridicaAcreedor;
//        document.getElementById('previoAcreedorDomicilio').value = AcreedorDomicilio;
//        document.getElementById('previoFechaVencimiento').value = FechaVencimiento;
//        document.getElementById('previoInteresFormula').value = InteresFormula;
//        document.getElementById('previoInteresTasaActual').value = InteresTasaActual;
//        document.getElementById('previoInteresBase').value = InteresBase;
//        document.getElementById('previoLugarPago').value = LugarPago;
//        document.getElementById('previoCedulaFiador').value = CedulaFiador;
//        document.getElementById('previoUbicacionFirma').value = UbicacionFirma;
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
//        document.getElementById('previoMontoNumerico').value = MontoNumerico;
//        document.getElementById('previoCedulaDeudor').value = CedulaDeudor;
//        document.getElementById('previoSociedadDeudor').value = SociedadDeudor;
//        document.getElementById('previoCedulaJuridicaSociedad').value = CedulaJuridicaSociedad;
//        document.getElementById('previoAcreedorNombre').value = AcreedorNombre;
//        document.getElementById('previoCedulaJuridicaAcreedor').value = CedulaJuridicaAcreedor;
//        document.getElementById('previoAcreedorDomicilio').value = AcreedorDomicilio;
//        document.getElementById('previoFechaVencimiento').value = FechaVencimiento;
//        document.getElementById('previoInteresFormula').value = InteresFormula;
//        document.getElementById('previoInteresTasaActual').value = InteresTasaActual;
//        document.getElementById('previoInteresBase').value = InteresBase;
//        document.getElementById('previoLugarPago').value = LugarPago;
//        document.getElementById('previoCedulaFiador').value = CedulaFiador;
//        document.getElementById('previoUbicacionFirma').value = UbicacionFirma;
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

//        document.getElementById('previoMontoNumerico').value = MontoNumerico;
//        document.getElementById('previoCedulaDeudor').value = CedulaDeudor;
//        document.getElementById('previoSociedadDeudor').value = SociedadDeudor;
//        document.getElementById('previoCedulaJuridicaSociedad').value = CedulaJuridicaSociedad;
//        document.getElementById('previoAcreedorNombre').value = AcreedorNombre;
//        document.getElementById('previoCedulaJuridicaAcreedor').value = CedulaJuridicaAcreedor;
//        document.getElementById('previoAcreedorDomicilio').value = AcreedorDomicilio;
//        document.getElementById('previoFechaVencimiento').value = FechaVencimiento;
//        document.getElementById('previoInteresFormula').value = InteresFormula;
//        document.getElementById('previoInteresTasaActual').value = InteresTasaActual;
//        document.getElementById('previoInteresBase').value = InteresBase;
//        document.getElementById('previoLugarPago').value = LugarPago;
//        document.getElementById('previoCedulaFiador').value = CedulaFiador;
//        document.getElementById('previoUbicacionFirma').value = UbicacionFirma;
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


//});