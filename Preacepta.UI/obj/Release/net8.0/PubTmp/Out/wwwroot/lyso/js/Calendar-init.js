

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

async function abrirModalListarDocumentos(idCita) {
    window.citaSeleccionadaId = idCita;
    
    if (!CryptoE2EE.getPrivateKeyCookie()) {
        console.warn(" No hay clave privada todavía. Se generará al subir el primer documento.");
    }

    // Cerrar modal detalle si está abierto
    const modalDetalleCita = document.getElementById('modalDetalleCita');
    if (modalDetalleCita) {
        modalDetalleCita.style.display = 'none';
        modalDetalleCita.setAttribute('aria-hidden', 'true');
    }

    try {
        const res = await fetch(`/DocumentosCita/Listar?idCita=${idCita}`);
        if (!res.ok) throw new Error(`Error HTTP ${res.status}`);
        const html = await res.text();

        const modalContainer = document.getElementById('modalContainer');
        if (!modalContainer) throw new Error("Contenedor de modal no encontrado");
        modalContainer.innerHTML = html;

        const modal = document.getElementById('modalListarDocumentos');
        if (!modal) throw new Error("Modal de listar documentos no encontrado");

        // Inicializar clave pública para cifrado
        const publicKeyInput = modal.querySelector("#publicKeyPem");
        if (publicKeyInput && publicKeyInput.value.trim()) {
            try {
                window.llavePublicaRSA = await importarRSAPublicKey(publicKeyInput.value.trim());
                console.log("Clave pública RSA cargada correctamente.");
            } catch (err) {
                console.error("Error al importar clave pública:", err);
                Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudo cargar la clave pública.' });
            }
        }

        modal.style.display = 'block';
        inicializarEventosSubida();
        // Cierre al hacer clic fuera del modal
        modal.addEventListener('click', e => {
            if (e.target === modal) modal.style.display = 'none';
        });
      
        const btnCerrar = modal.querySelector('#btnCerrarModal');
        if (btnCerrar) btnCerrar.addEventListener('click', () => modal.style.display = 'none');

        inicializarEventosModal();

    } catch (err) {
        console.error("Error cargando el modal de documentos:", err);
        Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudo cargar la lista de documentos.' });
    }
}


