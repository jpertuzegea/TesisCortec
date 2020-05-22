// Aca se realiza  la validacion para evitar que se ejecute el submit de unformulario mas de una vez 


function ValidarDobleCLicRegistrar() {
    // para que funcione, se debe cambiar la linea del form por la siguiente, adecuando el controlador y la accion  
    // @using (Html.BeginForm("accion", "controlador", FormMethod.Post, new { onsubmit = "return  ValidarDobleCLic()" })) { // el return indica que la accion del formulario llega false cuando ya le han dado clic una vez

    // alert("entro a la funcion ");
    document.getElementById("BotonRegistrar").disabled = true; // nombre del boton 
    document.getElementById("BotonRegistrar").innerHTML = "Enviando..."; // nombre del boton 
    return true;
}


function ValidarDobleClicModificar() {
    // para que funcione, se debe cambiar la linea del form por la siguiente, adecuando el controlador y la accion  
    // @using (Html.BeginForm("accion", "controlador", FormMethod.Post, new { onsubmit = "return  ValidarDobleCLic()" })) { // el return indica que la accion del formulario llega false cuando ya le han dado clic una vez

    // alert("entro a la funcion ");
    document.getElementById("BotonModificar").disabled = true; // nombre del boton 
    document.getElementById("BotonModificar").innerHTML = "Enviando..."; // nombre del boton 
    return true;
}