function abrirModalReunionZoom(inputSelector = "#crearCitaModal input[name='LinkVideo']") {
    const inputLinkVideo = document.querySelector(inputSelector);
    const grupoLinkVideo = document.getElementById("grupoLinkVideo");

    fetch('/Reuniones/Crear')
        .then(res => {
            if (!res.ok) throw new Error("Error al cargar el modal");
            return res.text();
        })
        .then(html => {
            console.log(html);
            const modalContainer = document.getElementById("modalContainer");
            modalContainer.innerHTML = "";
            modalContainer.innerHTML = html;

            const modal = document.getElementById("modalProgramar");
            if (modal) {
  
                modal.style.display = "flex";
            }

            const btnCerrar = document.getElementById("btnCerrarModal");
            if (btnCerrar) {
                btnCerrar.onclick = () => {
                    if (modal) {
                        modal.style.display = "none";
                    }
                    modalContainer.innerHTML = "";
                    // Mostrar el modal de crear cita nuevamente
                    const crearCitaModal = document.getElementById("crearCitaModal");
                    if (crearCitaModal) {
                        crearCitaModal.style.display = "flex";
                    }
                };
            }

            // Cerrar modal si el usuario hace click fuera del contenido
            window.addEventListener("click", (event) => {
                if (event.target === modal) {
                    if (modal) {
                        modal.style.display = "none";
                    }
                    modalContainer.innerHTML = "";
                }
            });

            const form = document.getElementById("formReunion");
            if (form) {
                form.onsubmit = (e) => {
                    e.preventDefault();

                    const datos = {
                        FechaInicio: document.getElementById("FechaInicio").value,
                        Duracion: parseInt(document.getElementById("Duracion").value),
                        Tema: document.getElementById("Tema").value,
                        Participantes: document.getElementById('Participantes').value
                    };
                    const emails = datos.Participantes.split(',').map(e => e.trim());
                    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

                    const todosValidos = emails.every(email => emailRegex.test(email));
                    if (!todosValidos) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Correo inválido',
                            text: 'Uno o más correos electrónicos tienen un formato incorrecto.'
                        });
                        return;
                    }
                    fetch("/Reuniones/CrearReunion", {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify(datos)
                    })
                        .then(res => res.json())
                        .then(res => {
                            console.log("Respuesta crear reunión:", res);
                            const resultado = document.getElementById("resultado");
                            if (res.success) {
                            
                                Swal.fire({
                                    icon: 'success',
                                    title: '¡Reunión programada!',
                                    html: `La reunión virtual de Zoom se programó correctamente.<br>
                                           <a href="${res.url}" target="_blank">${res.url}</a>`,
                                    confirmButtonText: 'Aceptar'
                                });
                                // Insertar el link en el input del modal de Crear Cita
                                const inputLinkVideo = document.querySelector(inputSelector);
                                if (inputLinkVideo) {
                                    inputLinkVideo.value = res.url;
                                }

                                const grupoLink = document.getElementById("grupoLinkVideo");
                                if (grupoLink) {
                                    grupoLink.classList.remove("d-none");
                                }

                                resultado.innerHTML = `<div style="color:green;">
                                Reunión creada: <a href="${res.url}" target="_blank">${res.url}</a>
                                 </div>`;

                                const modal = document.getElementById("modalProgramar");
                                if (modal) modal.style.display = "none";

                                const crearCitaModal = document.getElementById("crearCitaModal");
                                if (crearCitaModal) crearCitaModal.style.display = "flex";

                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    text: res.error || "No se pudo programar la reunión."
                                
                                });
                            }
                        })
                        .catch(error => {
                            console.error(error);
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: "Ocurrió un error al crear la reunión"
                            });
                        });
                };
            }
        })
        .catch(err => {
            console.error(err);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: "No se pudo cargar el formulario de reunión."
            });
        });
}
