document.getElementById("btnAbrirModalReunion").addEventListener("click", () => {
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
                        alert("Uno o más correos electrónicos tienen un formato inválido.");
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
                                resultado.innerHTML = `<div style="color:green;">
                                Reunión creada: <a href="${res.url}" target="_blank">${res.url}</a>
                            </div>`;
                            } else {
                                resultado.innerHTML = `<div style="color:red;">Error: ${res.error}</div>`;
                            }
                        })
                        .catch(error => {
                            console.error(error);
                            const resultado = document.getElementById("resultado");
                            resultado.innerHTML = `<div style="color:red;">Error al crear la reunión</div>`;
                        });
                };
            }
        })
        .catch(err => {
            console.error(err);
            alert("No se pudo cargar el formulario de reunión.");
        });
});
