
var PermiteEmail = /^[a-zA-Z0-9_\.\-]+@[a-zA-Z0-9\-]+\.[a-zA-Z0-9\-\.]+$/;
var PermiteLetras = /^[a-zA-z_\ \-_\�\-]*$/;
var PermiteLetras_y_Numeros = /^[a-zA-z_\0-9\-_\�\-]*$/;
var PermiteNumeros = /^[0-9\-]*$/;


// metodo que convierte un texto de minusculas a mayusculas
function UpperCase(texto) {
    texto.value = texto.value.toUpperCase();
}

