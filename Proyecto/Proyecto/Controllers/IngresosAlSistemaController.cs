using BLL;
using BLL.Enums;
using DAO;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class IngresosAlSistemaController : Controller
    {
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Listar_Ingreso_Al_Sistema)]
        public ActionResult Index()
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_IngresoAlSistema Bll_IngresoAlSistema = new Bll_IngresoAlSistema();
            List<IngresosAlSistema>lista = Bll_IngresoAlSistema.ListIngresosAlSistema();
            return View(lista);
        }
    }
}