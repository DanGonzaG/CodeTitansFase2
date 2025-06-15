// Mostrar el botón de "Crear Documento"
document.getElementById('btnCrearCaso').style.display = 'block';

document.getElementById('CreateDocsAutorizacionRevisionExpedientes').addEventListener('submit', (e) => {
    
});

// Función para abrir el PDF de previsualización en una nueva ventana
document.getElementById('btnPrevisualizar').addEventListener('click', () => {
    const expediente = document.getElementById('idExpediente').value;
    const delito = document.getElementById('idDelito').value;
    const cedulaImputado = document.getElementById('idCedulaImputado').value;
    const ofendido = document.getElementById('idOfendido').value;
    const cedulaAbogado = document.getElementById('idCedulaAbogado').value;
    const cedulaAsistente = document.getElementById('idCedulaAsistente').value;

    const timestamp = new Date().getTime();

    const url = `/DocsAutorizacionRevisionExpedientes/PrevisualizarPDF?expediente=${encodeURIComponent(expediente)}&delito=${encodeURIComponent(delito)}&cedulaImputado=${encodeURIComponent(cedulaImputado)}&ofendido=${encodeURIComponent(ofendido)}&cedulaAbogado=${encodeURIComponent(cedulaAbogado)}&cedulaAsistente=${encodeURIComponent(cedulaAsistente)}&t=${timestamp}`;

    window.open(url, '_blank');
});