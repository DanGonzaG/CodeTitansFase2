document.addEventListener('DOMContentLoaded', function () {
    //Toma la etiquetas de nice-select y las aplica la se del TipoIdentificacion, que es el verdadero select
    const niceSelect = document.querySelector('.nice-select');
    const selectOriginal = document.getElementById('TipoIdentificacion');
    const numCedulaInput = document.getElementById('NumCedula');

    //Valida que lo objetos esten en le DOM
    if (!niceSelect || !selectOriginal || !numCedulaInput) {
        console.warn('Faltan elementos en el DOM.');
        return;
    }

    //Funciones para el formateo de los diferentes numeros de identificacion
    function formatearCedula(valor) {
        const soloNumeros = valor.replace(/\D/g, '').slice(0, 9);//permite que solo se digiten 9 numeros
        if (soloNumeros.length < 9) return soloNumeros; //valida que los numeros sean nueve y aplica guiones
        return `${soloNumeros[0]}-${soloNumeros.slice(1, 5)}-${soloNumeros.slice(5, 9)}`;
    }

    function formatearDIMEX(valor) {
        const soloNumeros = valor.replace(/\D/g, '').slice(0, 12);//permite que solo se digiten 12 numeros
        if (soloNumeros.length < 12) return soloNumeros; //valida que los numeros sean nueve y aplica guiones
        return `${soloNumeros[0]}-${soloNumeros.slice(1, 4)}-${soloNumeros.slice(4, 10)}-${soloNumeros.slice(10, 12)}`;
    }

    function formatearPasaporte(valor) {
        return valor.replace(/[^a-zA-Z0-9]/g, '').slice(0, 9); //permite letras pero solo nueve caracteres
    }

    function formatearSinID() {
        return "Sin número de identificación"; //inserta el texto en el input
    }

    // Actualiza el <select> oculto al hacer clic en el menú 
    niceSelect.addEventListener('click', function (event) {
        const clickedOption = event.target.closest('.option');
        if (!clickedOption) return;

        const tipo = clickedOption.getAttribute('data-value');
        selectOriginal.value = tipo;
        console.log('Tipo seleccionado:', tipo);

        if (tipo === 'SinDocumento') {
            const formateado = formatearSinID();
            numCedulaInput.value = formateado;
            numCedulaInput.setAttribute('readonly', true);
            console.log('Campo bloqueado con texto fijo');
        } else {
            numCedulaInput.removeAttribute('readonly');
            numCedulaInput.value = '';
            console.log('Campo reactivado');
        }
    });

    // Valida y formatea el número de cédula
    numCedulaInput.addEventListener('input', function () {
        if (numCedulaInput.hasAttribute('readonly')) return;
        const tipo = selectOriginal.value;
        let formateado = this.value;

        if (tipo === 'Cedula') {
            formateado = formatearCedula(this.value);
            this.value = formateado;
        }
        if (tipo === 'DIMEX') {
            formateado = formatearDIMEX(this.value);
            this.value = formateado;
        }
        if (tipo === 'Pasaporte') {
            formateado = formatearPasaporte(this.value);
            this.value = formateado;
        }

    });
});



