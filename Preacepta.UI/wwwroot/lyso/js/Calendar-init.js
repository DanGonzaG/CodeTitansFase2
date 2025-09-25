let citaSeleccionadaId = null;

function agregarClickEventos() {
    const renderedEvents = document.querySelectorAll(".event");
    renderedEvents.forEach(ev => {
        ev.addEventListener("click", (e) => {
            e.stopPropagation();
            const citaId = ev.getAttribute("data-id");
            if (citaId) {
                fetch(`/Citas/Details/${citaId}`)
                    .then(res => res.text())
                    .then(html => {
                        document.getElementById("detalleCitaBody").innerHTML = html;
                        document.getElementById("modalDetalleCita").style.display = "block";
                    })
                    .catch(err => {
                        console.error("Error al cargar detalles de cita", err);
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo cargar la información de la cita.',
                            showConfirmButton: true
                        });
                    });
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'ID inválido',
                    text: 'La cita seleccionada no es válida.',
                    confirmButtonText: 'OK'
                });
            }
        });
    });
}


function mostrarDetallesCita(idCita) {
    citaSeleccionadaId = idCita;
    fetch(`/Citas/Details/${idCita}`)
        .then(res => {
            if (!res.ok) {
                if (res.status === 403) {
                    throw new Error("No tienes permiso para ver esta cita.");
                } else if (res.status === 404) {
                    throw new Error("La cita no fue encontrada.");
                } else {
                    throw new Error(`Error inesperado: ${res.status}`);
                }
            }
            return res.text();
        })
        .then(html => {
            document.getElementById("detalleCitaBody").innerHTML = html;
            const modal = document.getElementById("modalDetalleCita");
            if (modal) {
                modal.style.display = "flex";
                modal.setAttribute('aria-hidden', 'false');
                const focusElem = modal.querySelector('input, button, [tabindex]:not([tabindex="-1"])');
                if (focusElem) focusElem.focus();
            } else {
                console.warn("Modal detalle cita no encontrado en DOM");
            }
        })
        .catch(err => {
            console.error("Error al cargar detalles:", err);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No se pudo cargar la información de la cita.',
                confirmButtonText: 'OK'
            });

            const modal = document.getElementById("modalDetalleCita");
            if (modal) {
                modal.style.display = "none";
                modal.setAttribute('aria-hidden', 'true');
            }
        });
}

function abrirModalListarDocumentos(idCita) {
    const modalDetalleCita = document.getElementById('modalDetalleCita');
    if (modalDetalleCita) {
        modalDetalleCita.style.display = 'none'; 
        modalDetalleCita.setAttribute('aria-hidden', 'true');
    }
    fetch(`/DocumentosCita/Listar?idCita=${idCita}`)
        .then(response => response.text())
        .then(html => {
            const modalContainer = document.getElementById('modalContainer');
            modalContainer.innerHTML = html;
            setTimeout(() => {
                const modal = document.getElementById('modalListarDocumentos');
                if (modal) {
                    modal.style.display = "block";
                    inicializarEventosModal(); 
                } else {
                    console.error("El modal no se cargó correctamente.");
                }
            }, 100);
        })
        .catch(err => console.error("Error cargando el modal de documentos:", err));
}


const form = document.getElementById("formSubirDocumento");
if (form) {
    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const formData = new FormData(form);
        fetch(form.action, {
            method: "POST",
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Documento subido',
                        text: 'El documento se subió correctamente.',
                        confirmButtonText: 'Aceptar'
                    });

                    const idCita = form.dataset.idCita; 
                    if (idCita) {
                        fetch(`/DocumentosCita/Listar?idCita=${idCita}`)
                            .then(res => res.text())
                            .then(html => {
                                document.getElementById('modalListarDocumentosBody').innerHTML = html;
                                inicializarEventosModal();
                            });
                    }

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error al subir',
                        text: data.message || 'Ocurrió un error al intentar subir el documento.',
                        confirmButtonText: 'OK'
                    });
                }
            })
            .catch(err => {
                console.error("Error al subir documento:", err);
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Ocurrió un error inesperado al intentar subir el documento.',
                    confirmButtonText: 'OK'
                });
            });
    });
}

