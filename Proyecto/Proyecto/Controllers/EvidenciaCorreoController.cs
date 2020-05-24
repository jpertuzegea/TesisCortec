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
    public class EvidenciaCorreoController : Controller
    {
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Listar_Evidencia_Correo)]
        public ActionResult Index()
        {
           //   Bll_Login.VerificarSesionActiva();

            Bll_EvidenciaCorreo Bll_EvidenciaCorreo = new Bll_EvidenciaCorreo();
            List<EvidenciaCorreo> Lista = Bll_EvidenciaCorreo.ListarEvidenciaCorreo();
            return View(Lista);
        }
    }
}