function inicializarEventosSubida() {
    const btnSubir = document.getElementById("btnSubirDocumentoCifrado");
    if (!btnSubir) return;

    btnSubir.addEventListener("click", async function () {
        const file = document.getElementById("archivo").files[0];
        if (!file) return Swal.fire("Seleccione un archivo");

        const respuesta = await subirDocumentoCifrado();

        const json = await respuesta.json();
        if (json.success) {
            Swal.fire("Subido", "Documento cifrado correctamente", "success");
            abrirModalListarDocumentos(window.citaSeleccionadaId);
        } else {
            Swal.fire("Error", json.message, "error");
        }
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

            document.querySelectorAll('.documento-item').forEach(function (li) {
                const id = parseInt(li.dataset.id);
                const descargar = li.querySelector('.chk-permitir').checked;
                const activo = li.querySelector('.chk-activo').checked;

                documentos.push({ id, descargar, activo });
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

            fetch('/DocumentosCita/ActualizarPermisosBatch', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(documentos),
            })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: '¡Actualizado!',
                            text: 'Permisos y estado de documentos actualizados correctamente.',
                            timer: 2000,
                            showConfirmButton: false
                        });

                        documentos.forEach(d => {
                            const li = document.querySelector(`.documento-item[data-id='${d.id}']`);
                            if (li) {
                                const chkActivo = li.querySelector('.chk-activo');
                                const spanEstado = li.querySelector('.estado-activo');

                                chkActivo.checked = d.activo;
                                spanEstado.innerText = d.activo ? "Habilitado" : "Deshabilitado";
                                li.style.opacity = d.activo ? 1 : 0.5;
                            }
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo actualizar: ' + (data.message || ''),
                            confirmButtonText: 'OK'
                        });
                    }
                })
                .catch(err => {
                    console.error("Error al actualizar permisos batch:", err);
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Ocurrió un error al actualizar los permisos.',
                        confirmButtonText: 'OK'
                    });
                });
        });
    }

    const modal = document.getElementById('modalListarDocumentos');
    if (modal) {
        modal.addEventListener('click', function (e) {
            if (e.target.classList.contains('btnDeshabilitarDoc')) {
                const li = e.target.closest('li');
                const idDoc = parseInt(li.dataset.id);

                Swal.fire({
                    title: '¿Deshabilitar documento?',
                    text: "Ya no estará disponible en detalles.",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, deshabilitar',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        fetch(`/DocumentosCita/Deshabilitar?id=${idDoc}`, { method: 'POST' })
                            .then(res => res.json())
                            .then(data => {
                                if (data.success) {
                                    Swal.fire({
                                        icon: 'success',
                                        title: '¡Deshabilitado!',
                                        text: 'El documento ya no estará disponible.',
                                        timer: 2000,
                                        showConfirmButton: false
                                    });

                                    if (li) {
                                        const chkActivo = li.querySelector('.chk-activo');
                                        const spanEstado = li.querySelector('.estado-activo');
                                        chkActivo.checked = false;
                                        spanEstado.innerText = "Deshabilitado";
                                        li.style.opacity = 0.5;
                                    }
                                    
                                    if (document.querySelectorAll('#modalListarDocumentos li').length === 0) {
                                        const msg = document.getElementById('mensajeSinDocumentos');
                                        if (msg) msg.style.display = "block";
                                    }
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        text: 'No se pudo deshabilitar el documento.',
                                        confirmButtonText: 'OK'
                                    });
                                }
                            })
                            .catch(err => {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'Ocurrió un error al deshabilitar el documento.',
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

function mostrarEditarCitaDesdeDetalle(idCita) {
    citaSeleccionadaId = idCita;
    mostrarEditarCita();
}

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

        window.addEventListener('click', function (e) {
            const modal = document.getElementById('crearCitaModal');
            if (modal && e.target === modal) cerrarCrearModal();
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
                    const estadoInput = $form.find('select[name="Estado"]').val();
                    if (estadoInput === "1") {  
                        cambiarEstadoCita(response.idCita, 1);
                    }
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

async function cambiarEstadoCita(idCita, nuevoEstado) {
    const response = await fetch('/Citas/CambiarEstadoEnviarCorreo', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ idCita, nuevoEstado })
    });

    const text = await response.text();
    console.log("Respuesta servidor:", text);

    let data;
    try {
        data = JSON.parse(text);
    } catch (err) {
        console.error("No es JSON válido:", err);
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'El servidor no devolvió una respuesta válida.',
        });
        return;
    }
    if (data.success) {
        let mensaje = "Estado actualizado correctamente.";
        if (nuevoEstado === 1) {
            mensaje = "¡Cita terminada! El correo fue enviado al cliente.";
        }
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: mensaje,
            timer: 2000,
            showConfirmButton: false
        });
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'No se pudo actualizar la cita.',
            showConfirmButton: true
        });
    }
}
// ----------------- UTILIDADES -----------------
function base64ToPem(base64) {
    const lines = base64.match(/.{1,64}/g).join("\n");
    return `-----BEGIN PUBLIC KEY-----\n${lines}\n-----END PUBLIC KEY-----`;
}
function leerArchivoComoArrayBuffer(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = reject;
        reader.readAsArrayBuffer(file);
    });
}

function toBase64(buffer) {
    return btoa(String.fromCharCode(...new Uint8Array(buffer)));
}

function limpiarBase64(str) {
    if (!str) return "";
    return str.replace(/[\r\n\s]+/g, "").replace(/-/g, "+").replace(/_/g, "/").trim();
}


function fromBase64(b64) {
    b64 = limpiarBase64(b64);
    const binary = atob(b64);
    const bytes = new Uint8Array(binary.length);
    for (let i = 0; i < binary.length; i++) {
        bytes[i] = binary.charCodeAt(i);
    }
    return bytes; 
}

async function cifrarConAES(key, datos) {
    const iv = window.crypto.getRandomValues(new Uint8Array(12));
    const cifrado = await window.crypto.subtle.encrypt({ name: "AES-GCM", iv }, key, datos);
    return { iv, cifrado };
}

async function descifrarArchivoAES(encryptedFile, aesKey, ivBase64) {
    const iv = new Uint8Array([...atob(ivBase64)].map(c => c.charCodeAt(0)));
    return crypto.subtle.decrypt(
        { name: "AES-GCM", iv },
        aesKey,
        encryptedFile
    );
}


// ----------------- RSA -----------------
function limpiarPEMPublicKey(pem) {
    if (!pem) throw new Error("La clave pública está vacía");
    if (!pem.startsWith("-----BEGIN PUBLIC KEY-----")) {
        pem = "-----BEGIN PUBLIC KEY-----\n" + pem + "\n-----END PUBLIC KEY-----";
    }
    const b64 = pem.replace(/-----BEGIN PUBLIC KEY-----|-----END PUBLIC KEY-----|\s/g, "");
    if (!b64) throw new Error("La clave pública no contiene datos válidos o no es SPKI.");
    return b64;
}