function cerrarModalListarDocumentos() {
    var modal = document.getElementById('modalListarDocumentos');
    if (modal) modal.style.display = "none";
}

// Inicializar eventos dentro del modal de documentos
function inicializarEventosModal() {
    const switches = document.querySelectorAll('.chk-permitir');
    switches.forEach(function (switchElement) {
        switchElement.addEventListener('change', function () {
           
        });
    });

    const btnActualizar = document.getElementById("btnActualizarTodos");
    if (btnActualizar) {
        btnActualizar.addEventListener("click", function () {
            const documentos = [];
            document.querySelectorAll('.chk-permitir').forEach(function (switchElement) {
                documentos.push({
                    id: parseInt(switchElement.getAttribute('data-id')),
                    descargar: switchElement.checked
                });
            });

            if (documentos.length === 0) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Sin cambios',
                    text: 'No hay permisos para actualizar.',
                    confirmButtonText: 'OK'
                });
                return;
            }

            fetch('/DocumentosCita/ActualizarPermisoDescargaBatch', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(documentos),
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Actualizado!',
                            text: 'Permisos actualizados correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            cerrarModalListarDocumentos();
                            if (data.redirectUrl) {
                                window.location.href = data.redirectUrl;
                            } else {
                                window.location.reload();
                            }
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo actualizar el permiso: ' + (data.message || ''),
                            confirmButtonText: 'OK'
                        });
                    }
                })
                .catch(err => {
                    console.error("Error al actualizar permisos:", err);
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Ocurrió un error al intentar actualizar los permisos.',
                        confirmButtonText: 'OK'
                    });
                });
        });
    }
    const modal = document.getElementById('modalListarDocumentos');
    if (modal) {
        modal.addEventListener('click', function (e) {
            if (e.target.classList.contains('btnEliminarDoc')) {
                const li = e.target.closest('li');
                const idDoc = parseInt(li.dataset.id);
                Swal.fire({
                    title: '¿Eliminar documento?',
                    text: "Esta acción no se puede deshacer.",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        fetch(`/DocumentosCita/Eliminar?id=${idDoc}`, { method: 'POST' })
                            .then(res => res.json())
                            .then(data => {
                                if (data.success) {
                                    Swal.fire({
                                        icon: 'success',
                                        title: '¡Eliminado!',
                                        text: 'Documento eliminado correctamente.',
                                        timer: 2000,
                                        showConfirmButton: false
                                    });

                                    if (li) li.remove();
                                    if (document.querySelectorAll('#modalListarDocumentos li').length === 0) {
                                        const msg = document.getElementById('mensajeSinDocumentos');
                                        if (msg) msg.style.display = "block";
                                    }
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        text: 'No se pudo eliminar el documento.',
                                        confirmButtonText: 'OK'
                                    });
                                }
                            })
                            .catch(err => {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'Ocurrió un error al eliminar el documento.',
                                    confirmButtonText: 'OK'
                                });
                            });
                    }
                });
            }
        });
    }
}

// Cerrar modal si se hace clic fuera del modal
window.onclick = function (event) {
    var modal = document.getElementById('modalListarDocumentos');
    if (event.target == modal) {
        modal.style.display = "none";
    }
};

