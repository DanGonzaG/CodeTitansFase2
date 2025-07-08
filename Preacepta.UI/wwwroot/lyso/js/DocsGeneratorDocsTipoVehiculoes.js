//Vista Formulario caso
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsOpcionCompraventaVehiculoes').addEventListener('submit', (e) => {

});

// Función para abrir el PDF de previsualización en una nueva ventana
document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const Nombre = document.getElementById('idNombre').value;

    const timestamp = new Date().getTime();

    const url = `/DocsTipoVehiculoes/PrevisualizarPDF?Nombre = ${encodeURIComponent(Nombre)}& t=${timestamp}`;

    window.open(url, '_blank');
});

////este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
//document.getElementById('PrevioBtn').addEventListener('click', () => {
//    const Nombre = document.getElementById('idNombre').value;


//    // Actualiza el contenido del modal
//    document.getElementById('previoNombre').textContent = Nombre;

//    // Muestra el modal usando Bootstrap
//    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
//    previewModal.show();

//    document.getElementById('BtnConfirmar').addEventListener('click', () => {
//        console.log('Confirmando');
//        document.getElementById('idNombre').value = Nombre;
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
//        document.getElementById('idNombre').value = Nombre;
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

//        document.getElementById('idNombre').value = Nombre;
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