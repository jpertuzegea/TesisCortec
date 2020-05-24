using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class RolesController : Controller
    {
        public ActionResult Index()
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_Roles BLL_Roles = new Bll_Roles();
            List<Roles> Roles = BLL_Roles.ListarRoles(EnumEstadoFiltro.Todos);

            return View(Roles);
        }

        public ActionResult RolesAdd()
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolesAdd(Roles Roles)
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Roles.Estado);

            if (ModelState.IsValid)
            {
                Bll_Roles Bll_Roles = new Bll_Roles();

                if (Bll_Roles.GuardarRol(Roles))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index");
                }
                else
                {// no creado
                    return View(Roles);
                }
            }
            else
            {
                return View(Roles);
            }
        }

        [HttpGet]
        public ActionResult RolesUpdt(int id)
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_Roles Bll_Roles = new Bll_Roles();
            Roles NotaRapida = Bll_Roles.GetRolByRolId(id);
            //NotaRapida.FechaFinalizacionView = NotaRapida.FechaFinalizacion.ToString("yyyy-MM-dd");
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotaRapida.Estado);
            return View(NotaRapida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolesUpdt(Roles Roles)
        {
           //   Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Roles.Estado);

            if (Roles != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_Roles Bll_Roles = new Bll_Roles();

                    if (Bll_Roles.ModificarRol(Roles))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(Roles);
                    }
                }
                else
                {
                    return View(Roles);
                }
            }
            else
            {
                return View(Roles);
            }
        }

        [HttpGet]
        public ActionResult RolPerfilAdd(int id)
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_Roles BLL_Roles = new Bll_Roles();
            ListaPerfilesDelRol lista = BLL_Roles.ListarPerfilesDeUnRol(id);

            return View(lista);
        }
      
        [HttpPost]
        public ActionResult RolPerfilAdd(ListaPerfilesDelRol Lista)
        {
           //   Bll_Login.VerificarSesionActiva();
             
            if (Lista != null)
            { 
                Bll_Roles BLL_Roles = new Bll_Roles();

                if (BLL_Roles.GestionarPerfilesDelRol(Lista))
                {
                    return RedirectToAction("Index","Roles");
                }
                else
                {
                    return View(Lista);
                }
            }
            else
            {
                return View(Lista);
            } 
        }

    }
}