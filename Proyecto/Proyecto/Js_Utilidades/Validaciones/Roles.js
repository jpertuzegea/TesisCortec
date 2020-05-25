// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {

    if ($("#Nombre").val() == "" || !PermiteLetras.test($("#Nombre").val()) || ($("#Nombre").val().length) < 3) {
        $("#Nombre").focus();
        toastr.error('El Nombre solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Descripcion").val() == "" || !PermiteLetras.test($("#Descripcion").val()) || ($("#Descripcion").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Descripcion solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    } 

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#TipoDocumento option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }
     
});


$("#BotonModificar").click(function () { 

    if ($("#Nombre").val() == "" || !PermiteLetras.test($("#Nombre").val()) || ($("#Nombre").val().length) < 3) {
        $("#Nombre").focus();
        toastr.error('El Nombre solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Descripcion").val() == "" || !PermiteLetras.test($("#Descripcion").val()) || ($("#Descripcion").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Descripcion solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#TipoDocumento option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }

}); 






