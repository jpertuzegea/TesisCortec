// las funciones de validacion utilizadas (PermiteLetras, etc...), se cargan en el layout y se encuentran en el archivo Utilidades.js

$("#BotonRegistrar").click(function () {

    if ($("#TipoDocumento option:selected").text() == "Seleccione una opcion" || $("#TipoDocumento option:selected").text() == "") {
        toastr.error('Debe seleccionar un Tipo de Documento Valido', 'ERROR');
        return false;
    }

    if ($("#NumDocumento").val() == "" || !PermiteNumeros.test($("#NumDocumento").val()) || ($("#NumDocumento").val().length) < 7 || ($("#NumDocumento").val().length) > 12) {
        $("#NumDocumento").focus();
        toastr.error('El Numero de Documento solo puede tener numeros entre 7 y 12 digitos', 'ERROR');
        return false;
    }

    if ($("#NombreCompleto").val() == "" || !PermiteLetras.test($("#NombreCompleto").val()) || ($("#NombreCompleto").val().length) < 3) {
        $("#NombreCompleto").focus();
        toastr.error('El Nombre solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Email").val() == "" || !PermiteEmail.test($("#Email").val()) || ($("#Email").val().length) < 3) {
        $("#Email").focus();
        toastr.error('El Email debe tener un formato valido', 'ERROR');
        return false;
    }

    if ($("#Ciudad").val() == "" || !PermiteLetras.test($("#Ciudad").val()) || ($("#Ciudad").val().length) < 3) {
        $("#Ciudad").focus();
        toastr.error('La Ciudad solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Departamento").val() == "" || !PermiteLetras.test($("#Departamento").val()) || ($("#Departamento").val().length) < 3) {
        $("#Departamento").focus();
        toastr.error('El Departamento solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Direccion").val() == "" || ($("#Direccion").val().length) < 3) {
        $("#Direccion").focus();
        toastr.error('La Direccion solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Telefono").val() == "" || !PermiteNumeros.test($("#Telefono").val()) || ($("#Telefono").val().length) < 6) {
        $("#Telefono").focus();
        toastr.error('El Telefono solo NO puede tener menos de 7 caracteres', 'ERROR');
        return false;
    }

    if ($("#Clave").val() == "" || ($("#Clave").val().length) < 9) {
        $("#Clave").focus();
        toastr.error('La clave No debe tener menos de 9 caracteres', 'ERROR');
        return false;
    }  
});


$("#BotonModificar").click(function () { 
    
    if ($("#TipoDocumento option:selected").text() == "Seleccione una opcion" || $("#TipoDocumento option:selected").text() == "") {
        toastr.error('Debe seleccionar un Tipo de Documento Valido', 'ERROR');
        return false;
    }

    if ($("#NumDocumento").val() == "" || !PermiteNumeros.test($("#NumDocumento").val()) || ($("#NumDocumento").val().length) < 7 || ($("#NumDocumento").val().length) > 12) {
        $("#NumDocumento").focus();
        toastr.error('El Numero de Documento solo puede tener numeros entre 7 y 12 digitos', 'ERROR');
        return false;
    }

    if ($("#NombreCompleto").val() == "" || !PermiteLetras.test($("#NombreCompleto").val()) || ($("#NombreCompleto").val().length) < 3) {
        $("#NombreCompleto").focus();
        toastr.error('El Nombre solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Email").val() == "" || !PermiteEmail.test($("#Email").val()) || ($("#Email").val().length) < 3) {
        $("#Email").focus();
        toastr.error('El Email debe tener un formato valido', 'ERROR');
        return false;
    }

    if ($("#Ciudad").val() == "" || !PermiteLetras.test($("#Ciudad").val()) || ($("#Ciudad").val().length) < 3) {
        $("#Ciudad").focus();
        toastr.error('La Ciudad solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Departamento").val() == "" || !PermiteLetras.test($("#Departamento").val()) || ($("#Departamento").val().length) < 3) {
        $("#Departamento").focus();
        toastr.error('El Departamento solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Direccion").val() == "" || ($("#Direccion").val().length) < 3) {
        $("#Direccion").focus();
        toastr.error('La Direccion solo puede tener letras mayor a 3 caracteres', 'ERROR');
        return false;
    }

    if ($("#Telefono").val() == "" || !PermiteNumeros.test($("#Telefono").val()) || ($("#Telefono").val().length) < 6) {
        $("#Telefono").focus();
        toastr.error('El Telefono solo NO puede tener menos de 7 caracteres', 'ERROR');
        return false;
    } 
});

$("#BotonCambiarClave").click(function () {

    if ($("#Clave").val() == "" || ($("#Clave").val().length) < 9) {
        $("#Clave").focus();
        toastr.error('La clave No debe tener menos de 9 caracteres', 'ERROR');
        return false;
    } 
     
    let Parameter = {
        ClaveActual: $('#Clave').val(),
        PersonaId: $('#PersonaId').val()
    }

    var datos = EjecutarAjax(Parameter);
    if (!datos) {
        toastr.error("Clave actual NO Valida", "Error");
        return false;
    }

   

    if ($("#NuevaClave").val() == "" || ($("#NuevaClave").val().length) < 9) {
        $("#NuevaClave").focus();
        toastr.error('La Nueva Clave No debe tener menos de 9 caracteres', 'ERROR');
        return false;
    }  
});



function EjecutarAjax(Parameter){
    let resultado;

    $.ajax({
        dataType: "json",
        // headers: { 'Authorization': 'Bearer ' + localStorage.getItem('Token') },
        contentType: 'application/json',
        async: false,
        type: 'post',
        url: '/api/EsCorrectaClave',
        data: JSON.stringify(Parameter),
        success: function (datos) {
            resultado = datos; 
        },

        error: (function (Response, textStatus, errorThrown) {

            if (Response.status == 401) {
                alert("Usted NO Esta Autorizado Para Acceder A Este Recurso");
            } else {
                console.log(" ----------------------------- ");
                console.log(" errorThrown --> " + JSON.stringify(errorThrown) + "\n\n\n  jqXHR --> " + JSON.stringify(Response));

                alert(" errorThrown --> " + JSON.stringify(errorThrown) + "\n\n\n  jqXHR --> " + JSON.stringify(Response));
            }
        })

    });

    return resultado;
} 


