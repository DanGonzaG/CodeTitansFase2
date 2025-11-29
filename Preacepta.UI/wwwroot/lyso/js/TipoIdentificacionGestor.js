document.addEventListener('DOMContentLoaded', function () {
    const selectOriginal = document.getElementById('TipoIdentificacion');
    const numCedulaInput = document.getElementById('NumCedula');

    if (!selectOriginal || !numCedulaInput) {
        console.warn('Faltan elementos en el DOM.');
        return;
    }

    // Formateadores por tipo
    function formatearCedula(valor) {
        const soloNumeros = valor.replace(/\D/g, '').slice(0, 9);
        if (soloNumeros.length < 9) return soloNumeros;
        return `${soloNumeros[0]}-${soloNumeros.slice(1, 5)}-${soloNumeros.slice(5, 9)}`;
    }

    function formatearDIMEX(valor) {
        const soloNumeros = valor.replace(/\D/g, '').slice(0, 12);
        if (soloNumeros.length < 12) return soloNumeros;
        return `${soloNumeros[0]}-${soloNumeros.slice(1, 4)}-${soloNumeros.slice(4, 10)}-${soloNumeros.slice(10, 12)}`;
    }

    function formatearPasaporte(valor) {
        return valor.replace(/[^a-zA-Z0-9]/g, '').slice(0, 9);
    }

    function formatearSinID() {
        return "Sin número de identificación";
    }

    // Evento al cambiar el tipo de identificación
    selectOriginal.addEventListener('change', function () {
        const tipo = this.value;
        console.log('Tipo seleccionado:', tipo);

        if (tipo === 'SinDocumento') {
            numCedulaInput.value = formatearSinID();
            numCedulaInput.setAttribute('readonly', true);
            console.log('Campo bloqueado con texto fijo');
        } else {
            numCedulaInput.removeAttribute('readonly');
            numCedulaInput.value = '';
            console.log('Campo reactivado');
        }
    });

    // Evento al escribir en el campo de cédula
    numCedulaInput.addEventListener('input', function () {
        if (this.hasAttribute('readonly')) return;

        const tipo = selectOriginal.value;
        let formateado = this.value;

        if (tipo === 'Cedula') {
            formateado = formatearCedula(formateado);
        } else if (tipo === 'DIMEX') {
            formateado = formatearDIMEX(formateado);
        } else if (tipo === 'Pasaporte') {
            formateado = formatearPasaporte(formateado);
        }

        this.value = formateado;
    });
});



