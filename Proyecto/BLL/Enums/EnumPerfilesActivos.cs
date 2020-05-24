using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Enums
{
    public enum EnumPerfilesActivos
    {

        // Menus 
        Permite_Visualizar_Menu_Administrador,
        Permite_Visualizar_Menu_Cursos,
        Permite_Visualizar_Menu_Reportes,

        // Sub Menus
        Permite_Visualizar_SubMenu_Gestion_Personas,
        Permite_Visualizar_SubMenu_Gestion_PostCard,
        Permite_Visualizar_SubMenu_Gestion_Cursos,
        Permite_Visualizar_SubMenu_Gestion_Roles,
        Permite_Visualizar_SubMenu_Evidencia_De_Correos,
        Permite_Visualizar_SubMenu_IngresosAlSistema,

        // Personas       
        Permite_Acceder_Listar_Persona,
        Permite_Acceder_Crear_Persona ,
        Permite_Acceder_Modificar_Persona,
        Permite_Acceder_Cambiar_Imagen_Persona,
        Permite_Acceder_Cambiar_Clave_Persona,
        Permite_Acceder_Asignar_Rol_Persona,

        // PostCard
        Permite_Acceder_Listar_PostCard,
        Permite_Acceder_Crear_PostCard,
        Permite_Acceder_Modificar_PostCard,
        Permite_Acceder_Modificar_PanelInformativo,

        // Cursos
        Permite_Acceder_Listar_Cursos,
        Permite_Acceder_Listar_Cursos_Ofertados,
        Permite_Acceder_Crear_Curso,
        Permite_Acceder_Modificar_Curso,
        Permite_Acceder_Detalle_Del_Cursos,
        Permite_Acceder_Listar_Cursos_Dictados,
        Permite_Acceder_Listar_Mis_Cursos,
        Permite_Acceder_Calificar_Estudiantes,
        Permite_Acceder_Ver_Notas_Estudiente,
        Permite_Acceder_Listar_Cronograma_Actividades_Del_Curso,
        Permite_Acceder_Crear_Cronograma_Actividades_Del_Curso,
        Permite_Acceder_Modificar_Cronograma_Actividades_Del_Curso,
        Permite_Acceder_Listar_Foro_Tema_Del_Curso,
        Permite_Acceder_Crear_Foro_Tema_Del_Curso,
        Permite_Acceder_Listar_Participaciones_Foro_Tema_Del_Curso,
        Permite_Acceder_Crear_Participacion_Foro_Tema_Del_Curso,
        Permite_Acceder_Modificar_Foro_Tema_Del_Curso,
        Permite_Acceder_Calificaciones_Del_Curso_y_Docente,
        Permite_Acceder_Calificar_Docente_y_Curso,
        Permite_Acceder_Listar_Material_Didactico_Del_Curso,
        Permite_Acceder_Crear_Material_Didactico_Del_Curso,
        Permite_Acceder_Descargar_Material_Didactico_Del_Curso,
        Permite_Acceder_Descargar_Certificado_Del_Curso,

        // Roles
        Permite_Acceder_Listar_Roles,
        Permite_Acceder_Crear_Roles,
        Permite_Acceder_Modificar_Roles,
        Permite_Acceder_Asignar_Perfil_Al_Rol, 

        // Reportes
        Permite_Acceder_Listar_Evidencia_Correo,
        Permite_Acceder_Listar_Ingreso_Al_Sistema

    }
}
