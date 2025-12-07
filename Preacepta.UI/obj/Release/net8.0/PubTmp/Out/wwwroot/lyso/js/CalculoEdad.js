document.addEventListener('DOMContentLoaded', function () {
    const campoFecha = document.getElementById('InputFechaNacimiento');
    const campoEdad = document.getElementById('InputEdad');
    let valor1 = null;

    /*if (campoFecha) {
        const hoy = new Date().toISOString().split("T")[0];
        campoFecha.setAttribute("max", hoy);
    }*/

    if (campoFecha == null) {
        return
    }  

    campoFecha.addEventListener('input', function (evento) {
        const fecha = evento.target.value;
        if (!fecha) {
            valor1 = null;
            campoEdad.value = '';
            console.log('fecha no valida')
            return
        }

        valor1 = new Date(fecha);
        console.log(valor1)

        mostrarEdad();
    });


    function mostrarEdad() {
        if (!valor1) return;

        const FechaActual = new Date();
        let Edad = FechaActual.getFullYear() - valor1.getFullYear();
        const mesActual = FechaActual.getMonth();
        const diaActual = FechaActual.getDate();
        const mesNacimiento = valor1.getMonth();
        const diaNacimiento = valor1.getDate();

        if (mesActual < mesNacimiento || (mesActual === mesNacimiento && diaActual < diaNacimiento)) {
            Edad--; // Restar un año si el cumpleaños aún no ha pasado
        }

        if (Edad < 1)
        {
            Edad = 0
        }

        console.log('Edad:', Edad);
        campoEdad.value = Edad;
    }
});
