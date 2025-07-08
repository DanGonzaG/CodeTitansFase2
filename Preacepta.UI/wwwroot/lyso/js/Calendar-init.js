let citaSeleccionadaId = null;

// Delegar clic en eventos del calendario para mostrar detalles
function agregarClickEventos() {
    const events = document.querySelectorAll(".event");
    if (!events.length) {
        console.warn("No se encontraron los elementos necesarios para agregar eventos");
        return;
    }
    events.forEach(ev => {
        ev.addEventListener("click", (e) => {
            const idCita = e.currentTarget.getAttribute("data-id");
            if (idCita) {
                mostrarDetallesCita(idCita);
            } else {
                console.warn("ID de cita no encontrado en elemento click");
            }
        });
    });
}

// Mostrar detalles de la cita
function mostrarDetallesCita(idCita) {
    citaSeleccionadaId = idCita;
    fetch(`/Citas/Details/${idCita}`)
        .then(res => {
            if (!res.ok) throw new Error(`HTTP ${res.status}`);
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
        });
}

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

function mostrarEditarCitaDesdeDetalle(idCita) {
    citaSeleccionadaId = idCita;
    mostrarEditarCita();
}

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
function eliminarCitaDeDia(idCita, fechaISO) {
    const diaElemento = document.querySelector(`[data-fecha="${fechaISO}"]`);
    if (diaElemento) {
        diaElemento.classList.remove("tiene-cita", "event");
        diaElemento.removeAttribute("data-id");
    }
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
                eliminarCitaDeDia(data.idCita, data.fechaAnterior);
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
