// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {


    if ($("#Titulo").val() == "" || ($("#Titulo").val().length) < 3) {
        $("#Titulo").focus();
        toastr.error('El Titulo No debe tener menos de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Mensaje").val() == "" || ($("#Mensaje").val().length) < 3) {
        $("#Mensaje").focus();
        toastr.error('El Mensaje No debe tener menos de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Mensaje").val() == "" || ($("#Mensaje").val().length) < 3) {
        $("#Mensaje").focus();
        toastr.error('El Mensaje No debe tener menos de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#FechaFinalizacion").val() == "" || ($("#FechaFinalizacion").val().length) < 3) {
        $("#FechaFinalizacion").focus();
        toastr.error('Debe seleccionar una fecha finalizacion valida', 'ERROR');
        return false;
    }

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }


});


$("#BotonModificar").click(function () {

    if ($("#Titulo").val() == "" || ($("#Titulo").val().length) < 3) {
        $("#Titulo").focus();
        toastr.error('El Titulo No debe tener menos de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Mensaje").val() == "" || ($("#Mensaje").val().length) < 3) {
        $("#Mensaje").focus();
        toastr.error('El Mensaje No debe tener menos de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Mensaje").val() == "" || ($("#Mensaje").val().length) < 3) {
        $("#Mensaje").focus();
        toastr.error('El Mensaje No debe tener menos de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#FechaFinalizacion").val() == "" || ($("#FechaFinalizacion").val().length) < 3) {
        $("#FechaFinalizacion").focus();
        toastr.error('Debe seleccionar una fecha finalizacion valida', 'ERROR');
        return false;
    }

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }
});


$("#BotonModificarPanel").click(function () {

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }
});