// Editar cita
function mostrarEditarCita() {
    if (!citaSeleccionadaId) {
        Swal.fire({
            icon: 'warning',
            title: 'Datos incompletos',
            text: 'No se ha seleccionado ninguna cita para editar.',
            confirmButtonText: 'OK'
        });
        return;
    }

    const modalDetalle = document.getElementById('modalDetalleCita');
    if (modalDetalle) {
        modalDetalle.style.display = 'none';
        modalDetalle.setAttribute('aria-hidden', 'true');
    }

    fetch(`/Citas/Edit/${citaSeleccionadaId}`)
        .then(res => {
            if (!res.ok) throw new Error(`HTTP ${res.status}`);
            return res.text();
        })
        .then(html => {
            document.getElementById('editarCitaBody').innerHTML = html;

            const modalEditar = document.getElementById('modalEditarCita');
            if (modalEditar) {
                modalEditar.style.display = 'flex';
                modalEditar.setAttribute('aria-hidden', 'false');
                const focusElem = modalEditar.querySelector('input, button, [tabindex]:not([tabindex="-1"])');
                if (focusElem) focusElem.focus();
            }

            $.validator.unobtrusive.parse($('#editarCitaBody'));

            const tipoCitaSelect = $('#IdTipoCita');
            tipoCitaSelect.data('prevTipo', tipoCitaSelect.find('option:selected').text().trim().toLowerCase());

            function manejarTipoCita() {
                const tipoActual = tipoCitaSelect.find('option:selected').text().trim().toLowerCase();
                const tipoAnterior = tipoCitaSelect.data('prevTipo');

                if (tipoActual === 'virtual') {

                    if (tipoAnterior !== 'virtual') {
                        abrirModalReunionZoom();
                    }
                } else {
                    
                    if (tipoAnterior === 'virtual') {
                        Swal.fire({
                            icon: 'info',
                            title: 'Cambio de tipo de cita',
                            text: 'El enlace de Zoom ya no estará disponible en Detalles porque la cita dejó de ser virtual.',
                            confirmButtonText: 'Aceptar'
                        });
                    }
                }
                tipoCitaSelect.data('prevTipo', tipoActual);
            }
            manejarTipoCita();
            tipoCitaSelect.off('change').on('change', manejarTipoCita);
        })
        .catch(err => {
            console.error("Error al cargar edición:", err);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No se pudo cargar el formulario para editar',
                confirmButtonText: 'OK'
            });
        });
}


// Mostrar detalles de la cita desde la vista de detalles
function mostrarEditarCitaDesdeDetalle(idCita) {
    citaSeleccionadaId = idCita;
    mostrarEditarCita();
}

// Eliminar cita
window.mostrarModalEliminarCita = function (idCita) {
    fetch(`/Citas/Delete/${idCita}`)
        .then(res => {
            if (!res.ok) throw new Error(`HTTP ${res.status}`);
            return res.text();
        })
        .then(html => {
            const modalBody = document.getElementById('eliminarCitaBody');
            modalBody.innerHTML = html;
            const modal = document.getElementById('modalEliminarCita');
            if (modal) {
                modal.style.display = 'block';
                const focusElem = modal.querySelector('input, button, [tabindex]:not([tabindex="-1"])');
                if (focusElem) focusElem.focus();
            }
        })
        .catch(err => {
            console.error("Error al cargar modal eliminar:", err);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No se pudo cargar el modal de eliminación.',
                confirmButtonText: 'OK'
            });
        });
}

function eliminarCita() {
    const idInput = document.getElementById('IdCitaEliminar');
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');

    if (!idInput || !tokenInput) {
        Swal.fire({
            icon: 'warning',
            title: 'Datos incompletos',
            text: 'No se encontraron los datos necesarios para eliminar la cita.',
            confirmButtonText: 'OK'
        });
        return;
    }

    const id = idInput.value;
    const token = tokenInput.value;

    const formData = new FormData();
    formData.append('IdCita', id);
    formData.append('__RequestVerificationToken', token);

    fetch('/Citas/Delete', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (!response.ok) throw new Error('Error en la eliminación');
            return response.json();
        })
        .then(data => {
            if (data.success) {
                Swal.fire({
                    icon: 'success',
                    title: '¡Cita Eliminada!',
                    text: 'La cita se eliminó correctamente.',
                    timer: 2000,
                    showConfirmButton: false
                });
                cerrarModal('modalEliminarCita');
                window.location.href = "/Citas/Calendar";
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se pudo eliminar la cita.',
                    showConfirmButton: true
                });
            }
        })
        .catch(error => {
            console.error("Error al eliminar cita:", error);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Ocurrió un error al eliminar la cita.',
                showConfirmButton: true
            });
        });
}

// Cerrar modal de eliminación
function cerrarModal(idModal, redirigir = false) {
    const modal = document.getElementById(idModal);
    if (modal) {
        modal.style.display = 'none';
        modal.setAttribute('aria-hidden', 'true');
    }
    if (redirigir) {
        window.location.href = '/Citas/Calendar';
    }
}

