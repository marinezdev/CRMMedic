function Mensajes(titulo, mensaje, tipo) {
    var clase = "";
    var icono = "";
    switch (tipo) {
        case 1:
            clase = 'btn btn-info';
            icono = "info";
            break;
        case 2:
            clase = 'btn btn-success';
            icono = "success";
            break;
        case 3:
            clase = 'btn btn-warning';
            icono = "warning";
            break;
        case 4:
            clase = 'btn btn-danger';
            icono = "error";
            break;
    }
    swal({
        title: titulo,
        text: mensaje,
        icon: icono,
        buttons: {
            confirm: {
                className: clase
            }
        }
    });
}

function obtenerParametroUrl(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};