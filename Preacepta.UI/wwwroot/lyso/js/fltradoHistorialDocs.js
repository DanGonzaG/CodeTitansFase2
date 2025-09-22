document.addEventListener('DOMContentLoaded', () => {

    let boton = document.getElementById("botonBuscarHistorial");
    let search = document.getElementById("inputFechaHistorial");
    let docs = document.getElementById("selectTipoDocumento");

    if (!search || !boton || !docs) {
        console.error("No se encontró el caso");
        return;
    }

    search.addEventListener("keypress", (e) => {
        if (e.key === "Enter") {
            e.preventDefault(); 
            boton.click();
        }
    });

    boton.addEventListener("click", () => {
        console.log("Filtro activado");

        const valorFecha = search.value.trim();
        const valorTipo = docs.value.trim().toLowerCase();

        const elementos = document.getElementsByClassName("filtrosHistorial");

        for (let elemento of elementos) {
            const fechaElemento = elemento.getAttribute('fecha')?.trim() || "";
            const tipoElemento = elemento.getAttribute('nombreTipoDocumento')?.trim().toLowerCase() || "";

            const coincideFecha = !valorFecha || fechaElemento === valorFecha;
            const coincideTipo = !valorTipo || tipoElemento.includes(valorTipo);

            if (coincideFecha && coincideTipo) {
                elemento.style.display = 'table-row';
            } else {
                elemento.style.display = 'none';
            }
        }
    });

});
