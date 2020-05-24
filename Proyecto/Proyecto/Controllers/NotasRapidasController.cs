using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class NotasRapidasController : Controller
    {
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Listar_PostCard)]
        public ActionResult Index()
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas BLL_NotasRapidas = new Bll_NotasRapidas();
            List<NotasRapidas> Notas_Rapidas = BLL_NotasRapidas.ListarNotasRapidas(EnumEstadoFiltro.Todos);

            return View(Notas_Rapidas);
        }


        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_PostCard)]
        public ActionResult NotasRapidasAdd()
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_PostCard)]
        public ActionResult NotasRapidasAdd(NotasRapidas NotasRapidas)
        {
           //   Bll_Login.VerificarSesionActiva();

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


        [HttpGet]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Modificar_PostCard)]
        public ActionResult NotasRapidasUpdt(int id)
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();
            NotasRapidas NotaRapida = Bll_NotasRapidas.GetNotasRapidasByNotaRapidaId(id);
            //NotaRapida.FechaFinalizacionView = NotaRapida.FechaFinalizacion.ToString("yyyy-MM-dd");
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotaRapida.Estado);
            return View(NotaRapida);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Modificar_PostCard)]
        public ActionResult NotasRapidasUpdt(NotasRapidas NotasRapidas)
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotasRapidas.Estado);

            if (NotasRapidas != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();

                    if (Bll_NotasRapidas.ModificarNotasRapidas(NotasRapidas))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(NotasRapidas);
                    }
                }
                else
                {
                    return View(NotasRapidas);
                }
            }
            else
            {
                return View(NotasRapidas);
            }
        }


        [HttpGet]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Modificar_PanelInformativo)]
        public ActionResult PanelInformativoUpdt()
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_PanelInformativo Bll_PanelInformativo = new Bll_PanelInformativo();
            PanelInformativo PanelInformativo = Bll_PanelInformativo.ObtenerPanelInformativoByPanelInformativoId();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)PanelInformativo.Estado);

            return View(PanelInformativo);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Modificar_PanelInformativo)]
        public ActionResult PanelInformativoUpdt(PanelInformativo PanelInformativo, HttpPostedFileBase file)
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)PanelInformativo.Estado);

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    Bll_PanelInformativo Bll_PanelInformativo = new Bll_PanelInformativo();

                    if (Bll_PanelInformativo.ModificarPanelInformativo(PanelInformativo, file))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(PanelInformativo);
                    }
                }
                else
                {
                    return View(PanelInformativo);
                }
            }
            else
            {
                return View(PanelInformativo);
            }
        }
         

    }
}