// ----------------- UTILIDADES RSA -----------------

/**
 * Formatea una clave base64 pura a PEM SPKI estándar
 * @param {string} pemBase64 - clave pública base64 desde SQL
 * @returns {string} PEM formateado con encabezado y saltos de línea
 */
function formatearClaveSPKI(pemBase64) {
    if (!pemBase64) throw new Error("Clave pública vacía");
    const b64 = pemBase64.replace(/\s+/g, ""); 
    let formatted = "-----BEGIN PUBLIC KEY-----\n";
    for (let i = 0; i < b64.length; i += 64) {
        formatted += b64.slice(i, i + 64) + "\n";
    }
    formatted += "-----END PUBLIC KEY-----";
    return formatted;
}
function wrapPEM(b64) {
    return (
        "-----BEGIN PUBLIC KEY-----\n" +
        b64.match(/.{1,64}/g).join("\n") +
        "\n-----END PUBLIC KEY-----"
    );
}

/**
 * Importa la clave pública RSA desde PEM SPKI
 * @param {string} pem - PEM con encabezado y saltos de línea
 * @returns {Promise<CryptoKey>} Clave RSA usable con Web Crypto
 */
async function importarRSAPublicKey(pem) {

    const cleaned = pem
        .replace(/-----(BEGIN|END) PUBLIC KEY-----/g, "")
        .replace(/\s/g, "")
        .trim();

    const binaryDer = Uint8Array.from(
        atob(cleaned),
        c => c.charCodeAt(0)
    );

    const key = await crypto.subtle.importKey(
        "spki",
        binaryDer.buffer,
        {
            name: "RSA-OAEP",
            hash: "SHA-256"
        },
        true,
        ["encrypt"]
    );

    
    window.rsaPublicKey = key;

    console.log("Clave pública RSA cargada correctamente");

    return key;
}


/**
 * Inicializa la clave pública RSA de forma segura desde cualquier input
 * @param {string} selectorInput - selector CSS del input que contiene la clave base64
 * @returns {Promise<CryptoKey|null>} Clave pública o null si falla
 */
async function inicializarClavePublica(selectorInput = "#publicKeyPem") {
    const input = document.querySelector(selectorInput);
    if (!input || !input.value.trim()) {
        console.warn("No hay clave pública disponible en el input", selectorInput);
        return null;
    }
    try {
        const pemFormateada = formatearClaveSPKI(input.value.trim());
        const publicKey = await importarRSAPublicKey(pemFormateada);
        window.llavePublicaRSA = publicKey; 
        console.log("Clave pública RSA cargada correctamente.");
        return publicKey;
    } catch (err) {
        console.error("Error al inicializar clave pública:", err);
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'La clave pública no es válida y no se pudo cargar.',
            confirmButtonText: 'OK'
        });
        return null;
    }
}


// ----------------- INICIALIZACIÓN CLAVE PÚBLICA -----------------
async function inicializarClavePublica() {
    const input = document.getElementById("publicKeyPem");
    if (!input) {
        console.warn("No hay input #publicKeyPem en el DOM todavía.");
        return null;
    }

    const pemRaw = input.value.trim();
    if (!pemRaw) {
        console.warn("No hay clave pública disponible.");
        return null;
    }

    try {
        const publicKey = await importarRSAPublicKey(pemRaw);
        window.llavePublicaRSA = publicKey;
        console.log("Clave pública RSA cargada correctamente.");
        return publicKey;
    } catch (err) {
        console.error("Error al inicializar clave pública:", err);
        return null;
    }
}



// ----------------- SUBIR DOCUMENTO -----------------

async function subirDocumentoCifrado() {
    const ok = await CryptoE2EE.ensurePrivateKey();
    if (!ok) return Swal.fire("No es posible subir el documento sin clave segura");

    const fileInput = document.getElementById("archivo");
    const file = fileInput.files[0];
    if (!file) return Swal.fire("Seleccione un archivo");

    const buffer = await file.arrayBuffer();

    let publicKeyBase64 = document.getElementById("publicKeyPem").value?.trim();


    if (!publicKeyBase64) {
        publicKeyBase64 = await CryptoE2EE.generateKeysAndStore();
        console.log("🔐 Nueva clave pública generada y guardada");
    }

 
    const { ciphertext, aesKeyRaw, ivBase64 } = await CryptoE2EE.encryptAESFile(buffer);

    const encryptedKeyRSA = await CryptoE2EE.encryptAESKeyWithRSA(aesKeyRaw, publicKeyBase64);

 
    const formData = new FormData();
    formData.append("ArchivoCifrado", new Blob([ciphertext]), file.name + ".enc");
    formData.append("NombreArchivo", file.name);
    formData.append("IV", ivBase64);
    formData.append("EncryptedKeyBase64", encryptedKeyRSA);
    formData.append("ContentType", file.type);
    formData.append("OwnerId", document.getElementById("OwnerId").value);
    formData.append("IdCita", document.getElementById("IdCita").value);

    return fetch("/SubirCifrado", { method: "POST", body: formData });
}