// Inicialización de funciones cuando se carga el documento
document.addEventListener("DOMContentLoaded", function () {
    agregarClickEventos(); 
    const modalDetalleCita = document.getElementById("modalDetalleCita");
    if (modalDetalleCita) {
        window.addEventListener("click", function (event) {
            if (event.target === modalDetalleCita) {
                modalDetalleCita.style.display = "none";
                modalDetalleCita.setAttribute('aria-hidden', 'true');
            }
        });
    }

    $(document).ready(function () {
        agregarClickEventos();
        const modalDetalleCita = document.getElementById("modalDetalleCita");
        if (modalDetalleCita) {
            window.addEventListener("click", function (event) {
                if (event.target === modalDetalleCita) {
                    modalDetalleCita.style.display = "none";
                    modalDetalleCita.setAttribute('aria-hidden', 'true');
                }
            });
        }
        const cerrarCrearModalBtn = document.getElementById('cerrarCrearModal');
        if (cerrarCrearModalBtn) {
            cerrarCrearModalBtn.addEventListener('click', function () {
                cerrarCrearModal();
            });
        }

        function cerrarCrearModal() {
            const crearModal = document.getElementById('crearCitaModal');
            if (crearModal) {
                crearModal.style.display = 'none';
                crearModal.setAttribute('aria-hidden', 'true');
            }
            refrescarCalendarioCompleto(); 
        }

        function refrescarCalendarioCompleto() {
            $.get('/Citas/GetCitas', function (data) {
                document.querySelectorAll('.tiene-cita').forEach(dia => {
                    dia.classList.remove('tiene-cita');
                    dia.removeAttribute('data-id-cita');
                });

                data.forEach(cita => {
                    marcarDiaConCita(cita.Fecha, cita.IdCita);
                });

                agregarClickEventos();
            });
        }

        // Abrir modal de crear cita
        $('#btnAbrirModal').click(function () {
            $.get('/Citas/Create', function (data) {
                $('#crearCitaModalBody').html(data);
                const modal = document.getElementById('crearCitaModal');
                if (modal) modal.style.display = 'flex';
                $.validator.unobtrusive.parse($('#crearCitaModalBody'));

                $('#crearCitaModalBody #IdTipoCita').off('change').on('change', function () {
                    const textoSeleccionado = $(this).find('option:selected').text().trim().toLowerCase();
                    if (textoSeleccionado === 'virtual') {
                        abrirModalReunionZoom("#crearCitaModal input[name='LinkVideo']");
                    }
                });
            });
        });

        // Cerrar modal al hacer clic fuera
        window.addEventListener('click', function (e) {
            const modal = document.getElementById('crearCitaModal');
            if (modal && e.target === modal) cerrarCrearModal();
        });

        // Submit AJAX del formulario dentro del modal
        $('#crearCitaModalBody').on('submit', 'form', function (e) {
            e.preventDefault();
            var $form = $(this);
            var $submitBtn = $form.find('input[type="submit"]');
            $submitBtn.prop('disabled', true);

            $.ajax({
                type: $form.attr('method'),
                url: $form.attr('action'),
                data: $form.serialize(),
                success: function (response) {
                    if (response.success) {
                        cerrarCrearModal();
                        marcarDiaConCita(response.nuevaFecha, response.idCita);
                        agregarClickEventos();
                        Swal.fire({
                            icon: 'success',
                            title: '¡Cita creada!',
                            text: 'La cita se creó correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.reload();
                        });
                    } else {
                        $('#crearCitaModalBody').html(response);
                        $.validator.unobtrusive.parse($('#crearCitaModalBody'));

                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo crear la cita. Revisa los datos ingresados.',
                            showConfirmButton: true
                        });
                    }
                    $submitBtn.prop('disabled', false);
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Ocurrió un error al crear la cita.',
                        showConfirmButton: true
                    });
                    $submitBtn.prop('disabled', false);
                }
            });
        });
    });

    $('#editarCitaBody').on('submit', 'form', function (e) {
        e.preventDefault();
        const $form = $(this);
        const $submitBtn = $form.find('input[type="submit"]');
        $submitBtn.prop('disabled', true);

        $.ajax({
            type: $form.attr('method'),
            url: $form.attr('action'),
            data: $form.serialize(),
            success: function (response) {
                if (response.success) {
                    document.getElementById('modalEditarCita').style.display = 'none';
                    Swal.fire({
                        icon: 'success',
                        title: '¡Cita Actualizada!',
                        text: 'La cita se actualizó correctamente.',
                        timer: 2000,
                        showConfirmButton: false
                    }).then(() => {
                        location.reload();
                    });

                    if (response.fechaAnterior && response.fechaAnterior !== response.nuevaFecha) {
                        const anterior = document.querySelector(`[data-fecha="${response.fechaAnterior}"]`);
                        if (anterior) {
                            anterior.classList.remove("tiene-cita", "event");
                            anterior.removeAttribute("data-id");
                        }
                    }

                    marcarDiaConCita(response.nuevaFecha, response.idCita);
                    agregarClickEventos();
                } else {
                    $('#editarCitaBody').html(response);
                    $.validator.unobtrusive.parse($('#editarCitaBody'));
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Error al guardar los cambios',
                    showConfirmButton: true
                });
            },
            complete: function () {
                $submitBtn.prop('disabled', false);
            }
        });
    });

    function marcarDiaConCita(fechaISO, idCita = null) {
        const diaElemento = document.querySelector(`[data-fecha="${fechaISO}"]`);
        if (diaElemento) {
            diaElemento.classList.add("tiene-cita");

            if (idCita) {
                diaElemento.setAttribute("data-id", idCita);
                diaElemento.classList.add("event");
            }
        }
    }


    $('#ModalCitasFuturas').on('shown.bs.modal', function () {

        if ($.fn.DataTable.isDataTable('#tablaCitasFuturas')) {
            $('#tablaCitasFuturas').DataTable().destroy();
        }

        $('#tablaCitasFuturas').DataTable({
            language: {
                paginate: {
                    previous: "Anterior",
                    next: "Siguiente"
                },
                emptyTable: "No hay datos disponibles en la tabla",
                info: "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                lengthMenu: "Mostrar _MENU_ entradas",
                search: "Buscar:",
                zeroRecords: "No se encontraron resultados"
            }
        });
    });
    $('#ModalCitasPasadas').on('shown.bs.modal', function () {
        if (!$.fn.DataTable.isDataTable('#tablaCitasPasadas')) {
            $('#tablaCitasPasadas').DataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'
                }
            });
        }
    });
});

