 
// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {
      
    if ($("#NombreActividad").val() == "" || ($("#NombreActividad").val().length) < 3) {
        $("#NombreActividad").focus();
        toastr.error('El Nombre Actividad Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#FechaActividad").val() == "" || ($("#FechaActividad").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Fecha Actividad Debeser valida', 'ERROR');
        return false;
    } 

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }  

});

$("#BotonModificar").click(function () {

    if ($("#NombreActividad").val() == "" || ($("#NombreActividad").val().length) < 3) {
        $("#NombreActividad").focus();
        toastr.error('El Nombre Actividad Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#FechaActividad").val() == "" || ($("#FechaActividad").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Fecha Actividad Debeser valida', 'ERROR');
        return false;
    }

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }

});

