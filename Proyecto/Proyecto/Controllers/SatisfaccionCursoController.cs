using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using DAO.ViewModel;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class SatisfaccionCursoController : Controller
    {
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Calificar_Docente_y_Curso)]
        public ActionResult SatisfaccionCurso(int CursoId)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_SatisfaccionCurso Bll_SatisfaccionCurso = new Bll_SatisfaccionCurso();
            ListaPreguntas Lista = Bll_SatisfaccionCurso.ObtenerPreguntasSatisfaccion();
            ViewBag.RespuestaPregunta = new SelectList(FuncionesEnum.ListaEnum<EnumRangoCalificacion>(), "Value", "Text");
            ViewBag.CursoId = CursoId;

            return View(Lista);
        }

        [HttpPost]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Calificar_Docente_y_Curso)]
        public ActionResult SatisfaccionCurso(ListaPreguntas ListaPreguntas)
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.RespuestaPregunta = new SelectList(FuncionesEnum.ListaEnum<EnumRangoCalificacion>(), "Value", "Text");
            ViewBag.CursoId = ListaPreguntas.ListaRespuestas[0].CursoId;

            if (ModelState.IsValid)
            {
                Bll_CalificacionDocenteCursoEstudiante Bll_CalificacionDocenteCursoEstudiante = new Bll_CalificacionDocenteCursoEstudiante();

                if (Bll_CalificacionDocenteCursoEstudiante.GuardarCalificacion(ListaPreguntas))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index", "CursoEstudiante");
                }
                else
                {// no creado
                    return View(ListaPreguntas);
                }
            }
            else
            {
                return View(ListaPreguntas);
            }



        }
         
    }
}