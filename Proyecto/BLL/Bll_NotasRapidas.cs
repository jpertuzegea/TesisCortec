﻿using BLL.Enums;
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
    public class Bll_NotasRapidas
    {
        private TESISCortecEntities BD = null;

        public Bll_NotasRapidas()
        {
            BD = new TESISCortecEntities();
        }

        // metodo para listar las NotasRapidas existentes
        public List<NotasRapidas> ListarNotasRapidas(EnumEstadoFiltro Filtro)
        {
            try
            {
                List<NotasRapidas> ListNotasRapidas = null;

                switch (Filtro)
                {
                    case EnumEstadoFiltro.Activo://Activo
                        ListNotasRapidas = BD.NotasRapidas.Where(c => c.Estado == (byte)EnumEstados.Activo).ToList();
                        break;

                    case EnumEstadoFiltro.Inactivo://Inactivo
                        ListNotasRapidas = BD.NotasRapidas.Where(c => c.Estado == (byte)EnumEstados.Inactivo).ToList();
                        break;

                    case EnumEstadoFiltro.Todos:// Todos
                        ListNotasRapidas = BD.NotasRapidas.ToList();
                        break;
                }

                return (ListNotasRapidas);// retorna una lista de entidades
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        // Metodo para listar las NotasRapidas existentes que se encentren en estado activo y  la fecha este vigente
        public List<NotasRapidas> VisualizarNotas()
        {
            try
            {
                DateTime HoraActual = UtilitiesCommons.ObtenerHorayFechaActualLocal();
                List<NotasRapidas> ListNotasRapidas = BD.NotasRapidas.Where(c => c.Estado == (byte)EnumEstados.Activo).Where(c => c.FechaFinalizacion >= HoraActual).Include(x =>x.Personas).ToList();
                return (ListNotasRapidas);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        // metodo para buscar una sola NotaRapida por id
        public NotasRapidas GetNotasRapidasByNotaRapidaId(int IdNotaRapida)
        {
            try
            {
                NotasRapidas NotasRapidas = BD.NotasRapidas.Find(IdNotaRapida);
                if (NotasRapidas != null)
                {
                    return (NotasRapidas);
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

        // metodo para crear una NotasRapidas
        public bool GuardarNotaRapida(NotasRapidas NotasRapidas)
        {
            if (NotasRapidas != null)
            {// si el objeto es diferente de nulo
                try
                {
                    Bll_Personas Bll_Personas = new Bll_Personas();

                    NotasRapidas.FechaPublicacion = UtilitiesCommons.ObtenerHorayFechaActualLocal();
                    var IdUsuario = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
                    NotasRapidas.UsuarioPublica = Bll_Personas.GetPersonaByPersonaId(IdUsuario).PersonaId;

                    BD.NotasRapidas.Add(NotasRapidas);
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

        public bool ModificarNotasRapidas(NotasRapidas NotasRapidas)
        {
            NotasRapidas Notas = GetNotasRapidasByNotaRapidaId(NotasRapidas.NotaRapidaId);

            if (Notas != null)
            {
                try
                {
                    Notas.FechaPublicacion = UtilitiesCommons.ObtenerHorayFechaActualLocal();

                    Notas.FechaFinalizacion = NotasRapidas.FechaFinalizacion;

                    Notas.Titulo = NotasRapidas.Titulo;
                    Notas.Mensaje = NotasRapidas.Mensaje;
                    //NotasRapidas.UsuarioPublica = ;// No se puede modificar nunca
                    Notas.Estado = (byte)NotasRapidas.Estado;

                    BD.Entry(Notas).State = EntityState.Modified;
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