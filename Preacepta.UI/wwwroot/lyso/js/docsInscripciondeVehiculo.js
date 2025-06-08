//Vista Formulario documento de inscripcion de vehiculo
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'none';

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
document.getElementById('PrevioBtn').addEventListener('click', () => {
    const CedulaCliente = document.getElementById('idCedulaCliente').value;
    const CedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const MarcaVehiculo = document.getElementById('idMarcaVehiculo').value;
    const EstiloVehiculo = document.getElementById('idEstiloVehiculo').value;
    const ModeloVehiculo = document.getElementById('idModeloVehiculo').value;
    const Categoria = document.getElementById('idCategoria').value;
    const MarcaMotor = document.getElementById('idMarcaMotor').value;
    const NumeroMotor = document.getElementById('idNumeroMotor').value;
    const NumeroSerieChasis = document.getElementById('idNumeroSerieChasis').value;
    const Vin = document.getElementById('idVin').value;
    const Anio = document.getElementById('idAnio').value;
    const Carroceria = document.getElementById('idCarroceria').value;
    const PesoNeto = document.getElementById('idPesoNeto').value;
    const PesoBruto = document.getElementById('idPesoBruto').value;
    const Potencia = document.getElementById('idPotencia').value;
    const Color = document.getElementById('idColor').value;
    const Capacidad = document.getElementById('idCapacidad').value;
    const Combustible = document.getElementById('idCombustible').value;
    const Cilindraje = document.getElementById('idCilindraje').value;
    const LugarFirma = document.getElementById('idLugarFirma').value;
    const FechaFirma = document.getElementById('idFechaFirma').value;



    // Actualiza el contenido del modal
    document.getElementById('previoCedulaCliente').textContent = CedulaCliente;
    document.getElementById('previoCedulaAbogado').textContent = CedulaAbogado;
    document.getElementById('previoMarcaVehiculo').textContent = MarcaVehiculo;
    document.getElementById('previoEstiloVehiculo').textContent = EstiloVehiculo;
    document.getElementById('previoModeloVehiculo').textContent = ModeloVehiculo;
    document.getElementById('previoCategoria').textContent = Categoria;
    document.getElementById('previoMarcaMotor').textContent = MarcaMotor;
    document.getElementById('previoNumeroMotor').textContent = NumeroMotor;
    document.getElementById('previoNumeroSerieChasis').textContent = NumeroSerieChasis;
    document.getElementById('previoVin').textContent = Vin;
    document.getElementById('previoAnio').textContent = Anio;
    document.getElementById('previoCarroceria').textContent = Carroceria;
    document.getElementById('previoPesoNeto').textContent = PesoNeto;
    document.getElementById('previoPesoBruto').textContent = PesoBruto;
    document.getElementById('previoPotencia').textContent = Potencia;
    document.getElementById('previoColor').textContent = Color;
    document.getElementById('previoCapacidad').textContent = Capacidad;
    document.getElementById('previoCombustible').textContent = Combustible;
    document.getElementById('previoCilindraje').textContent = Cilindraje;
    document.getElementById('previoLugarFirma').textContent = LugarFirma;
    document.getElementById('previoFechaFirma').textContent = FechaFirma;


    // Muestra el modal usando Bootstrap
    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
    previewModal.show();

    document.getElementById('BtnConfirmar').addEventListener('click', () => {
        console.log('Confirmando');
        document.getElementById('idCedulaCliente').value = CedulaCliente;
        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
        document.getElementById('idMarcaVehiculo').value = MarcaVehiculo;
        document.getElementById('idEstiloVehiculo').value = EstiloVehiculo;
        document.getElementById('idModeloVehiculo').value = ModeloVehiculo;
        document.getElementById('idCategoria').value = Categoria;
        document.getElementById('idMarcaMotor').value = MarcaMotor;
        document.getElementById('idNumeroMotor').value = NumeroMotor;
        document.getElementById('idNumeroSerieChasis').value = NumeroSerieChasis;
        document.getElementById('idVin').value = Vin;
        document.getElementById('idAnio').value = Anio;
        document.getElementById('idCarroceria').value = Carroceria;
        document.getElementById('idPesoNeto').value = PesoNeto;
        document.getElementById('idPesoBruto').value = PesoBruto;
        document.getElementById('idPotencia').value = Potencia;
        document.getElementById('idColor').value = Color;
        document.getElementById('idCapacidad').value = Capacidad;
        document.getElementById('idCombustible').value = Combustible;
        document.getElementById('idCilindraje').value = Cilindraje;
        document.getElementById('idLugarFirma').value = LugarFirma;
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
        document.getElementById('idCedulaCliente').value = CedulaCliente;
        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
        document.getElementById('idMarcaVehiculo').value = MarcaVehiculo;
        document.getElementById('idEstiloVehiculo').value = EstiloVehiculo;
        document.getElementById('idModeloVehiculo').value = ModeloVehiculo;
        document.getElementById('idCategoria').value = Categoria;
        document.getElementById('idMarcaMotor').value = MarcaMotor;
        document.getElementById('idNumeroMotor').value = NumeroMotor;
        document.getElementById('idNumeroSerieChasis').value = NumeroSerieChasis;
        document.getElementById('idVin').value = Vin;
        document.getElementById('idAnio').value = Anio;
        document.getElementById('idCarroceria').value = Carroceria;
        document.getElementById('idPesoNeto').value = PesoNeto;
        document.getElementById('idPesoBruto').value = PesoBruto;
        document.getElementById('idPotencia').value = Potencia;
        document.getElementById('idColor').value = Color;
        document.getElementById('idCapacidad').value = Capacidad;
        document.getElementById('idCombustible').value = Combustible;
        document.getElementById('idCilindraje').value = Cilindraje;
        document.getElementById('idLugarFirma').value = LugarFirma;
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

        document.getElementById('idCedulaCliente').value = CedulaCliente;
        document.getElementById('idCedulaAbogado').value = CedulaAbogado;
        document.getElementById('idMarcaVehiculo').value = MarcaVehiculo;
        document.getElementById('idEstiloVehiculo').value = EstiloVehiculo;
        document.getElementById('idModeloVehiculo').value = ModeloVehiculo;
        document.getElementById('idCategoria').value = Categoria;
        document.getElementById('idMarcaMotor').value = MarcaMotor;
        document.getElementById('idNumeroMotor').value = NumeroMotor;
        document.getElementById('idNumeroSerieChasis').value = NumeroSerieChasis;
        document.getElementById('idVin').value = Vin;
        document.getElementById('idAnio').value = Anio;
        document.getElementById('idCarroceria').value = Carroceria;
        document.getElementById('idPesoNeto').value = PesoNeto;
        document.getElementById('idPesoBruto').value = PesoBruto;
        document.getElementById('idPotencia').value = Potencia;
        document.getElementById('idColor').value = Color;
        document.getElementById('idCapacidad').value = Capacidad;
        document.getElementById('idCombustible').value = Combustible;
        document.getElementById('idCilindraje').value = Cilindraje;
        document.getElementById('idLugarFirma').value = LugarFirma;
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