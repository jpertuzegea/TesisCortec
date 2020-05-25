// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {
      
    if ($("#Mensaje").val() == "" || !PermiteLetras.test($("#Mensaje").val()) || ($("#Mensaje").val().length) < 3) {
        $("#Mensaje").focus();
        toastr.error('El Mensaje solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    } 
      
});
 
