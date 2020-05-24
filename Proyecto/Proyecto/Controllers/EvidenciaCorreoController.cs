using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class EvidenciaCorreoController : Controller
    {
        // GET: EvienciaCorreo
        public ActionResult Index()
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_EvidenciaCorreo Bll_EvidenciaCorreo = new Bll_EvidenciaCorreo();
            List<EvidenciaCorreo> Lista = Bll_EvidenciaCorreo.ListarEvidenciaCorreo();
            return View(Lista);
        }
    }
}