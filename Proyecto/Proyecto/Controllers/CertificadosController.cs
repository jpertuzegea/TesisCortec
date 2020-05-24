using BLL;
using BLL.Enums;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CertificadosController : Controller
    {

 
        public ActionResult Index(int CursoId)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_Certificado Bll_Certificado = new Bll_Certificado();
            Bll_Certificado.ImprimirCertificado(CursoId, 0); 
            return View();
        }


    }
}