function fixBase64(b64) {
    if (!b64) return "";
    return b64
        .replace(/[\r\n\s]+/g, "")   
        .replace(/-/g, "+")
        .replace(/_/g, "/");
}
function fixBase64(str) {
    if (!str) return "";
    str = str.replace(/-/g, "+").replace(/_/g, "/").trim();
    while (str.length % 4 !== 0) str += "=";
    return str;
}
// ----------------- DESCARGAR/DESCIFRAR -----------------
async function procesoCompletoDescifrado(documentoId) {
    try {

        const resp = await fetch(`/DescargarCifrado/${documentoId}`);
        const data = await resp.json();

        const aesBytes = await CryptoE2EE.decryptAESKeyRSA(data.encryptedKeyBase64);

        const clearBytes = await CryptoE2EE.decryptFileAES(data.archivoBase64, data.iv, aesBytes);


        const blob = new Blob([clearBytes], { type: data.contentType });
        const link = document.createElement("a");
        link.href = URL.createObjectURL(blob);
        link.download = data.nombreArchivo;
        link.click();

        Swal.fire("Documento descifrado", "Descarga completa", "success");

    } catch (err) {
        console.error(err);
        Swal.fire("Error", err.message, "error");
    }
}


async function importarRSAPublicKeyDesdeBase64(base64) {
    const pem = convertirBase64APem(base64);

   
    const b64 = pem
        .replace(/-----(BEGIN|END) PUBLIC KEY-----/g, "")
        .replace(/\s+/g, "");

    const binary = atob(b64);
    const binaryDer = new Uint8Array(binary.length);
    for (let i = 0; i < binary.length; i++) {
        binaryDer[i] = binary.charCodeAt(i);
    }

    return crypto.subtle.importKey(
        "spki",
        binaryDer.buffer,
        { name: "RSA-OAEP", hash: "SHA-256" },
        true,
        ["encrypt"]
    );
}
async function generarParClaves(password) {


    const keyPair = await crypto.subtle.generateKey(
        {
            name: "RSA-OAEP",
            modulusLength: 2048,
            publicExponent: new Uint8Array([1, 0, 1]),
            hash: "SHA-256"
        },
        true,
        ["encrypt", "decrypt"]
    );


    const publicKeySpki = await crypto.subtle.exportKey("spki", keyPair.publicKey);
    const privateKeyPkcs8 = await crypto.subtle.exportKey("pkcs8", keyPair.privateKey);

    const publicKeyBase64 = btoa(String.fromCharCode(...new Uint8Array(publicKeySpki)));
    const privateKeyRaw = new Uint8Array(privateKeyPkcs8);


    const salt = crypto.getRandomValues(new Uint8Array(16));
    const iv = crypto.getRandomValues(new Uint8Array(12));

    const aesKey = await crypto.subtle.deriveKey(
        {
            name: "PBKDF2",
            salt: salt,
            iterations: 100000,
            hash: "SHA-256"
        },
        await crypto.subtle.importKey("raw", new TextEncoder().encode(password), "PBKDF2", false, ["deriveKey"]),
        { name: "AES-GCM", length: 256 },
        false,
        ["encrypt"]
    );


    const encryptedPrivateKey = await crypto.subtle.encrypt(
        { name: "AES-GCM", iv: iv },
        aesKey,
        privateKeyRaw
    );

    await fetch("/Usuarios/GuardarClaves", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            publicKeyBase64,
            encryptedPrivateKey: btoa(String.fromCharCode(...new Uint8Array(encryptedPrivateKey))),
            salt: btoa(String.fromCharCode(...salt)),
            iv: btoa(String.fromCharCode(...iv))
        })
    });

    alert("Claves generadas y almacenadas de forma segura");
}

window.subirDocumentoCifrado = subirDocumentoCifrado;
window.procesoCompletoDescifrado = procesoCompletoDescifrado;

window.abrirModalListarDocumentos = abrirModalListarDocumentos;
