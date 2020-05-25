

// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {
      
    if ($("#Tema").val() == "" || ($("#Tema").val().length) < 3) {
        $("#Tema").focus();
        toastr.error('El Tema Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Descripcion").val() == "" || ($("#Descripcion").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Descripcion Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    } 
      
});

$("#BotonModificar").click(function () {

    if ($("#Tema").val() == "" || ($("#Tema").val().length) < 3) {
        $("#Tema").focus();
        toastr.error('El Tema Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Descripcion").val() == "" || ($("#Descripcion").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Descripcion Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }
    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }
});

