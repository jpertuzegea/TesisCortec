// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {
      
    if ($("#Mensaje").val() == "" || ($("#Mensaje").val().length) < 3) {
        $("#Mensaje").focus();
        toastr.error('El Mensaje minimo son 3 caracteres', 'ERROR');
        return false;
    } 
      
});
 
