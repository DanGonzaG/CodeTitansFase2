//Vista Formulario compraventa de fincas

//Oculta le boton de crear caso

document.getElementById('btnCrearCaso').style.display = 'none';

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa

document.getElementById('PrevioBtn').addEventListener('click', () => {

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



    // Actualiza el contenido del modal

    document.getElementById('previoNumeroEscritura').textContent = numeroEscritura;
    document.getElementById('previoCedulaAbogado').textContent = cedulaAbogado;
    document.getElementById('previoCedulaVendedor').textContent = cedulaVendedor;
    document.getElementById('previoCedulaComprador').textContent = cedulaComprador;
    document.getElementById('previoMontoVenta').textContent = montoVenta;
    document.getElementById('previoPartidoFinca').textContent = partidoFinca;
    document.getElementById('previoMatriculaFinca').textContent = matriculaFinca;
    document.getElementById('previoNaturalezaFinca').textContent = naturalezaFinca;
    document.getElementById('previoDistritoFinca').textContent = distritoFinca;
    document.getElementById('previoCantonFinca').textContent = cantonFinca;
    document.getElementById('previoProvinciaFinca').textContent = provinciaFinca;
    document.getElementById('previoAreaFincaM2').textContent = areaFincaM2;
    document.getElementById('previoPlanoCatastrado').textContent = planoCatastrado;
    document.getElementById('previoColindaNorte').textContent = colindaNorte;
    document.getElementById('previoColindaSur').textContent = colindaSur;
    document.getElementById('previoColindaEste').textContent = colindaEste;
    document.getElementById('previoColindaOeste').textContent = colindaOeste;
    document.getElementById('previoFormaPago').textContent = formaPago;
    document.getElementById('previoMedioPago').textContent = medioPago;
    document.getElementById('previoOrigenFondos').textContent = origenFondos;
    document.getElementById('previoLugarFirma').textContent = lugarFirma;
    document.getElementById('previoHoraFirma').textContent = horaFirma;
    document.getElementById('previoFechaFirma').textContent = fechaFirma;



    // Muestra el modal usando Bootstrap

    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));

    previewModal.show();

    document.getElementById('BtnConfirmar').addEventListener('click', () => {

        console.log('Confirmando');

        document.getElementById('idNumeroEscritura').value = numeroEscritura;
        document.getElementById('idCedulaAbogado').value = cedulaAbogado;
        document.getElementById('idCedulaVendedor').value = cedulaVendedor;
        document.getElementById('idCedulaComprador').value = cedulaComprador;
        document.getElementById('idMontoVenta').value = montoVenta;
        document.getElementById('idPartidoFinca').value = partidoFinca;
        document.getElementById('idMatriculaFinca').value = matriculaFinca;
        document.getElementById('idNaturalezaFinca').value = naturalezaFinca;
        document.getElementById('idDistritoFinca').value = distritoFinca;
        document.getElementById('idCantonFinca').value = cantonFinca;
        document.getElementById('idProvinciaFinca').value = provinciaFinca;
        document.getElementById('idAreaFincaM2').value = areaFincaM2;
        document.getElementById('idPlanoCatastrado').value = planoCatastrado;
        document.getElementById('idColindaNorte').value = colindaNorte;
        document.getElementById('idColindaSur').value = colindaSur;
        document.getElementById('idColindaEste').value = colindaEste;
        document.getElementById('idColindaOeste').value = colindaOeste;
        document.getElementById('idFormaPago').value = formaPago;
        document.getElementById('idMedioPago').value = medioPago;
        document.getElementById('idOrigenFondos').value = origenFondos;
        document.getElementById('idLugarFirma').value = lugarFirma;
        document.getElementById('idHoraFirma').value = horaFirma;
        document.getElementById('idFechaFirma').value = fechaFirma;


        const previewModalInstance = bootstrap.Modal.getInstance(document.getElementById('PrevioModal'));

        if (previewModalInstance) {

            previewModalInstance.hide(); // Cerrar modal correctamente

            console.log('Cerrando modal');

        };

        const modalBackdrops = document.querySelectorAll('.modal-backdrop');

        modalBackdrops.forEach((backdrop) => {

            backdrop.parentNode.removeChild(backdrop); // Quitar el fondo

        });

        // Reactiva el scroll de la página

        document.body.style.overflow = 'auto';

        document.documentElement.style.overflow = 'auto';

        document.body.style.position = 'static'; // Asegura que el body pueda desp

        document.getElementById('PrevioBtn').style.display = 'none'; // oculata el boton de Revisar

        document.getElementById('btnCrearCaso').style.display = 'block';

    });


    document.getElementById('Retroceso').addEventListener('click', () => {

        document.getElementById('idNumeroEscritura').value = numeroEscritura;
        document.getElementById('idCedulaAbogado').value = cedulaAbogado;
        document.getElementById('idCedulaVendedor').value = cedulaVendedor;
        document.getElementById('idCedulaComprador').value = cedulaComprador;
        document.getElementById('idMontoVenta').value = montoVenta;
        document.getElementById('idPartidoFinca').value = partidoFinca;
        document.getElementById('idMatriculaFinca').value = matriculaFinca;
        document.getElementById('idNaturalezaFinca').value = naturalezaFinca;
        document.getElementById('idDistritoFinca').value = distritoFinca;
        document.getElementById('idCantonFinca').value = cantonFinca;
        document.getElementById('idProvinciaFinca').value = provinciaFinca;
        document.getElementById('idAreaFincaM2').value = areaFincaM2;
        document.getElementById('idPlanoCatastrado').value = planoCatastrado;
        document.getElementById('idColindaNorte').value = colindaNorte;
        document.getElementById('idColindaSur').value = colindaSur;
        document.getElementById('idColindaEste').value = colindaEste;
        document.getElementById('idColindaOeste').value = colindaOeste;
        document.getElementById('idFormaPago').value = formaPago;
        document.getElementById('idMedioPago').value = medioPago;
        document.getElementById('idOrigenFondos').value = origenFondos;
        document.getElementById('idLugarFirma').value = lugarFirma;
        document.getElementById('idHoraFirma').value = horaFirma;
        document.getElementById('idFechaFirma').value = fechaFirma;

        const previewModalInstance = bootstrap.Modal.getInstance(document.getElementById('PrevioModal'));

        if (previewModalInstance) {

            previewModalInstance.hide(); // Cerrar modal correctamente

        };

        const modalBackdrops = document.querySelectorAll('.modal-backdrop');

        modalBackdrops.forEach((backdrop) => {

            backdrop.parentNode.removeChild(backdrop); // Quitar el fondo

        });

        // Reactiva el scroll de la página

        document.body.style.overflow = 'auto';

        document.documentElement.style.overflow = 'auto';

        document.body.style.position = 'static'; // Asegura que el body pueda desp


        console.log('Cerrando el modal y eliminando el fondo...');




    });

    document.getElementById('Retroceso2').addEventListener('click', () => {

        document.getElementById('idNumeroEscritura').value = numeroEscritura;
        document.getElementById('idCedulaAbogado').value = cedulaAbogado;
        document.getElementById('idCedulaVendedor').value = cedulaVendedor;
        document.getElementById('idCedulaComprador').value = cedulaComprador;
        document.getElementById('idMontoVenta').value = montoVenta;
        document.getElementById('idPartidoFinca').value = partidoFinca;
        document.getElementById('idMatriculaFinca').value = matriculaFinca;
        document.getElementById('idNaturalezaFinca').value = naturalezaFinca;
        document.getElementById('idDistritoFinca').value = distritoFinca;
        document.getElementById('idCantonFinca').value = cantonFinca;
        document.getElementById('idProvinciaFinca').value = provinciaFinca;
        document.getElementById('idAreaFincaM2').value = areaFincaM2;
        document.getElementById('idPlanoCatastrado').value = planoCatastrado;
        document.getElementById('idColindaNorte').value = colindaNorte;
        document.getElementById('idColindaSur').value = colindaSur;
        document.getElementById('idColindaEste').value = colindaEste;
        document.getElementById('idColindaOeste').value = colindaOeste;
        document.getElementById('idFormaPago').value = formaPago;
        document.getElementById('idMedioPago').value = medioPago;
        document.getElementById('idOrigenFondos').value = origenFondos;
        document.getElementById('idLugarFirma').value = lugarFirma;
        document.getElementById('idHoraFirma').value = horaFirma;
        document.getElementById('idFechaFirma').value = fechaFirma;

        const previewModalInstance = bootstrap.Modal.getInstance(document.getElementById('PrevioModal'));

        if (previewModalInstance) {

            previewModalInstance.hide(); // Cerrar modal correctamente

        };

        const modalBackdrops = document.querySelectorAll('.modal-backdrop');

        modalBackdrops.forEach((backdrop) => {

            backdrop.parentNode.removeChild(backdrop); // Quitar el fondo

        });

        // Reactiva el scroll de la página

        document.body.style.overflow = 'auto';

        document.documentElement.style.overflow = 'auto';

        document.body.style.position = 'static'; // Asegura que el body pueda desp


        console.log('Cerrando el modal y eliminando el fondo...');

    });



});