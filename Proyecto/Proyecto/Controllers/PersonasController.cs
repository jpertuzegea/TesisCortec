using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using DAO.ViewModel;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class PersonasController : Controller
    {

        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Listar_Persona)]
        public ActionResult Index()
        {
            //   Bll_Login.VerificarSesionActiva();
            Bll_Personas Bll_Personas = new Bll_Personas();
            List<Personas> Lista = Bll_Personas.ListarPersonas(EnumEstadoFiltro.Todos);
            return View(Lista);
        }


        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_Persona)]
        public ActionResult PersonaAdd()
        {
            //Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_Persona)]
        public ActionResult PersonaAdd(Personas Persona)
        {
            //Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", Persona.TipoDocumento);

            if (ModelState.IsValid)
            {
                Bll_Personas Bll_Personas = new Bll_Personas();

                if (Bll_Personas.GuardarPersona(Persona))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index");
                }
                else
                {// no creado
                    return View(Persona);
                }
            }
            else
            {
                return View(Persona);
            }
        }


        [HttpGet]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Modificar_Persona)]
        public ActionResult PersonasUpdt(int id)
        {
            //   Bll_Login.VerificarSesionActiva();
            Bll_Personas Bll_Personas = new Bll_Personas();
            Personas persona = Bll_Personas.GetPersonaByPersonaId(id);

            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", (int)persona.TipoDocumento);
            ViewBag.RolAcademico = new SelectList(FuncionesEnum.ListaEnum<EnumRolAcademico>(), "Value", "Text", (int)persona.RolAcademico);
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)persona.Estado);
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Modificar_Persona)]        
        public ActionResult PersonasUpdt(Personas Persona)
        {
            //   Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", (int)Persona.Estado);
            ViewBag.RolAcademico = new SelectList(FuncionesEnum.ListaEnum<EnumRolAcademico>(), "Value", "Text", Persona.RolAcademico);
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Persona.Estado);

            if (Persona != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_Personas Bll_Personas = new Bll_Personas();
                    if (Bll_Personas.ModificarPersona(Persona))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(Persona);
                    }
                }
                else
                {
                    return View(Persona);
                }
            }
            else
            {
                return View(Persona);
            }
        }

         
        public ActionResult ConvertirImagen(int PersonaId)
        {

            //   Bll_Login.VerificarSesionActiva();

            if (PersonaId == 0)// panel informativo siempre sera 0 (se reutilizo el metodo)
            {
                Bll_PanelInformativo Bll_PanelInformativo = new Bll_PanelInformativo();
                PanelInformativo PanelInformativo = Bll_PanelInformativo.ObtenerPanelInformativoByPanelInformativoId();
                if (PanelInformativo.Imagen != null)
                {
                    return File(PanelInformativo.Imagen, PanelInformativo.ContetType);
                }
                else
                {
                    return null;
                }
            }

            Bll_Personas Bll_Personas = new Bll_Personas();
            byte[] PersonaImagen = Bll_Personas.GetImagenByPersonaId(PersonaId);

            if (PersonaImagen != null)
            {
                if (PersonaImagen != null)
                {
                    return File(PersonaImagen, "image/jpg");
                }
                else
                {
                    return null;
                }
            }
            return null;
        }



        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Cambiar_Imagen_Persona)]
        public ActionResult CambioImagen()
        {
            //   Bll_Login.VerificarSesionActiva();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Cambiar_Imagen_Persona)]
        public ActionResult CambioImagen(HttpPostedFileBase file)
        {
            //   Bll_Login.VerificarSesionActiva();

            if (ModelState.IsValid)
            {
                Bll_Personas Bll_Personas = new Bll_Personas();

                if (Bll_Personas.CambioImagen(file))
                {
                    return RedirectToAction("Index", "Inicio");
                }
                else
                {// no creado
                    return View();
                }
            }
            else
            {
                return View();
            }
        }




        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Cambiar_Clave_Persona)]
        public ActionResult CambioClave()
        {
            //   Bll_Login.VerificarSesionActiva();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Cambiar_Clave_Persona)]
        public ActionResult CambioClave(string Clave, string NuevaClave)
        {
            //   Bll_Login.VerificarSesionActiva();

            Bll_Personas Bll_Personas = new Bll_Personas();

            if (Bll_Personas.CambioClave(NuevaClave))
            {
                return RedirectToAction("Index", "Inicio");
            }
            else
            {// no creado
                return View();
            }

        }



        [HttpGet]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Asignar_Rol_Persona)]
        public ActionResult PersonaRolAdd(int id)
        {
            //   Bll_Login.VerificarSesionActiva();

            Bll_Personas Bll_Personas = new Bll_Personas();
            ListaRolesDelaPersona lista = Bll_Personas.ListarRolesDeUnaPersona(id);
            return View(lista);
        }

        [HttpPost]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Asignar_Rol_Persona)]
        public ActionResult PersonaRolAdd(ListaRolesDelaPersona Lista)
        {
            //   Bll_Login.VerificarSesionActiva();

            if (Lista != null)
            {
                Bll_Personas Bll_Personas = new Bll_Personas();

                if (Bll_Personas.GestionarRolesDeUnaPersona(Lista))
                {
                    return RedirectToAction("Index", "Personas");
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