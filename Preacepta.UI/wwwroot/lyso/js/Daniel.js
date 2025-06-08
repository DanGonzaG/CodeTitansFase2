//Vista Formulario caso
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'none';

//este codigo en JavaScript lo que hace es almacenar los datos de form en el modal y viceversa
document.getElementById('PrevioBtn').addEventListener('click', () => {
    const TipoCaso = document.getElementById('IdTipoCaso').value;
    const titulo = document.getElementById('Nombre').value;
    //const Pruebas = document.getElementById('Pruebas').value;
    const NomCliente = document.getElementById('IdCliente').value;
    const descripcionCaso = document.getElementById('Descripcion').value;
    

    // Actualiza el contenido del modal
    document.getElementById('previoTipoCaso').textContent = TipoCaso;
    document.getElementById('previotitulo').textContent = titulo;
    //document.getElementById('previoPruebas').textContent = Pruebas;
    document.getElementById('previoCliente').textContent = NomCliente;
    document.getElementById('previoDescripcion').textContent = descripcionCaso;

    // Muestra el modal usando Bootstrap
    const previewModal = new bootstrap.Modal(document.getElementById('PrevioModal'));
    previewModal.show();

    document.getElementById('BtnConfirmar').addEventListener('click', () => {
        console.log('Confirmando');
        document.getElementById('IdTipoCaso').value = TipoCaso;
        document.getElementById('Nombre').value = titulo;
        //document.getElementById('Pruebas').value = Pruebas;
        document.getElementById('IdCliente').value = NomCliente;
        document.getElementById('Descripcion').value = descripcionCaso;
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
       
        document.getElementById('IdTipoCaso').value = TipoCaso;
        document.getElementById('Nombre').value = titulo;
        //document.getElementById('Pruebas').value = Pruebas;
        document.getElementById('IdCliente').value = NomCliente;
        document.getElementById('Descripcion').value = descripcionCaso;
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

        document.getElementById('IdTipoCaso').value = TipoCaso;
        document.getElementById('Nombre').value = titulo;
        //document.getElementById('Pruebas').value = Pruebas;
        document.getElementById('IdCliente').value = NomCliente;
        document.getElementById('Descripcion').value = descripcionCaso;
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