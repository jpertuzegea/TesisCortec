
var PermiteEmail = /^[a-zA-Z0-9_\.\-]+@[a-zA-Z0-9\-]+\.[a-zA-Z0-9\-\.]+$/;
var PermiteLetras = /^[a-zA-z_\ \-_\�\-]*$/;
var PermiteLetras_y_Numeros = /^[a-zA-z_\0-9\-_\�\-]*$/;
var PermiteNumeros = /^[0-9\-]*$/;


// metodo que convierte un texto de minusculas a mayusculas
function UpperCase(texto) {
    texto.value = texto.value.toUpperCase();
}





function EjecutarAjax(MetodoHttp, RutaServicio, Parameter) {
    let resultado;

    $.ajax({
        dataType: "json",
        // headers: { 'Authorization': 'Bearer ' + localStorage.getItem('Token') },
        contentType: 'application/json',
        async: false,
        type: MetodoHttp,
        url: RutaServicio,
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


