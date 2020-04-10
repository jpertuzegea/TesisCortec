using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class IngresosAlSistemaController : Controller
    {
        // GET: IngresosAlSistema
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();

            Bll_IngresoAlSistema Bll_IngresoAlSistema = new Bll_IngresoAlSistema();
            List<IngresosAlSistema>lista = Bll_IngresoAlSistema.ListIngresosAlSistema();
            return View(lista);
        }
    }
}