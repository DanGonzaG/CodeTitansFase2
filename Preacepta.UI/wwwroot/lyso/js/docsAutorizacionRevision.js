//Vista Formulario Autorizacion expedientes

//Oculta le boton de crear caso

document.getElementById('btnCrearCaso').style.display = 'none';

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa

document.getElementById('PrevioBtn').addEventListener('click', () => {

    const expediente = document.getElementById('idExpediente').value;

    const delito = document.getElementById('idDelito').value;

    const cedulaImputado = document.getElementById('idCedulaImputado').value;

    const ofendido = document.getElementById('idOfendido').value;
    const cedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const cedulaAsistente = document.getElementById('idCedulaAsistente').value;


    // Actualiza el contenido del modal

    document.getElementById('previoExpediente').textContent = expediente;

    document.getElementById('previoDelito').textContent = delito;

    document.getElementById('previoCedulaImputado').textContent = cedulaImputado;

    document.getElementById('previoOfendido').textContent = ofendido;
    document.getElementById('previoCedulaAbogado').textContent = cedulaAbogado;
    document.getElementById('previoCedulaAsistente').textContent = cedulaAsistente;


    // Muestra el modal usando Bootstrap

    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));

    previewModal.show();

    document.getElementById('BtnConfirmar').addEventListener('click', () => {

        console.log('Confirmando');

        document.getElementById('idExpediente').value = expediente;

        document.getElementById('idDelito').value = delito;

        document.getElementById('idCedulaImputado').value = cedulaImputado;

        document.getElementById('idOfendido').value = ofendido;
        document.getElementById('idCedulaAbogado').value = cedulaAbogado;
        document.getElementById('idCedulaAsistente').value = cedulaAsistente;

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

        document.getElementById('idExpediente').value = expediente;

        document.getElementById('idDelito').value = delito;

        document.getElementById('idCedulaImputado').value = cedulaImputado;

        document.getElementById('idOfendido').value = ofendido;
        document.getElementById('idCedulaAbogado').value = cedulaAbogado;
        document.getElementById('idCedulaAsistente').value = cedulaAsistente;

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

        document.getElementById('idExpediente').value = expediente;

        document.getElementById('idDelito').value = delito;

        document.getElementById('idCedulaImputado').value = cedulaImputado;

        document.getElementById('idOfendido').value = ofendido;
        document.getElementById('idCedulaAbogado').value = cedulaAbogado;
        document.getElementById('idCedulaAsistente').value = cedulaAsistente;

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