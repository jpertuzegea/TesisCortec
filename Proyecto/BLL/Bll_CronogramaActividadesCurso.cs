using BLL.Utilidades;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_CronogramaActividadesCurso
    {

        private TESISCortecEntities BD = null;

        public Bll_CronogramaActividadesCurso()
        {
            BD = new TESISCortecEntities();
        }

        public CronogramaActividadesCurso GetCronogramaActividadesCursoByCronogramaActividadesCursoId(int CronogramaActividadesCursoId)
        {
            try
            {
                CronogramaActividadesCurso CronogramaActividadesCurso = BD.CronogramaActividadesCurso.Find(CronogramaActividadesCursoId);
                if (CronogramaActividadesCurso != null)
                {
                    return (CronogramaActividadesCurso);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }



        public List<CronogramaActividadesCurso> ListarConogramasActividadesByCursoId(int CursoId)
        {
            try
            {
                List<CronogramaActividadesCurso> Lista = null;
                Lista = BD.CronogramaActividadesCurso.Where(x => x.CursoId == CursoId).ToList();
                return (Lista);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }

        }


        public bool GuardarCronogramaActividadesCurso(CronogramaActividadesCurso CronogramaActividadesCurso)
        {
            if (CronogramaActividadesCurso != null)
            {// si el objeto es diferente de nulo
                try
                {
                    CronogramaActividadesCurso.FechaPublicacion = UtilitiesCommons.ObtenerHorayFechaActualLocal();
                    BD.CronogramaActividadesCurso.Add(CronogramaActividadesCurso);
                    BD.SaveChanges();
                    return true;
                }
                catch (Exception error)
                {
                    Bll_File.EscribirLog(error.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }




        public bool ModificarCronogramaActividadesCurso(CronogramaActividadesCurso CronogramaActividadesCurso)
        {
            CronogramaActividadesCurso ActividadedCurso = GetCronogramaActividadesCursoByCronogramaActividadesCursoId(CronogramaActividadesCurso.CronogramaActividadesCursoId);

            if (ActividadedCurso != null)
            {
                try
                {
                    ActividadedCurso.NombreActividad = CronogramaActividadesCurso.NombreActividad;
                    ActividadedCurso.FechaActividad = CronogramaActividadesCurso.FechaActividad;
                    ActividadedCurso.Estado = (byte)CronogramaActividadesCurso.Estado;

                    BD.Entry(ActividadedCurso).State = EntityState.Modified;
                    BD.SaveChanges();
                    return true;
                }
                catch (Exception error)
                {
                    Bll_File.EscribirLog(error.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

    }
}
