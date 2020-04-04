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
    public class NotasRapidasController : Controller
    {
        // GET: Notas_Rapidas
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas BLL_NotasRapidas = new Bll_NotasRapidas();
            List<NotasRapidas> Notas_Rapidas = BLL_NotasRapidas.ListarNotasRapidas(EnumEstadoFiltro.Todos);

            return View(Notas_Rapidas);
        }

        // GET: Notas_RapidasAdd
        public ActionResult NotasRapidasAdd()
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");
            return View();
        }

        // POST: Crear Notas_Rapidas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotasRapidasAdd(NotasRapidas NotasRapidas)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotasRapidas.Estado);

            if (ModelState.IsValid)
            {
                Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();

                if (Bll_NotasRapidas.GuardarNotaRapida(NotasRapidas))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index");
                }
                else
                {// no creado
                    return View(NotasRapidas);
                }
            }
            else
            {
                return View(NotasRapidas);
            }
        }

        ////Update Notas_Rapidas
        //[HttpGet]
        //public ActionResult Notas_RapidasUpdt(string id)
        //{
        //    BLL_Login.VerificarSesionActiva(); // verifica que la sesion sea correcta

        //    BLL_NotasRapidas BLL_NotasRapidas = new BLL_NotasRapidas();
        //    NotaRapidaModel Notas_Rapidas_Model = Assemblers_NotasRapidas.De_Entidad_a_Modelo(BLL_NotasRapidas.GetNotasRapidasByNotaRapidaId(id));

        //    ViewBag.Estado = new SelectList(Funciones_Enum.mi_lista_enum<Enum_Estados>(), "Value", "Text", (int)Notas_Rapidas_Model.Estado);

        //    return View(Notas_Rapidas_Model);
        //}

        ////Update Notas_Rapidas
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Notas_RapidasUpdt(NotaRapidaModel Notas_Rapidas_Model)
        //{
        //    BLL_Login.VerificarSesionActiva(); // verifica que la sesion sea correcta

        //    ViewBag.Estado = new SelectList(Funciones_Enum.mi_lista_enum<Enum_Estados>(), "Value", "Text", (int)Notas_Rapidas_Model.Estado);

        //    if (Notas_Rapidas_Model != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            BLL_NotasRapidas BLL_NotasRapidas = new BLL_NotasRapidas();

        //            if (BLL_NotasRapidas.UpdateNotasRapidas(Notas_Rapidas_Model))
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View(Notas_Rapidas_Model);
        //            }
        //        }
        //        else
        //        {
        //            return View(Notas_Rapidas_Model);
        //        }
        //    }
        //    else
        //    {
        //        return View(Notas_Rapidas_Model);
        //    }
        //}

    }
}