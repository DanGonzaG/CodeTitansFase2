document.addEventListener('DOMContentLoaded', () => {

    let boton = document.getElementById("BtnBuscarCaso")
    let search = document.getElementById("InputBusquedaCaso")


    if (!search || !boton) {
        console.error("No se encontro el caso")
        return
    }

    search.addEventListener("keypress", (e) => {
        if (e.key === "Enter") {
            e.preventDefault(); // Evita la acción predeterminada
            boton.click(); // Simula clic en el botón
        }
    });

    boton.addEventListener("click", () => {
        const valorInput = search.value.toUpperCase().trim();
        const elementos = document.getElementsByClassName("losCasosFiltrados");

        for (let elemento of elementos) {
            const idCaso = elemento.getAttribute('idCaso')?.toUpperCase() || "";
            if (idCaso.includes(valorInput)) {
                elemento.style.display = 'table-row'; // Mostrar la fila
            } else {
                elemento.style.display = 'none';
            }
        }

    });

});