using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();
            List<NotasRapidas> Lista = Bll_NotasRapidas.VisualizarNotas();

            Bll_PanelInformativo Bll_PanelInformativo = new Bll_PanelInformativo();
            ViewBag.PanelInformativo = 1;// Bll_PanelInformativo.ObtenerPanelInformativoByPanelInformativoId().Estado;
            return View(Lista);
        }
        public ActionResult PanelInformativo()
        {
            Bll_Login.VerificarSesionActiva();
            return View();
        }

    }
}