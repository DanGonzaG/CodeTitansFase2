//Vista Formulario documento prestacion de servicios
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'none';

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
document.getElementById('PrevioBtn').addEventListener('click', () => {
    const RazonSocialEmpresa = document.getElementById('idRazonSocialEmpresa').value;
    const Provincia = document.getElementById('idProvincia').value;
    const CedulaJuridicaEmpresa = document.getElementById('idCedulaJuridicaEmpresa').value;
    const CedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const CedulaCliente = document.getElementById('idCedulaCliente').value;
    const TipoServicios = document.getElementById('idTipoServicios').value;
    const FechaInicio = document.getElementById('idFechaInicio').value;
    const FechaFinal = document.getElementById('idFechaFinal').value;
    const MontoHonorarios = document.getElementById('idMontoHonorarios').value;
    const InformacionConfidencial = document.getElementById('idInformacionConfidencial').value;
    const CiudadFirma = document.getElementById('idCiudadFirma').value;
    const HoraFirma = document.getElementById('idHoraFirma').value;
    const FechaFirma = document.getElementById('idFechaFirma').value;


    // Actualiza el contenido del modal
    document.getElementById('previoRazonSocialEmpresa').textContent = RazonSocialEmpresa;
    document.getElementById('previoProvincia').textContent = Provincia;
    document.getElementById('previoCedulaJuridicaEmpresa').textContent = CedulaJuridicaEmpresa;
    document.getElementById('previoCedulaAbogado').textContent = CedulaAbogado;
    document.getElementById('previoCedulaCliente').textContent = CedulaCliente;
    document.getElementById('previoTipoServicios').textContent = TipoServicios;
    document.getElementById('previoFechaInicio').textContent = FechaInicio;
    document.getElementById('previoFechaFinal').textContent = FechaFinal;
    document.getElementById('previoMontoHonorarios').textContent = MontoHonorarios;
    document.getElementById('previoInformacionConfidencial').textContent = InformacionConfidencial;
    document.getElementById('previoCiudadFirma').textContent = CiudadFirma;
    document.getElementById('previoHoraFirma').textContent = HoraFirma;
    document.getElementById('previoFechaFirma').textContent = FechaFirma;

    // Muestra el modal usando Bootstrap
    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
    previewModal.show();

    document.getElementById('BtnConfirmar').addEventListener('click', () => {
        console.log('Confirmando');
        document.getElementById('idRazonSocialEmpresa').value = RazonSocialEmpresa;
        document.getElementById('idProvincia').value = Provincia;
        document.getElementById('idCedulaJuridicaEmpresa').value = CedulaJuridicaEmpresa;
        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
        document.getElementById('idCedulaCliente').value = CedulaCliente;
        document.getElementById('idTipoServicios').value = TipoServicios;
        document.getElementById('idFechaInicio').value = FechaInicio;
        document.getElementById('idFechaFinal').value = FechaFinal;
        document.getElementById('idMontoHonorarios').value = MontoHonorarios;
        document.getElementById('idInformacionConfidencial').value = InformacionConfidencial;
        document.getElementById('idCiudadFirma').value = CiudadFirma;
        document.getElementById('idHoraFirma').value = HoraFirma;
        document.getElementById('idFechaFirma').value = FechaFirma;

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
        document.getElementById('idRazonSocialEmpresa').value = RazonSocialEmpresa;
        document.getElementById('idProvincia').value = Provincia;
        document.getElementById('idCedulaJuridicaEmpresa').value = CedulaJuridicaEmpresa;
        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
        document.getElementById('idCedulaCliente').value = CedulaCliente;
        document.getElementById('idTipoServicios').value = TipoServicios;
        document.getElementById('idFechaInicio').value = FechaInicio;
        document.getElementById('idFechaFinal').value = FechaFinal;
        document.getElementById('idMontoHonorarios').value = MontoHonorarios;
        document.getElementById('idInformacionConfidencial').value = InformacionConfidencial;
        document.getElementById('idCiudadFirma').value = CiudadFirma;
        document.getElementById('idHoraFirma').value = HoraFirma;
        document.getElementById('idFechaFirma').value = FechaFirma;
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

        document.getElementById('idRazonSocialEmpresa').value = RazonSocialEmpresa;
        document.getElementById('idProvincia').value = Provincia;
        document.getElementById('idCedulaJuridicaEmpresa').value = CedulaJuridicaEmpresa;
        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
        document.getElementById('idCedulaCliente').value = CedulaCliente;
        document.getElementById('idTipoServicios').value = TipoServicios;
        document.getElementById('idFechaInicio').value = FechaInicio;
        document.getElementById('idFechaFinal').value = FechaFinal;
        document.getElementById('idMontoHonorarios').value = MontoHonorarios;
        document.getElementById('idInformacionConfidencial').value = InformacionConfidencial;
        document.getElementById('idCiudadFirma').value = CiudadFirma;
        document.getElementById('idHoraFirma').value = HoraFirma;
        document.getElementById('idFechaFirma').value = FechaFirma;
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