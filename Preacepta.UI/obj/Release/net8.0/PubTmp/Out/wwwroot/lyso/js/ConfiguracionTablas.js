/*$(document).ready(function () {
    const $tablaFuturas = $('#listaCasosCerrados');
    //const $tablaFuturas = $('table');

    // Inicializar DataTable solo si la tabla existe y aún no ha sido inicializada
    if ($tablaFuturas.length && !$.fn.DataTable.isDataTable($tablaFuturas)) {
        $tablaFuturas.DataTable({
            language: {
                url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'
            },
            pageLength: 15,
            lengthChange: false,
            searching: false,
            order: [[0, "desc"]]

        });
    }
});*/


$(document).ready(function () {
    const $tabla = $('table');

    if ($tabla.length === 0) {
        console.warn("La tabla #listaCasosCerrados no se encontró en el DOM.");
        return;
    }

    if (!$.fn.DataTable) {
        console.error("El plugin DataTables no está disponible. ¿Se cargó correctamente el JS?");
        return;
    }

    try {
        if (!$.fn.DataTable.isDataTable($tabla)) {
            console.log("Inicializando DataTable para #listaCasosCerrados…");
            $tabla.DataTable({
                columnDefs: [ {
                    targets: '_all', defaultContent: '-'
                }],
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json',                    
                    emptyTable: '<div class="col-12 text-center"><div class="alert alert-info">No cuenta con casos en el despacho</div></div>'
                },
                pageLength: 15,
                lengthChange: false,
                searching: false,
                order: [[0, "desc"]]
            });
        }
    } catch (e) {
        console.error("Error al intentar inicializar la DataTable:", e);
    }
});