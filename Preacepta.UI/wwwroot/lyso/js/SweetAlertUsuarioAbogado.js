document.addEventListener('DOMContentLoaded', function () {

    /*SweetAler, se encarga de buscar ABOGADO y valida su existencia en el sistema*/
    document.getElementById('doc1').addEventListener('click', function (event) {
        event.preventDefault();
        Swal.fire({
            title: 'Cédula del cliente',
            input: 'text',
            showCancelButton: true,
            confirmButtonText: 'Continuar',
            icon: 'question'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/Personas/IdExiste?id=' + result.value)
                    .then(respuesta => respuesta.json())
                    .then(datos => {
                        if (datos.bandera) {
                            window.location.href = '/DocsAutorizacionRevisionExpedientes/CreateDocsAutorizacionRevisionExpedientes?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', 'La cédula ingresada no esta registrada.', 'error')
                        }
                    });
            }
        });
    });
});