//Vista Formulario caso
//Oculta le boton de crear caso
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsPoderesEspecialesJudiciales').addEventListener('submit', (e) => {
});

document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const fecha = document.getElementById('idFecha').value;
    const idAbogado = document.getElementById('idIdAbogado').value;
    const idCliente = document.getElementById('idIdCliente').value;
    const texto = document.getElementById('idTexto').value;

    const timestamp = new Date().getTime();

    const url = `/TDocsPoderesEspecialesJudiciales/PrevisualizarPDF` +
        `?idDoc=0&` +
        `&fecha=${encodeURIComponent(fecha)}` +
        `&idAbogado=${encodeURIComponent(idAbogado)}` +
        `&idCliente=${encodeURIComponent(idCliente)}` +
        `&texto=${encodeURIComponent(texto)}` +
        `&t=${timestamp}`;

    window.open(url, '_blank');
});