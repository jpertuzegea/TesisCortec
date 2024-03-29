﻿
//LLamado de modales para los modulos

// -------- inicio Personas --------
function RegistrarPersona() {
    $("#contenido").load("/Personas/PersonaAdd");
}
function Registrarse() {
    $("#contenido").load("/Registrarse/PersonaAdd");
}
 
function ModificarPersona(id) {
    $("#contenido").load("/Personas/PersonasUpdt/" + id);
}
function CambiarImagen(id) {
    $("#contenido").load("/Personas/CambioImagen/" + id);
}
function CambiarClave(id) {
    $("#contenido").load("/Personas/CambioClave/" + id);
}
function PersonaRolAdd(id) {
    $("#contenido").load("/Personas/PersonaRolAdd/" + id);
}



// -------- fin Personas --------


// -------- inicio notas rapidas --------
function CrearNotaRapida() {
    $("#contenido").load("/NotasRapidas/NotasRapidasAdd");
}
function ModificarNotaRapida(id) {
    $("#contenido").load("/NotasRapidas/NotasRapidasUpdt/" + id);
}
function ModificarPanelInformaivo() {
    $("#contenido").load("/NotasRapidas/PanelInformativoUpdt");
}
// -------- fin notas rapidas --------

// -------- inicio Cursos --------
function CrearCurso() {
    $("#contenido").load("/Cursos/CursosAdd");
}
function ModificarCurso(id) {
    $("#contenido").load("/Cursos/CursoUpdt/" + id);
}
function DetalleCurso(id) { 
    $('#MyModalDetalle').modal('show');
    $("#ContentDetalle").load("/Cursos/DetalleCurso/" + id);
}
function VisualizarInforme() { 
    $('#MyModaInicio').modal('show');
    $("#ContentInicio").load("/Inicio/PanelInformativo"); 
}
// -------- Fin notas rapidas --------
 
// -------- Inicio MaterialDigital --------
function CrearMaterialDidactico(id) { 
    $("#contenido").load("/MaterialDidactico/MaterialDidacticoAdd/" + id);
} 
// -------- Fin MaterialDigital --------


// -------- inicio Cronograma --------
function CrearActividadCronograma(id) {
    $("#contenido").load("/CronogramaActividades/CronogramaActividadesAdd/" + id);
}
function ModificarCronogramaActividadesCurso(id) {
    $("#contenido").load("/CronogramaActividades/CronogramaActividadesUpdt/" + id);
} 
// -------- Fin Cronograma --------

// -------- Inicio ForoTema --------
function CrearForoTema(id) {
    $("#contenido").load("/ForoTema/ForoTemaAdd/" + id);
}
function ModificarForoTema(id) {
    $("#contenido").load("/ForoTema/ForoTemaUpdt/" + id);
}
function ParticiparEnForoTema(ForoTemaId) {  
    $("#contenido").load("/ParticipacionEnForoTema/ParticiparEnForoTemaAdd/" + ForoTemaId);
}
// -------- Fin Foro --------


// -------- inicio Roles --------
function CrearRol() {
    $("#contenido").load("/Roles/RolesAdd");
}
function ModificarRol(id) {
    $("#contenido").load("/Roles/RolesUpdt/" + id);
}
function RolPerfilAdd(id) {
    $("#contenido").load("/Roles/RolPerfilAdd/" + id);
} 
// -------- fin Roles --------



