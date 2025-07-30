document.addEventListener("click", function (event) {
    if (event.target.classList.contains("contact-btn--revision")) {
        const idCaso = event.target.value;
        console.log("Valor seleccionado:", idCaso);

        if (idCaso) {
            fetch(`/CasosEtapa/DetalleEtapas/${idCaso}`)
                .then(response => response.json())
                .then(data => {
                    console.log(Object.keys(data));
                    console.log("Detalles obtenidos:", data);
                    // Aquí puedes actualizar el contenido del modal con los datos obtenidos
                    console.log("NombreEtapa:", data.NombreEtapa);

                    document.getElementById('Nombre').innerHTML = `<strong>Descripción:</strong> <p>${data.nombreEtapa}</p>`;
                    document.getElementById('FechaEtapa').innerHTML = `<strong>Fecha de Etapa:</strong> <p>${data.fechaEtapa}</p>`;
                    document.getElementById('NumeroEtapa').innerHTML = `<strong>Número de Etapa:</strong> <p>${data.id}</p>`;
                    document.getElementById('DescripcionEtapa').innerHTML = `<strong>Detalles:</strong> <p>${data.descripcioEtapa}</p>`;
                    document.getElementById('CasoId').innerHTML = `<strong>Número de Caso:</strong> <p>${data.casoId}</p>`;
                    console.log(document.getElementById('inputIdCaso'));
                    document.getElementById('inputEditEtapaPL').value = data.id ?? "Id de etapa legal no encontrado";
                    document.getElementById('inputDeleteEtapaPL').value = data.id ?? "Id de etapa legal no encontrado";
                    document.getElementById('inputDescargaEtapaPL').value = data.id ?? "Id de etapa legal no encontrado";
                })
                .catch(error => {
                    console.error("Error al obtener detalles:", error);
                });
        }
    }
});