 
// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {
      
    if ($("#Nombre").val() == "" || ($("#Nombre").val().length) < 3) {
        $("#Nombre").focus();
        toastr.error('El Nombre Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Descripcion").val() == "" || ($("#Descripcion").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Descripcion Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Codigo").val() == "" || ($("#Codigo").val().length) < 3) {
        $("#Codigo").focus();
        toastr.error('El Codigo Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#TituloOtorgado").val() == "" || ($("#TituloOtorgado").val().length) < 3) {
        $("#TituloOtorgado").focus();
        toastr.error('El Titulo Otorgado Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#ValorCurso").val() == "" || !PermiteNumeros.test($("#ValorCurso").val()) || ($("#ValorCurso").val().length) < 3) {
        $("#ValorCurso").focus();
        toastr.error('El Valor Curso Debe tener mas de 3 digitos', 'ERROR');
        return false;
    }

    if ($("#DuracionHoras").val() == "" || !PermiteNumeros.test($("#DuracionHoras").val()) || ($("#DuracionHoras").val().length) < 3) {
        $("#DuracionHoras").focus();
        toastr.error('La Duracion en Horas Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Docente option:selected").text() == "Seleccione una opcion" || $("#Docente option:selected").text() == "") {
        toastr.error('Debe seleccionar un Docente Valido', 'ERROR');
        return false;
    }

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }


    
});

$("#BotonModificar").click(function () {

    if ($("#Nombre").val() == "" || ($("#Nombre").val().length) < 3) {
        $("#Nombre").focus();
        toastr.error('El Nombre Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Descripcion").val() == "" || ($("#Descripcion").val().length) < 3) {
        $("#Descripcion").focus();
        toastr.error('La Descripcion Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Codigo").val() == "" || ($("#Codigo").val().length) < 3) {
        $("#Codigo").focus();
        toastr.error('El Codigo Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#TituloOtorgado").val() == "" || ($("#TituloOtorgado").val().length) < 3) {
        $("#TituloOtorgado").focus();
        toastr.error('El Titulo Otorgado Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#ValorCurso").val() == "" || !PermiteNumeros.test($("#ValorCurso").val()) || ($("#ValorCurso").val().length) < 3) {
        $("#ValorCurso").focus();
        toastr.error('El Valor Curso Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#DuracionHoras").val() == "" || !PermiteNumeros.test($("#DuracionHoras").val()) || ($("#DuracionHoras").val().length) < 3) {
        $("#DuracionHoras").focus();
        toastr.error('La Duracion en Horas Debe tener mas de 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Docente option:selected").text() == "Seleccione una opcion" || $("#Docente option:selected").text() == "") {
        toastr.error('Debe seleccionar un Docente Valido', 'ERROR');
        return false;
    }

    if ($("#Estado option:selected").text() == "Seleccione una opcion" || $("#Estado option:selected").text() == "") {
        toastr.error('Debe seleccionar un Estado Valido', 'ERROR');
        return false;
    }
});

