let citaSeleccionadaId = null;

// Delegar clic en eventos del calendario para mostrar detalles
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
                        alert("No se pudo cargar la información de la cita.");
                    });
            } else {
                alert("ID de cita inválido");
            }
        });
    });
}

// Mostrar detalles de la cita
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
            alert("No se pudo cargar la información de la cita.");

            const modal = document.getElementById("modalDetalleCita");
            if (modal) {
                modal.style.display = "none";
                modal.setAttribute('aria-hidden', 'true');
            }
        });
}

// Modal para listar documentos de la cita
function abrirModalListarDocumentos(idCita) {
    const modalDetalleCita = document.getElementById('modalDetalleCita');
    if (modalDetalleCita) {
        modalDetalleCita.style.display = 'none'; // Cerrar el modal de detalles
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
                    inicializarEventosModal(); // Inicializa eventos del modal
                } else {
                    console.error("El modal no se cargó correctamente.");
                }
            }, 100);
        })
        .catch(err => console.error("Error cargando el modal de documentos:", err));
}

// Subir documento
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
                    console.log("Archivo subido con éxito");
                    cerrarModalListarDocumentos();
                    console.log("Redirigiendo a /Citas/Calendar");
                    window.location.href = '/Citas/Calendar';
                }
                else {
                    alert("Error al subir: " + data.message);
                }
            })
            .catch(err => console.error("Error al subir documento:", err));
    });
}

// Cerrar modal de listar documentos
function cerrarModalListarDocumentos() {
    var modal = document.getElementById('modalListarDocumentos');
    if (modal) modal.style.display = "none";
}

// Inicializar eventos dentro del modal de documentos
function inicializarEventosModal() {
    const switches = document.querySelectorAll('.chk-permitir');
    switches.forEach(function (switchElement) {
        switchElement.addEventListener('change', function () {
            // Si quieres, aquí solo harías algo visual
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
                alert("No hay permisos para actualizar.");
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
                        alert("Permisos actualizados correctamente.");
                        cerrarModalListarDocumentos();
                        if (data.redirectUrl) {
                            window.location.href = data.redirectUrl;
                        } else {
                            window.location.reload();
                        }
                    } else {
                        alert("No se pudo actualizar el permiso: " + (data.message || ""));
                    }
                })
                .catch(err => {
                    console.error("Error al actualizar permisos:", err);
                    alert("Ocurrió un error al intentar actualizar los permisos.");
                });
        });
    } else {
        console.warn("Botón btnActualizarTodos no encontrado en modal.");
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
        alert("No se ha seleccionado ninguna cita para editar.");
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
        })
        .catch(err => {
            console.error("Error al cargar edición:", err);
            alert("No se pudo cargar el formulario de edición.");
        });
}

// Mostrar detalles de la cita desde la vista de detalles
function mostrarEditarCitaDesdeDetalle(idCita) {
    citaSeleccionadaId = idCita;
    mostrarEditarCita();
}

// Eliminar cita
window.mostrarModalEliminarCita = function (idCita) {
    // Cargar partial view con detalles y formulario de eliminar
    fetch(`/Citas/Delete/${idCita}`)
        .then(res => {
            if (!res.ok) throw new Error(`HTTP ${res.status}`);
            return res.text();
        })
        .then(html => {
            const modalBody = document.getElementById('eliminarCitaBody');
            modalBody.innerHTML = html;

            // Mostrar el modal
            const modal = document.getElementById('modalEliminarCita');
            if (modal) {
                modal.style.display = 'block';
                // Enfocar algún elemento dentro del modal
                const focusElem = modal.querySelector('input, button, [tabindex]:not([tabindex="-1"])');
                if (focusElem) focusElem.focus();
            }
        })
        .catch(err => {
            console.error("Error al cargar modal eliminar:", err);
            alert("No se pudo cargar el modal de eliminación.");
        });
}

function eliminarCita() {
    const idInput = document.getElementById('IdCitaEliminar');
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');

    if (!idInput || !tokenInput) {
        alert("Datos de eliminación incompletos.");
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
                alert("Cita eliminada correctamente.");
                cerrarModal('modalEliminarCita');
                window.location.href = "/Citas/Calendar";
            } else {
                alert("No se pudo eliminar la cita.");
            }
        })
        .catch(error => {
            console.error("Error al eliminar cita:", error);
            alert("Error al eliminar la cita.");
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
    agregarClickEventos(); // Añadir eventos a las citas del calendario

    // Cerrar modal de detalles si se hace clic fuera del modal
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
            const crearModal = document.getElementById('crearCitaModal');
            if (crearModal) {
                crearModal.style.display = 'none';
            }
        });
    }
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
// --- DOCUMENT READY ---

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
            const crearModal = document.getElementById('crearCitaModal');
            if (crearModal) {
                crearModal.style.display = 'none';
            }
        });
    }
    $('#btnAbrirModal').click(function () {
        $.get('/Citas/Create', function (data) {
            $('#crearCitaModalBody').html(data);
            const modal = document.getElementById('crearCitaModal');
            if (modal) {
                modal.style.display = 'flex';
            }
            $.validator.unobtrusive.parse($('#crearCitaModalBody'));
        });
    });

    // Cerrar modal al hacer clic fuera del contenido
    window.addEventListener('click', function (e) {
        const modal = document.getElementById('crearCitaModal');
        if (modal && e.target === modal) {
            modal.style.display = 'none';
        }
    });

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
                    document.getElementById('crearCitaModal').style.display = 'none';
                    marcarDiaConCita(response.nuevaFecha, response.idCita);
                    agregarClickEventos();
                    alert("Cita creada con éxito.");
                } else {
                    $('#crearCitaModalBody').html(response);
                    $.validator.unobtrusive.parse($('#crearCitaModalBody'));
                }
                $submitBtn.prop('disabled', false);
            },
            error: function () {
                alert("Error al crear la cita");
                $submitBtn.prop('disabled', false);
            }
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
                    alert("Cita actualizada con éxito.");

                    if (response.fechaAnterior && response.fechaAnterior !== response.nuevaFecha) {
                        const anterior = document.querySelector(`[data-fecha="${response.fechaAnterior}"]`);
                        if (anterior) {
                            anterior.classList.remove("tiene-cita", "event");
                            anterior.removeAttribute("data-id");
                        }
                    }

                    marcarDiaConCita(response.nuevaFecha, response.idCita);
                    agregarClickEventos();
                    location.reload();
                } else {
                    $('#editarCitaBody').html(response);
                    $.validator.unobtrusive.parse($('#editarCitaBody'));
                }
            },
            error: function () {
                alert("Error al guardar los cambios.");
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
        if (!$.fn.DataTable.isDataTable('#tablaCitasFuturas')) {
            $('#tablaCitasFuturas').DataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'
                }
            });
        }
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
