//Vista Formulario caso
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'none';

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
document.getElementById('PrevioBtn').addEventListener('click', () => {
    const Fecha = document.getElementById('idFecha').value;
    const IdAbogadoText = document.getElementById('idIdAbogado').options[document.getElementById('idIdAbogado').selectedIndex].text;
    const IdCliente = document.getElementById('idIdCliente').value;
    const Texto = document.getElementById('idTexto').value;
    const Submit = document.getElementById('idSubmit').value;


    // Actualiza el contenido del modal
    document.getElementById('previoFecha').textContent = Fecha;
    document.getElementById('previoIdAbogado').textContent = IdAbogado;
    document.getElementById('previoIdCliente').textContent = IdCliente;
    document.getElementById('previoTexto').textContent = Texto;

    // Muestra el modal usando Bootstrap
    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
    previewModal.show();

    document.getElementById('BtnConfirmar').addEventListener('click', () => {
        console.log('Confirmando');
        document.getElementById('previoFecha').value = Fecha;
        document.getElementById('previoIdAbogado').value = IdAbogado;
        document.getElementById('previoIdCliente').value = IdCliente;
        document.getElementById('previoTexto').value = Texto;
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
        document.getElementById('previoFecha').value = Fecha;
        document.getElementById('previoIdAbogado').value = IdAbogado;
        document.getElementById('previoIdCliente').value = IdCliente;
        document.getElementById('previoTexto').value = Texto;
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

        document.getElementById('previoFecha').value = Fecha;
        document.getElementById('previoIdAbogado').value = IdAbogado;
        document.getElementById('previoIdCliente').value = IdCliente;
        document.getElementById('previoTexto').value = Texto;
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