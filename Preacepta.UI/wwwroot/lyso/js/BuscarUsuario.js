document.addEventListener("click", function (event) {
    if (event.target.classList.contains("contact-btn--primario")) {
        const cedula = document.getElementById('InputBuscarPersona').value;
        const id = cedula;
        console.log("Valor seleccionado:", id);

        if (id) {
            fetch(`/Personas/DetallesPersona/${id}`)
                .then(response => response.json())
                .then(data => {
                    console.log(Object.keys(data));
                    console.log("Detalles obtenidos:", data);
                    // Aquí puedes actualizar el contenido del modal con los datos obtenidos
                    console.log("NombreEtapa:", data.Nombre);

                    if (data.bandera == true)
                    {
                        document.getElementById('Nombre').innerHTML = `<p>${data.nombre}</p>`;
                        document.getElementById('Apellido1').innerHTML = `<p>${data.apellido1} ${data.apellido2}</p>`;
                        document.getElementById('Oficio').innerHTML = `<p>${data.ocupacion}</p>`;
                        document.getElementById('Telefono').innerHTML = `<p>${data.telefono}</p>`;
                        document.getElementById('Correo').innerHTML = `<p>${data.correo}</p>`;
                    }

                    if (data.bandera == false)
                    {
                        document.getElementById('Nombre').innerHTML = `<p>No encontrado</p>`;
                        document.getElementById('Apellido1').innerHTML = `<p>No encontrado</p>`;
                        document.getElementById('Oficio').innerHTML = `<p>No encontrado</p>`;
                        document.getElementById('Telefono').innerHTML = `<p>No encontrado</p>`;
                        document.getElementById('Correo').innerHTML = `<p>No encontrado</p>`;

                    }



                    
                })
                .catch(error => {
                    console.error("Error al obtener detalles:", error);
                });
        }
    }
});