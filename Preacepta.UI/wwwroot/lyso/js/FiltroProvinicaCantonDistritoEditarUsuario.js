document.addEventListener("DOMContentLoaded", function () {
    fetch("/Ubicacion/Provincias") //llama el controller y metodo en controller usa la equita route al igual que el metodo usa la equite httpsGet
        .then(response => response.json())
        .then(data => {
            console.log("Provincias obtenidas:", data);
            const selectProvincia = document.getElementById("provincia");

            let optionsHtml = '<option value="">Seleccione una provincia</option>';
            data.forEach(provincia => {
                optionsHtml += `<option value="${provincia.idProvincia}">${provincia.nombreProvincia}</option>`;
            });

            selectProvincia.innerHTML = optionsHtml;
            selectProvincia.style.display = "block";

            //Actualizar Nice Select después de agregar las opciones
            $('#provincia').niceSelect('update');
        });
});

document.getElementById("provincia").addEventListener("change", function () {
    const idProvincia = this.value;
    const selectCanton = document.getElementById("canton");
    //const inputCanton = document.getElementById('inputCanton')

    console.log("Evento change ejecutado");
    console.log("Valor seleccionado:", idProvincia);

    selectCanton.style.display = "none";
    //inputCanton.style.display = "block";
    //inputCanton.disabled = true;

    selectCanton.innerHTML = '<option value="">Seleccione un cantón</option>';

    if (idProvincia) {
        fetch(`/Ubicacion/Cantones/${idProvincia}`)
            .then(response => response.json())
            .then(data => {
                console.log("Cantones obtenidos:", data);

                let optionsHtml = '<option value="">Seleccione un cantón</option>';
                data.forEach(canton => {
                    optionsHtml += `<option value="${canton.idCanton}">${canton.nombreCanton}</option>`;
                });

                selectCanton.innerHTML = optionsHtml;
                selectCanton.disabled = false;                
                selectCanton.style.display = "block";
                //inputCanton.style.display = "none";

                // actuliza el nice select
                $('#canton').niceSelect('update');
            })
            .catch(error => {
                console.error("Error al obtener cantones:", error);
                selectCanton.innerHTML = '<option value="">Error al cargar los cantones</option>';
                selectCanton.disabled = true;
                
            });

    } else {
        selectCanton.disabled = true;
        document.getElementById("distrito").disabled = true;
    }
});

document.getElementById("canton").addEventListener("change", function () {
   /* document.getElementById("distrito").addEventListener("change", function () {
        const inputDistrito = document.getElementById("inputDistrito");
        inputDistrito.value = this.value; // Guarda el ID numérico del distrito seleccionado
        console.log(inputDistrito);
    });*/
    const idCanton = this.value;
    const selectDistrito = document.getElementById("distrito");
    //const inputDistrito = document.getElementById('inputDistrito')

    console.log("Evento change ejecutado");
    console.log("Valor seleccionado:", idCanton);

    selectDistrito.style.display = "none";
    //inputDistrito.style.display = "block";
    //inputDistrito.disabled = true;

    selectDistrito.innerHTML = '<option value="">Seleccione un distrito</option>';

    if (idCanton) {
        fetch(`/Ubicacion/Distritos/${idCanton}`)
            .then(response => response.json())
            .then(data => {
                console.log("Distritos obtenidos:", data);

                let optionsHtml = '<option value="">Seleccione un distrito</option>';
                data.forEach(distrito => {
                    optionsHtml += `<option value="${distrito.idDistrito}">${distrito.nombreDistrito}</option>`;
                });

                selectDistrito.innerHTML = optionsHtml;
                selectDistrito.disabled = false;
                selectDistrito.style.display = "block";                
                //inputDistrito.style.display = "none";

                // actuliza el nice select
                $('#canton').niceSelect('update');
            })
            .catch(error => {
                console.error("Error al obtener cantones:", error);
                selectDistrito.innerHTML = '<option value="">Error al cargar los distritos</option>';
                selectDistrito.disabled = true;
            });

    } else {
        selectDistrito.disabled = true;
    }
    /*document.querySelector("form").addEventListener("submit", function () {
        document.getElementById("inputDistrito").disabled = false;
    });*/
});