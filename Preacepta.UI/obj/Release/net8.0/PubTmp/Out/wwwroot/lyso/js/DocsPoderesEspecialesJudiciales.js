document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsPoderesEspecialesJudiciales').addEventListener('submit', (e) => {
});

document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const fecha = document.getElementById('Fecha').value;
    const idAbogado = document.getElementById('idIdAbogado').value;
    const idCliente = document.getElementById('idIdCliente').value;
    const texto = document.getElementById('idTexto').value;

    const NumCausa = document.getElementById('idNumCausa')?.value || '';

    const timestamp = new Date().getTime();

    const url = `/TDocsPoderesEspecialesJudiciales/PrevisualizarPDF` +
        `?idDoc=0` +
        `&fecha=${encodeURIComponent(fecha)}` +
        `&idAbogado=${encodeURIComponent(idAbogado)}` +
        `&idCliente=${encodeURIComponent(idCliente)}` +
        `&texto=${encodeURIComponent(texto)}` +
        `&NumCausa=${encodeURIComponent(NumCausa)}` + 
        `&t=${timestamp}`;

    window.open(url, '_blank');
});