$('#ModalCitasFuturas, #ModalCitasPasadas').on('hide.bs.modal', function () {
    if (document.activeElement && $(this).has(document.activeElement).length) {
        document.activeElement.blur();
    }
});

let lastTrigger = null;

$('[data-bs-toggle="modal"]').on('click', function () {
    lastTrigger = this;
});

$('#ModalCitasFuturas, #ModalCitasPasadas').on('hidden.bs.modal', function () {
    if (lastTrigger) {
        lastTrigger.focus();
    }
});

async function cargarCitasCliente() {
    try {
        const response = await fetch('/Citas/CalendarCliente'); 
        if (!response.ok) {
            throw new Error(`Error HTTP: ${response.status}`);
        }

        const html = await response.text();
        const contenedor = document.getElementById('contenedorCitasCliente');
        if (contenedor) {
            contenedor.innerHTML = html;
            agregarClickEventos();
        } else {
            console.warn("Contenedor de citas cliente no encontrado.");
        }
    } catch (error) {
        console.error("Error al cargar citas del cliente:", error);
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'No se pudieron cargar las citas del cliente.',
            confirmButtonText: 'OK'
        });
    }
}

document.addEventListener('DOMContentLoaded', () => {
    cargarCitasCliente();
});

async function terminarCita(idCita) {
    const response = await fetch('/Citas/TerminarCitaEnviarCorreo', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ idCita })
    });

    const data = await response.json();
    if (data.success) {
        Swal.fire({
            icon: 'success',
            title: '¡Cita terminada!',
            text: 'El correo fue enviado al cliente.',
            timer: 2000,
            showConfirmButton: false
        });
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'No se pudo terminar la cita.',
            showConfirmButton: true
        });
    }
}