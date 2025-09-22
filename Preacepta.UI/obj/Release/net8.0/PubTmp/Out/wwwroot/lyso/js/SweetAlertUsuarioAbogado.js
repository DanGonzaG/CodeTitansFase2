document.addEventListener('DOMContentLoaded', function () {
    /*SweetAler, se encarga de buscar la persona y valida su existencia en el sistema*/
    /*document.getElementById('BuscarPersona').addEventListener('click', function (event) {
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
                            window.location.href = '/Personas/Details?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', 'La cédula ingresada no esta registrada.', 'error')
                        }
                    });
            }
        });
    });*/

    /*SweetAler, se encarga de buscar ABOGADO y valida su existencia en el sistema*/
    /*document.getElementById('BuscarAbogado').addEventListener('click', function (event) {
        event.preventDefault();
        Swal.fire({
            title: 'Cédula del abogado',
            input: 'text',
            showCancelButton: true,
            confirmButtonText: 'Continuar',
            icon: 'question'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/Abogado/IdExiste?id=' + result.value)
                    .then(respuesta => respuesta.json())
                    .then(datos => {
                        if (datos.bandera) {
                            window.location.href = '/Abogado/Details?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', `No se halló coincidencia para la cédula ingresada ${result.value}.`, 'error')
                        }
                    });
            }
        });
    });*/

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

    /*SweetAler, se encarga de buscar CITA y valida su existencia en el sistema*/
    /*document.getElementById('BuscarCita').addEventListener('click', function (event) {
        event.preventDefault();
        Swal.fire({
            title: 'Número de cita',
            input: 'text',
            showCancelButton: true,
            confirmButtonText: 'Continuar',
            icon: 'question'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/Citas/IdExiste?id=' + result.value)
                    .then(respuesta => respuesta.json())
                    .then(datos => {
                        if (datos.bandera) {
                            window.location.href = '/Citas/Details?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', `No se halló coincidencia para la cita número ${result.value}.`, 'error')
                        }
                    });
            }
        });
    });*/

    /*SweetAler, se encarga de buscar CASO y valida su existencia en el sistema*/
    /*document.getElementById('BuscarCaso').addEventListener('click', function (event) {
        event.preventDefault();
        Swal.fire({
            title: 'Número de caso',
            input: 'text',
            showCancelButton: true,
            confirmButtonText: 'Continuar',
            icon: 'question'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/Caso/IdExiste?id=' + result.value)
                    .then(respuesta => respuesta.json())
                    .then(datos => {
                        if (datos.bandera) {
                            window.location.href = '/Caso/Details?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', `No se halló coincidencia para la caso número ${result.value}.`, 'error')
                        }
                    });
            }
        });
    });*/

    /*SweetAler, se encarga de buscar TESTIMONIO y valida su existencia en el sistema*/
    /*document.getElementById('BuscarTestimonio').addEventListener('click', function (event) {
        event.preventDefault();
        Swal.fire({
            title: 'Número de testimonio',
            input: 'text',
            showCancelButton: true,
            confirmButtonText: 'Continuar',
            icon: 'question'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/TTestimonios/IdExiste?id=' + result.value)
                    .then(respuesta => respuesta.json())
                    .then(datos => {
                        if (datos.bandera) {
                            window.location.href = '/TTestimonios/Details?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', `No se halló coincidencia para el testimonio número ${result.value}.`, 'error')
                        }
                    });
            }
        });
    });*/

    /*SweetAler, se encarga de buscar ETAPA DE PROCESO LEGAL DE UN CASO y valida su existencia en el sistema*/
    /*document.getElementById('BuscarEtpaPL').addEventListener('click', function (event) {
        event.preventDefault();
        Swal.fire({
            title: 'Número de etapa de caso',
            input: 'text',
            showCancelButton: true,
            confirmButtonText: 'Continuar',
            icon: 'question'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/CasosEtapa/IdExiste?id=' + result.value)
                    .then(respuesta => respuesta.json())
                    .then(datos => {
                        if (datos.bandera) {
                            window.location.href = '/CasosEtapa/Details?id=' + result.value;
                        }
                        else {
                            Swal.fire('Sistema de busqueda', `No se halló coincidencia para la etapa de caso número ${result.value}.`, 'error')
                        }
                    });
            }
        });*/
    });
});