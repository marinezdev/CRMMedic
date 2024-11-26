$(document).ready(function () {
    $('#opcEmpresas').addClass("nav-item active submenu");

    var obtenerParametrodeURL = function getUrlParameter(sParam) {
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

    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9\-]/g, '');
    });

    var idd = obtenerParametrodeURL('Id');
    SeleccionarPorId(idd);
    SeleccionarAdicionalPorId(idd);

    $('#IdEmpresa').val(idd);

    if ($('#Contadas').val() >= 1 || $('#ContactosContados').val() >= 1) {
        $('#Activo').prop('disabled', true);
    }

    $('#BtnModificarAdicional').hide();
    $('#BtnModificarDireccionAdicional').hide();

});

function SeleccionarPorId(id) {
    $.ajax({
        url: "/Empresas/SeleccionarPorId/" + id
        , type: "GET"
        , contentType: "application/json"
        , dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            $('#Nombre').val(data.Nombre);
            $('#titulo').text('Edición de Médico: ' + data.Nombre);
            //$('#Correo').val(data.Correo);
            $('#Telefono').val(data.Telefono);
            $('#Extension').val(data.Extension);
            $('#PaginaWeb').val(data.PaginaWeb);
            $('#Direccion').val(data.Direccion);
            $('#CP').val(data.CP);
            $('#Ciudad').val(data.Ciudad);
            $('#divCP').text(data.Ciudad);
            $('#hCiudad').val(data.Ciudad);
            $('#Estado').val(data.Estado);

            if (data.Prospecto) {                          // Para un checkbox
                $("#Prospecto").prop('checked', true);
            }
            else {
                $("#Prospecto").prop('checked', false);
            }
            if (data.Cliente) {                          // Para un checkbox
                $("#Cliente").prop('checked', true);
            }
            else {
                $("#Cliente").prop('checked', false);
            }
            if (data.Competidor) {                          // Para un checkbox
                $("#Competidor").prop('checked', true);
            }
            else {
                $("#Competidor").prop('checked', false);
            }

            $('#Sector').val(data.Sector);
            $('#RFC').val(data.RFC);
            $('#Id').val(data.Id);
            $('#Espera').hide();

            if (data.InternaExterna === 1) {                          // Para un radiobutton
                $('input:radio[class=form-radio-input][id=test1]').prop('checked', true);
            }
            else {
                $('input:radio[class=form-radio-input][id=test1]').prop('checked', false);
            }

            if (data.InternaExterna === 2) {                          // Para un radiobutton
                $('input:radio[class=form-radio-input][id=test2]').prop('checked', true);
            }
            else {
                $('input:radio[class=form-radio-input][id=test2]').prop('checked', false);
            }

            if (data.Tipo === 1) {                          // Para un radiobutton
                $('input:radio[class=form-radio-input][id=tipoemp1]').prop('checked', true);
            }
            else {
                $('input:radio[class=form-radio-input][id=tipoemp1]').prop('checked', false);
            }

            if (data.Tipo === 2) {                          // Para un radiobutton
                $('input:radio[class=form-radio-input][id=tipoemp2]').prop('checked', true);
            }
            else {
                $('input:radio[class=form-radio-input][id=tipoemp2]').prop('checked', false);
            }

            $('#Pais').val(data.Pais);
            $('#UDN').val(data.IdUDN);

            if (data.Activo)
                $("#Activo").prop('checked', true);

            MostrarCompartidos(data.Id);
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function SeleccionarAdicionalPorId(id) {
    $.ajax({
        url: "/Empresas/SeleccionarAdicionalPorId/" + id
        , type: "GET"
        , contentType: "application/json"
        , dataType: "json",
        success: function (data) {            
            $('#Fuente').val(data.IdFuente);
            $('#Tipo').val(data.IdTipo);
            $('#Industria').val(data.IdIndustria);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function Guardar() {
    var pro = $("#Prospecto").val();
    alert(pro);

    $.ajax({
        url: "/Empresas/Guardar/",
        data: {
            Nombre: $('#Nombre').val(),
            Correo: $('#Correo').val(),
            Telefono: $('#Telefono').val(),
            PaginaWeb: $('#PaginaWeb').val(),
            Direccion: $('#Direccion').val(),
            Ciudad: $('#Ciudad').val(),
            Estado: $('#Estado').val(),
            Prospecto: $("#Prospecto").val() === 'on' ? true : false,
            Cliente: $("#Cliente").val() === 'on' ? true : false,
            Competidor: $("#Competidor").val() === 'on' ? true : false,
            Sector: $('#Sector').val(),
            IdUsuario: $('#Id').val(),
            RFC: $('#RFC').val()
        },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            swal({
                title: 'CRM ASAE',
                text: 'Se guardó el nuevo registro',
                icon: 'success',
                buttons: {
                    confirm: {
                        className: 'btn btn-success'
                    }
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function Cambiar() {
    ValidarEmpresaUsuarioPermisos()
    $.ajax({
        url: "/Empresas/Cambiar/",
        data: {
            Nombre: $('#Nombre').val(),
            Correo: $('#Correo').val(),
            Telefono: $('#Telefono').val(),
            PaginaWeb: $('#PaginaWeb').val(),
            Direccion: $('#Direccion').val(),
            CP: $('#CP').val(),
            Ciudad: $('#divCP').html(),
            Estado: $('#Estado').val(),
            Prospecto: $("#Prospecto").val(),
            Cliente: $("#Cliente").val(),
            Competidor: $("#Competidor").val(),
            Sector: $('#Sector').val(),
            Id: $('#RFC').val(),
            RFC: $('#IdEmpresa').val(),
            UDN: $('#UDN').val()
        },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            swal({
                title: 'CRM ASAE',
                text: 'Se guardó el registro modificado',
                icon: 'success',
                buttons: {
                    confirm: {
                        className: 'btn btn-success'
                    }
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function Compartir() {
    $.ajax({
        url: "/Empresas/CompartirRegistro/",
        data: { idempresa: $('#Id').val(), idusuario: $('#NuevoResponsable').val() },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            //console.log(data);
            if (data == "1") {
                $('#ModalCompartir').modal('hide');
                swal({
                    title: 'CRM ASAE',
                    text: 'Se compartió este registro con el usuario asignado.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-success'
                        }
                    },
                });
                MostrarCompartidos($('#Id').val());
            }
            else {
                $('#ModalCompartir').modal('hide');
                swal({
                    title: 'CRM ASAE',
                    text: 'El registro ya está compartido con el usuario indicado.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-warning'
                        }
                    },
                });
            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function GuardarAdicional() {
    $.ajax({
        url: "/Empresas/GuardarAdicional/",
        data: { idempresa: $('#Id').val(), idfuente: $('#Fuente').val(), idtipo: $('#Tipo').val(), idindustria: $('#Industria').val() },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            //console.log(data);
            if (data === 1) {
                swal({
                    title: 'CRM ASAE',
                    text: 'Se agregaron los datos.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-success'
                        }
                    },
                });
            }
            else if (data >= 3) {
                swal({
                    title: 'CRM ASAE',
                    text: 'Se guardaron los cambios.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-success'
                        }
                    },
                });
            }
            else if (data === 0) {
                swal({
                    title: 'CRM ASAE',
                    text: 'No se modificó el registro, sólo puede hacerlo el que lo creó, solicítele cualquier cambio.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-warning'
                        }
                    },
                }).then(
                    function () {
                        SolicitarCambios($('#IdUsuario').val(), $('#Id').val())
                    });
            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function GuardarDireccionAdicional() {
    $.ajax({
        url: "/Empresas/GuardarDireccionAdicional/",
        data: {
            idempresa: $('#Id').val(),
            idtipodireccion: $('input:radio[name=tipodireccion]:checked').val(),
            direccion: $('#DireccionAdicional').val(),
            cp: $('#CPAdicional').val(),
            ciudad: $('#divCPAdicional').text(),
            idpais: $('#PaisAdicional').val()
        },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            //console.log(data);
            if (data != 0) {
                swal({
                    title: 'CRM ASAE',
                    text: 'Se agregaron los datos.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-success'
                        }
                    },
                }).then(
                    function () {
                        $('#DireccionAdicional').val('');
                        $('#CPAdicional').val('');
                        $('#divCPAdicional').text('');
                        $('#PaisAdicional').val('0');
                        //Recargar la tabla
                        $('#basic-datatables').empty();
                        var firstRow = '<thead><tr><th>Tipo</th><th>Dirección</th><th>CP</th><th>Colonia, Ciudad, Estado</th><th>País</th></tr></thead><tbody>';
                        $('#basic-datatables').append(firstRow);
                        for (var i = 0; i < data.length; i++) {
                            var tipodireccion = data[i].IdTipoDireccion === 1 ? 'Dirección' : 'Fiscal';
                            var row = '<tr>' +
                                '<td>' + tipodireccion + '</td>' +
                                '<td>' + data[i].Direccion + '</td>' +
                                '<td>' + data[i].CP + '</td>' +
                                '<td>' + data[i].Ciudad + '</td>' +
                                '<td>' + data[i].NombrePais + '</td>' +
                                '</tr>';
                            $('#basic-datatables').append(row);
                        }
                        var lastRow = '</tbody></table>';
                        $('#basic-datatables').append(lastRow);
                    });
            }
            else {
                swal({
                    title: 'CRM ASAE',
                    text: 'No se modificó el registro, sólo puede hacerlo el que lo creó, solicítele cualquier cambio.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-warning'
                        }
                    },
                }).then(
                    function () {
                        SolicitarCambios($('#IdUsuario').val(), $('#Id').val())
                    });
            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function SeleccionarDireccionAdicionalPorId(id) {
    $.ajax({
        url: "/Empresas/SeleccionarDireccionAdicionalPorId/",
        data: { iddireccionadicional: id },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            $('#BtnGuardarDireccionAdicional').hide();
            $('#BtnModificarDireccionAdicional').show();
            $('#DireccionAdicional').val(data.Direccion);
            $('#CPAdicional').val(data.CP);
            $('#Ciudad').val(data.Ciudad);
            $('#divCPAdicional').text(data.Ciudad);
            $('#hCiudadAdicional').val(data.Ciudad);
            if (data.IdTipoDireccion === 1) {                          // Para un radiobutton
                $('input:radio[class=form-radio-input][id=tipodireccion1]').prop('checked', true);
            }
            else {
                $('input:radio[class=form-radio-input][id=tipodireccion1]').prop('checked', false);
            }
            if (data.IdTipoDireccion === 2) {                          // Para un radiobutton
                $('input:radio[class=form-radio-input][id=tipodireccion2]').prop('checked', true);
            }
            else {
                $('input:radio[class=form-radio-input][id=tipodireccion2]').prop('checked', false);
            }
            $('#TipoDireccion').val(data.IdTipoDireccion);
            $('#PaisAdicional').val(data.IdPais);
            $('#IdDireccionAdicional').val(data.Id);
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function GuardarDireccionAdicionalModificada() {
    $('#BtnGuardarDirecionAdicional').show();
    $('#BtnModificarDireccionAdicional').hide();
    $.ajax({
        url: "/Empresas/GuardarDireccionAdicionalModificada/",
        data: {
            idempresa: $('#IdEmpresa').val(),
            idtipodireccion: $('input:radio[name=tipodireccion]:checked').val(), 
            direccion: $('#DireccionAdicional').val(),
            cp: $('#CPAdicional').val(),
            ciudad: $('#divCPAdicional').text(),
            idpais: $('#PaisAdicional').val(),
            iddireccionadicional: $('#IdDireccionAdicional').val()
        },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            $.notify('Se modificó el registro exitosamente', { autohide: true, placement: { from: 'bottom', align: 'right' }, type: 'success' });
            $('#DireccionAdicional').val('');
            $('#CPAdicional').val('');
            $('#divCPAdicional').text('');
            $('#PaisAdicional').val('0');
            //Recargar la tabla
            $('#basic-datatables').empty();
            var firstRow = '<thead><tr><th>Tipo</th><th>Dirección</th><th>CP</th><th>Colonia, Ciudad, Estado</th><th>País</th></tr></thead><tbody>';
            $('#basic-datatables').append(firstRow);
            for (var i = 0; i < data.length; i++) {
                var tipodireccion = data[i].IdTipoDireccion === 1 ? 'Dirección' : 'Fiscal';
                var row = '<tr>' +
                    '<td>' + tipodireccion + '</td>' +
                    '<td>' + data[i].Direccion + '</td>' +
                    '<td>' + data[i].CP + '</td>' +
                    '<td>' + data[i].Ciudad + '</td>' +
                    '<td>' + data[i].NombrePais + '</td>' +
                    '</tr>';
                $('#basic-datatables').append(row);
            }
            var lastRow = '</tbody></table>';
            $('#basic-datatables').append(lastRow);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

$('.input-number').on('input', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

function CodigoPostal() {
    if ($('#CP').val() === '') {
        return;
    }
    $.ajax({
        async: true,
        url: "/Oportunidades/CodigoPostal/",
        data: {
            numero: $('#CP').val()
        },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            //console.log(data.length + '/' + data[0].CP + '/' + data[0].Direccion + ' ' + data[0].CP);
            if (typeof data[0] === 'undefined') {
                $('#divCP').empty();
                $('#divCP').val('');
                $('#divCP').append('Incorrecto!');
                $('#lblColonias').hide();
                $('#Colonias').hide();
            }
            else if (data.length >= 2) {
                //Agregar los cps encontrados en un select
                $('#divCP').empty();
                $('#Colonias').empty();
                $('#lblColonias').show();
                $('#Colonias').show();
                $('#Colonias').append('<option value=></option>');
                for (var i = 0; i < data.length; i++) {
                    $('#Colonias').append('<option value=' + data[i].Direccion + '>' + data[i].Direccion + '</option>');
                }
            }
            else if (data.length === 1) {
                $('#divCP').empty();
                $('#divCP').val('');
                $('#divCP').show();
                $('#divCP').append(data[0].Direccion);
                $('#hCiudad').val(data[0].Direccion);
                $('#Colonias').hide();
                $('#lblColonias').hide();

            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function CodigoPostal2() {
    if ($('#CPAdicional').val() === '') {
        return;
    }
    $.ajax({
        async: true,
        url: "/Oportunidades/CodigoPostal/",
        data: {
            numero: $('#CPAdicional').val()
        },
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            //console.log(data.length + '/' + data[0].CP + '/' + data[0].Direccion + ' ' + data[0].CP);
            if (typeof data[0] === 'undefined') {
                $('#divCPAdicional').empty();
                $('#divCPAdicional').val('');
                $('#divCPAdicional').append('Incorrecto!');
                $('#lblColoniasAdicional').hide();
                $('#ColoniasAdicional').hide();
            }
            else if (data.length >= 2) {
                //Agregar los cps encontrados en un select
                $('#divCPAdicional').empty();
                $('#ColoniasAdicional').empty();
                $('#lblColoniasAdicional').show();
                $('#ColoniasAdicional').show();
                $('#ColoniasAdicional').append('<option value=></option>');
                for (var i = 0; i < data.length; i++) {
                    $('#ColoniasAdicional').append('<option value=' + data[i].Direccion + '>' + data[i].Direccion + '</option>');
                }
            }
            else if (data.length === 1) {
                $('#divCPAdicional').empty();
                $('#divCPAdicional').val('');
                $('#divCPAdicional').show();
                $('#divCPAdicional').append(data[0].Direccion);
                $('#hCiudadAdicional').val(data[0].Direccion);
                $('#ColoniasAdicional').hide();
                $('#lblColoniasAdicional').hide();

            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function CodigoElegido() {
    var valor = $('#Colonias option:selected').text();
    $('#lblColonias').hide();
    $('#Colonias').hide();
    $('#divCP').empty();
    $('#divCP').show();
    $('#divCP').empty();
    $('#divCP').append(valor);
    $('#hCiudad').val(valor);
}

function CodigoElegido2() {
    var valor = $('#ColoniasAdicional option:selected').text();
    $('#lblColoniasAdicional').hide();
    $('#ColoniasAdicional').hide();
    $('#divCPAdicional').empty();
    $('#divCPAdicional').show();
    $('#divCPAdicional').empty();
    $('#divCPAdicional').append(valor);
    $('#hCiudadAdicional').val(valor);
}

function SolicitarCambios(idusuario, idempresa) {
    swal({
        title: 'Solicitar Modificación de Detalle',
        html: '<br><input class="form-control" placeholder="Agregue brevemente lo que desea que cambie el creador de la empresa" id="input-field">',
        content: {
            element: "input",
            attributes: {
                placeholder: "Solicito que se cambie o agregue",
                type: "text",
                id: "input-field",
                className: "form-control"
            },
        },
        buttons: {
            cancel: {
                visible: true,
                className: 'btn btn-danger'
            },
            confirm: {
                className: 'btn btn-success'
            }
        },
    }).then(
        function () {
            //Enviar un correo solicitando el cambio o el agregado
            if ($('#input-field').val() == '') { }
            else {
                EnviarCorreo(idusuario, idempresa, $('#input-field').val());
            }
        }
    );
}

function EnviarCorreo(idusuario, idempresa, mensaje) {
    $.ajax({
        type: "GET",
        url: "/Empresas/EnviarCorreoCambios",
        data: { idusuario: idusuario, idempresa: idempresa, mensaje: mensaje },
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            //console.log(data);
            if (data == "1") {
                swal({
                    title: 'CRM ASAE',
                    text: 'Se envió el correo con la solicitud.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-success'
                        }
                    },
                });
            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function MostrarCompartidos(id) {
    $.ajax({
        type: "GET",
        url: "/Empresas/EmpresasUsuariosCompartidos",
        data: { idempresa: id },
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            if (data.length === 0) {
                $('#divCompartidos').empty();
            }
            else {
                $('#divCompartidos').empty();
                var tabla = '<table class=table><tr><th>Compartido con</th><th></th></tr>';
                for (var i = 0; i < data.length; i++) {
                    tabla += '<tr><td>' + data[i].Usuarios.Nombre + '</td><td><a href=# onclick=EliminarUsuario(' + data[i].Usuarios.Id + ');>Eliminar</a></td></tr>';
                }
                tabla += '</table>';
                $('#divCompartidos').append(tabla);                
            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function EliminarUsuario(idusuario) {
    ValidarEmpresaUsuarioPermisos()
    $.ajax({
        type: "GET",
        url: "/Empresas/EliminarUsuarioCompartido",
        data: { idempresa: $('#Id').val(), idusuario: idusuario  },
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            if (data == 0) {
                swal({
                    title: 'CRM ASAE',
                    text: 'Sin permiso para eliminar la relación.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-warning'
                        }
                    },
                });
            }
            else {
                swal({
                    title: 'CRM ASAE',
                    text: 'Se eliminó el usuario con el que se compartía este registro.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-success'
                        }
                    },
                });
                MostrarCompartidos($('#Id').val());
            }
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

function ValidarEmpresaUsuarioPermisos() {
    $.ajax({
        type: "POST",
        url: "/Administracion/ValidarEmpresaSiUSuarioEsPropietarioOTienePermisoParaModificar",
        data: JSON.stringify({
            idempresa: $('#IdEmpresa').val(),
            idusuario: $('#IdUsuario').val()
        }),
        contentType: "application/json",
        dataType: "json",
        beforeSend: function () {
            $('#Espera').show();
        },
        success: function (data) {
            if (data == 0) {
                swal({
                    title: 'CRM ASAE',
                    text: 'Sólo el creador de la empresa puede modificar su contenido.',
                    buttons: {
                        confirm: {
                            className: 'btn btn-warning'
                        }
                    },
                });
            }
            return;
        },
        complete: function () {
            $('#Espera').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('jqXHR:');
            console.log(jqXHR);
            console.log('textStatus:');
            console.log(textStatus);
            console.log('errorThrown:');
            console.log(errorThrown);
        }
    });
}

