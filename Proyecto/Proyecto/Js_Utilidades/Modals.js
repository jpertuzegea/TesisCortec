
//LLamado de modales para los modulos

// -------- inicio Personas --------
function RegistrarPersona() {
    $("#contenido").load("/Personas/PersonaAdd");
} 
function ModificarPersona(id) {
    alert(id);
    $("#contenido").load("/Personas/PersonasUpdt/" + id);
} // -------- fin Personas --------


// -------- inicio notas rapidas --------
function CrearNotaRapida() {
    $("#contenido").load("/NotasRapidas/NotasRapidasAdd");
}
function ModificarNotaRapida(id) {
    $("#contenido").load("/NotasRapidas/NotasRapidasUpdt/" + id);
}
// -------- fin notas rapidas --------

// -------- inicio Cursos --------
function CrearCurso() {
    $("#contenido").load("/Cursos/CursosAdd");
}
function ModificarCurso(id) {
    $("#contenido").load("/Cursos/CursosUpdt/" + id);
}
// -------- fin notas rapidas --------

