
using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace Proyecto.Controllers.Api
{
    public class ApiPersonasController : ApiController
    {
        [HttpPost]
        [Route("api/EsCorrectaClave")]


        public bool EsCorrectaClave(JObject jObject)
        {
            try
            {
                Bll_Personas Bll_Personas = new Bll_Personas();
                return Bll_Personas.EsCorrectaClave(jObject["ClaveActual"].ToString(), (int)jObject["PersonaId"]);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            }
        }





    }
}