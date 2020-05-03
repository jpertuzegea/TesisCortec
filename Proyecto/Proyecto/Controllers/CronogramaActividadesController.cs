using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CronogramaActividadesController : Controller
    {
        public ActionResult ListCronograma(int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_CronogramaActividadesCurso Bll_CronogramaActividadesCurso = new Bll_CronogramaActividadesCurso();
            List<CronogramaActividadesCurso> Lista = Bll_CronogramaActividadesCurso.ListarConogramasActividadesByCursoId(CursoId);
            ViewBag.CursoId = CursoId;
            return View(Lista);
        }

        public ActionResult CronogramaActividadesAdd(int id)
        {

            Bll_Login.VerificarSesionActiva();
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");
            ViewBag.CursoId = id;
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CronogramaActividadesAdd(CronogramaActividadesCurso CronogramaActividadesCurso)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)CronogramaActividadesCurso.Estado);
            ViewBag.CursoId = CronogramaActividadesCurso.CursoId;
            if (ModelState.IsValid)
            {
                Bll_CronogramaActividadesCurso Bll_CronogramaActividadesCurso = new Bll_CronogramaActividadesCurso();

                if (Bll_CronogramaActividadesCurso.GuardarCronogramaActividadesCurso(CronogramaActividadesCurso))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("ListCronograma", "CronogramaActividades", new { CursoId = CronogramaActividadesCurso.CursoId });
                }
                else
                {// no creado
                    return View(CronogramaActividadesCurso);
                }
            }
            else
            {
                return View(CronogramaActividadesCurso);
            }
        }

        [HttpGet]
        public ActionResult CronogramaActividadesUpdt(int id)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_CronogramaActividadesCurso Bll_CronogramaActividadesCurso = new Bll_CronogramaActividadesCurso();
            CronogramaActividadesCurso CronogramaActividadesCurso = Bll_CronogramaActividadesCurso.GetCronogramaActividadesCursoByCronogramaActividadesCursoId(id);
            ViewBag.CursoId = CronogramaActividadesCurso.CursoId;
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)CronogramaActividadesCurso.Estado);
            return View(CronogramaActividadesCurso);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CronogramaActividadesUpdt(CronogramaActividadesCurso CronogramaActividadesCurso)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)CronogramaActividadesCurso.Estado);

            if (CronogramaActividadesCurso != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_CronogramaActividadesCurso Bll_CronogramaActividadesCurso = new Bll_CronogramaActividadesCurso();

                    if (Bll_CronogramaActividadesCurso.ModificarCronogramaActividadesCurso(CronogramaActividadesCurso))
                    {
                        return RedirectToAction("ListCronograma", "CronogramaActividades", new { CursoId = CronogramaActividadesCurso.CursoId }); 
                    }
                    else
                    {
                        return View(CronogramaActividadesCurso);
                    }
                }
                else
                {
                    return View(CronogramaActividadesCurso);
                }
            }
            else
            {
                return View(CronogramaActividadesCurso);
            }
        }

    }
}