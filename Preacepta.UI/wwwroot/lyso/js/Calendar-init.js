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
            document.getElementById("modalDetalleCita").style.display = "block";

            const modal = document.getElementById("modalDetalleCita");
            if (modal) {
                modal.style.display = "block";
            } else {
                console.warn("Modal detalle cita no encontrado en DOM");
            }
        })
        .catch(err => {
            console.error("Error al cargar detalles:", err);
            alert("No se pudo cargar la información de la cita.");
        });
}
document.addEventListener("DOMContentLoaded", function () {
    agregarClickEventos();
    const cerrarBtnIcon = document.getElementById("cerrarDetalleCita");
    const cerrarBtnTexto = document.getElementById("btnCerrarModalDetalle");
    const modal = document.getElementById("modalDetalleCita");

    function cerrarModal() {
        if (modal) modal.style.display = "none";
    }

    if (cerrarBtnIcon) cerrarBtnIcon.addEventListener("click", cerrarModal);
    if (cerrarBtnTexto) cerrarBtnTexto.addEventListener("click", cerrarModal);

    window.addEventListener("click", function (event) {
        if (event.target === modal) cerrarModal();
    });
});

// Editar Cita
function mostrarEditarCita() {
    console.log("ID de cita para editar:", citaSeleccionadaId);

    if (!citaSeleccionadaId) {
        alert("No se ha seleccionado ninguna cita para editar.");
        return;
    }

    fetch(`/Citas/Edit/${citaSeleccionadaId}`)
        .then(res => {
            if (!res.ok) throw new Error(`HTTP ${res.status}`);
            return res.text();
        })
        .then(html => {
            $('#editarCitaBody').html(html);
           
            const modal = new bootstrap.Modal(document.getElementById('modalEditarCita'));
            modal.show();

           
            $('#modalDetalleCita').modal('hide');

            
            $.validator.unobtrusive.parse($('#editarCitaBody'));
        })
        .catch(err => {
            console.error("Error al cargar edición:", err);
            alert("No se pudo cargar el formulario de edición.");
        });
}
function mostrarModalEliminarCita(idCita) {
    document.getElementById('idCitaEliminar').value = idCita;

    const modal = new bootstrap.Modal(document.getElementById('modalEliminarCita'));
    modal.show();
}

function confirmarEliminarCita() {
    const id = document.getElementById('idCitaEliminar').value;
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    fetch('/Citas/Delete', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token
        },
        body: JSON.stringify({ Id: parseInt(id) })
    })
    .then(response => {
        if (!response.ok) throw new Error('Error en la eliminación');
        return response.json();
    })
    .then(data => {
        if (data.success) {
         
            window.location.reload();
        } else {
            alert("No se pudo eliminar la cita.");
        }
    })
    .catch(error => {
        console.error("Error al eliminar cita:", error);
        alert("Error al eliminar la cita.");
    });
}



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
                $('#modalEditarCita').modal('hide');
                alert("Cita actualizada con éxito.");
                
                marcarDiaConCita(response.nuevaFecha);
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

function mostrarEditarCitaDesdeDetalle(idCita) {
    citaSeleccionadaId = idCita;
    mostrarEditarCita();
}

$(document).ready(function () {
    $('#btnAbrirModal').click(function () {
        $.get('/Citas/Create', function (data) {
            $('#crearCitaModalBody').html(data);
            $('#crearCitaModal').modal('show');
            $.validator.unobtrusive.parse($('#crearCitaModalBody'));
        });
    });

    // Delegar el submit del formulario dentro del modal
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
                    $('#crearCitaModal').modal('hide');
                    marcarDiaConCita(response.nuevaFecha);
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
    function mostrarModalCrearCita() {
        document.getElementById('modalFondoCrear').style.display = 'block';
        document.getElementById('crearCitaModal').style.display = 'block';
    }

    function ocultarModalCrearCita() {
        document.getElementById('modalFondoCrear').style.display = 'none';
        document.getElementById('crearCitaModal').style.display = 'none';
    }
    function marcarDiaConCita(fechaISO) {
        const diaElemento = document.querySelector(`[data-fecha="${fechaISO}"]`);
        if (diaElemento) {
            diaElemento.classList.add("tiene-cita"); 
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

    agregarClickEventos(); 
}); 
