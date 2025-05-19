//Vista Formulario caso
//es codigo lo que hace es almacenar los datos de form en el modal y viceversa
document.getElementById('PrevioBtn').addEventListener('click', () => {
    const TipoCaso = document.getElementById('TipoCaso').value;
    const titulo = document.getElementById('titulo').value;
    const Pruebas = document.getElementById('Pruebas').value;
    const NomCliente = document.getElementById('NomCliente').value;
    const descripcionCaso = document.getElementById('descripcionCaso').value;
    

    // Actualiza el contenido del modal
    document.getElementById('previoTipoCaso').textContent = TipoCaso;
    document.getElementById('previotitulo').textContent = titulo;
    document.getElementById('previoPruebas').textContent = Pruebas;
    document.getElementById('previoCliente').textContent = NomCliente;
    document.getElementById('previoDescripcion').textContent = descripcionCaso;

    // Muestra el modal usando Bootstrap
    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
    previewModal.show();


    document.getElementById('Retroceso').addEventListener('click', () => {
       
        document.getElementById('TipoCaso').value = TipoCaso;
        document.getElementById('titulo').value = titulo;
        document.getElementById('Pruebas').value = Pruebas;
        document.getElementById('NomCliente').value = NomCliente;
        document.getElementById('descripcionCaso').value = descripcionCaso;
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

        document.getElementById('TipoCaso').value = TipoCaso;
        document.getElementById('titulo').value = titulo;
        document.getElementById('Pruebas').value = Pruebas;
        document.getElementById('NomCliente').value = NomCliente;
        document.getElementById('descripcionCaso').value = descripcionCaso;
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

/*
//Vista DocsGenerator
//este codigo lo que hace es mostrar solo el formulario seleccionado en dropdown
function mostrarFormulario() {
    // Oculta todos los formularios
    const formularios = document.querySelectorAll('.formulario');
    formularios.forEach(formulario => formulario.style.display = 'none');

    // Muestra el formulario seleccionado
    const seleccion = document.getElementById('selector').value;
    if (seleccion) {
        document.getElementById(seleccion).style.display = 'block';
    }
}*/