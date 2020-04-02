using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class PersonasController : Controller
    {
        // GET: Personas
        public ActionResult Index()
        {
            Bll_Personas Bll_Personas = new Bll_Personas();
            List<Personas> Lista = Bll_Personas.ListarPersonas(BLL.Enums.EnumEstados.Activo);
            return View(Lista);
        }

        public ActionResult ConvertirImagen(int PersonaId)
        {
            Bll_Personas Bll_Personas = new Bll_Personas();
            Personas Persona = Bll_Personas.GetImagenByPersonaId(PersonaId);
            if (Persona.Imagen != null)
            {
                return File(Persona.Imagen, "image/jpg");
            } 
            else
            {
                return null;
            }

        }

    }
}