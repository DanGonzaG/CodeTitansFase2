let valor1 = null;
const campoFecha = document.getElementById('InputFechaNacimiento');
const campoEdad = document.getElementById('InputEdad');

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
    console.log('Edad:', Edad);
    campoEdad.value = Edad;
}